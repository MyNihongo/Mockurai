using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace MyNihongo.Mockurai.Tests.ExportTests;

/// <summary>
/// Library (exports mocks with <see cref="MockuraiExportAttribute"/>) -> consumer (reuses the exported mocks) scenario.
/// Library extensions operate on the exported mock class, therefore the consumer must create exactly the same class.
/// </summary>
public sealed class ExportShould
{
	private const string LibraryCode =
		"""
		[assembly: MyNihongo.Mockurai.MockuraiExport(typeof(Library.IService))]

		namespace Library
		{
			public interface IService
			{
				string? Get(string key);

				void Set(string key, string? value);
			}

			public interface IInternalDependency
			{
				int Count();
			}

			public static class ServiceMockEx
			{
				public static void SetupValue(this IMock<IService> @this, string key, string value)
				{
					// calls the exported extension, which casts to the exported mock
					@this.SetupGet(key).Returns(value);
				}

				public static int CreateInternalMock()
				{
					var mock = Mock.Create<IInternalDependency>();
					mock.SetupCount().Returns(3);
					return mock.Object.Count();
				}
			}
		}
		""";

	private const string ConsumerCode =
		"""
		namespace Consumer
		{
			[MockuraiGenerate]
			public sealed partial class Tests
			{
				public partial IMock<Library.IService> ServiceMock { get; }

				public string? FromFactory()
				{
					var mock = Mock.Create<Library.IService>();
					Library.ServiceMockEx.SetupValue(mock, "key", "value");
					return mock.Object.Get("key");
				}

				public string? FromProperty()
				{
					Library.ServiceMockEx.SetupValue(ServiceMock, "key", "property");
					ServiceMock.SetupSet("key", "value");
					return ServiceMock.Object.Get("key");
				}
			}
		}
		""";

	[Fact]
	public void GeneratePublicExportedMock()
	{
		var (_, generated) = RunGenerator("Library", LibraryCode);

		var serviceMock = generated["ServiceMock.g.cs"];
		Assert.Contains("public sealed class ServiceMock : IMock<Library.IService>", serviceMock);
		Assert.Contains("public static partial class ServiceMockExtensions", serviceMock);
		Assert.Contains("public static partial class ServiceMockSequenceExtensions", serviceMock);
		Assert.DoesNotContain("class MockExtensions", serviceMock);
	}

	[Fact]
	public void GenerateInternalNotExportedMocksInLibrary()
	{
		var (_, generated) = RunGenerator("Library", LibraryCode);

		var internalMock = generated["InternalDependencyMock.g.cs"];
		Assert.Contains("internal sealed class InternalDependencyMock : IMock<Library.IInternalDependency>", internalMock);
		Assert.Contains("internal ISetup<System.Action, int, System.Func<int>> SetupCount() =>", internalMock);
	}

	[Fact]
	public void GenerateExportedAttribute()
	{
		var (_, generated) = RunGenerator("Library", LibraryCode);

		Assert.Equal(
			"""
			#nullable enable
			[assembly: global::MyNihongo.Mockurai.MockuraiExported(typeof(global::Library.IService), typeof(global::MyNihongo.Mockurai.ServiceMock))]
			""",
			generated["MockExports.g.cs"],
			ignoreLineEndingDifferences: true
		);
	}

	[Fact]
	public void ReuseExportedMockInConsumer()
	{
		var libraryReference = EmitLibrary();
		var (_, generated) = RunGenerator("Consumer", ConsumerCode, libraryReference);

		Assert.DoesNotContain("ServiceMock.g.cs", generated.Keys);
		Assert.DoesNotContain("MockExports.g.cs", generated.Keys);
		Assert.Contains("new global::MyNihongo.Mockurai.ServiceMock(InvocationIndex.CounterValue);", generated["MockFactoryInterceptors.g.cs"]);
		Assert.Contains("private readonly global::MyNihongo.Mockurai.ServiceMock _serviceMock = new(InvocationIndex.CounterValue);", generated["Tests.g.cs"]);
	}

	[Fact]
	public void RunLibraryExtensionsOnConsumerMocks()
	{
		var libraryReference = EmitLibrary(out var libraryImage);
		var (compilation, _) = RunGenerator("Consumer", ConsumerCode, libraryReference);

		var context = new AssemblyLoadContext(nameof(RunLibraryExtensionsOnConsumerMocks), isCollectible: true);
		try
		{
			context.Resolving += (_, name) => name.Name == "Library" ? context.LoadFromStream(new MemoryStream(libraryImage)) : null;
			var consumerAssembly = context.LoadFromStream(new MemoryStream(Emit(compilation)));

			var testsType = consumerAssembly.GetType("Consumer.Tests")!;
			var tests = Activator.CreateInstance(testsType)!;

			Assert.Equal("value", Invoke(testsType, tests, "FromFactory"));
			Assert.Equal("property", Invoke(testsType, tests, "FromProperty"));
		}
		finally
		{
			context.Unload();
		}

		static object? Invoke(Type type, object instance, string methodName) =>
			type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance)!.Invoke(instance, null);
	}

	private static MetadataReference EmitLibrary() =>
		EmitLibrary(out _);

	private static MetadataReference EmitLibrary(out byte[] image)
	{
		var (compilation, _) = RunGenerator("Library", LibraryCode);
		image = Emit(compilation);
		return MetadataReference.CreateFromImage(image);
	}

	private static (Compilation Compilation, Dictionary<string, string> Generated) RunGenerator(string assemblyName, string code, params MetadataReference[] additionalReferences)
	{
		var parseOptions = new CSharpParseOptions(LanguageVersion.Preview)
			.WithFeatures([new KeyValuePair<string, string>("InterceptorsNamespaces", "MyNihongo.Mockurai")]);

		var syntaxTree = CSharpSyntaxTree.ParseText(code, parseOptions, path: $"/{assemblyName}/Code.cs");
		var compilation = CSharpCompilation.Create(
			assemblyName,
			[syntaxTree],
			GetReferences().Concat(additionalReferences),
			new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable)
		);

		var driver = CSharpGeneratorDriver
			.Create([new SourceGenerator().AsSourceGenerator()], parseOptions: parseOptions)
			.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var generatorDiagnostics);

		Assert.Empty(generatorDiagnostics);

		var errors = outputCompilation
			.GetDiagnostics()
			.Where(static x => x.Severity == DiagnosticSeverity.Error)
			.Select(static x => x.ToString());

		Assert.Empty(errors);

		var generated = driver
			.GetRunResult()
			.GeneratedTrees
			.ToDictionary(static x => Path.GetFileName(x.FilePath), static x => x.GetText().ToString());

		return (outputCompilation, generated);
	}

	private static byte[] Emit(Compilation compilation)
	{
		using var stream = new MemoryStream();
		var result = compilation.Emit(stream);

		Assert.True(result.Success, string.Join(Environment.NewLine, result.Diagnostics));
		return stream.ToArray();
	}

	private static IEnumerable<MetadataReference> GetReferences()
	{
		return ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
			.Split(Path.PathSeparator)
			.Append(typeof(Mock).Assembly.Location)
			.Distinct()
			.Select(static x => MetadataReference.CreateFromFile(x));
	}
}
