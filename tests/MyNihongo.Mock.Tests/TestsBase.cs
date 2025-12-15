namespace MyNihongo.Mock.Tests;

public abstract class TestsBase
{
	protected static CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier> CreateFixture(string testCode, GeneratedSources expected)
	{
		return new CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier>
		{
			ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
			TestCode = testCode,
			TestState =
			{
				AdditionalReferences = { typeof(MockuraiGenerateAttribute).Assembly },
				GeneratedSources =
				{
					expected,
					(typeof(SourceGenerator), "_Usings.g.cs", "global using MyNihongo.Mock;"),
				},
			},
		};
	}

	protected static string CreateInterfaceTestCode(string members, string customCode)
	{
		return
			$$"""
			  namespace MyNihongo.Mock.Tests;

			  {{customCode}}

			  public interface IInterface
			  {
			  	{{members}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<IInterface> InterfaceMock { get; }
			  }
			  """;
	}

	protected static string CreateInterfaceGenericTestCode(string members, string customCode)
	{
		return
			$$"""
			  namespace MyNihongo.Mock.Tests;

			  {{customCode}}

			  public interface IInterface<T>
			  {
			  	{{members}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<IInterface<string>> InterfaceMock { get; }
			  }
			  """;
	}
	
	protected static string CreateClassTestCode(string members, string customCode)
	{
		return
			$$"""
			  namespace MyNihongo.Mock.Tests;

			  {{customCode}}

			  public class Class
			  {
			  	{{members}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<Class> ClassMock { get; }
			  }
			  """;
	}
}

file static class SourceFileCollectionEx
{
	public static void Add(this SourceFileCollection @this, GeneratedSources expected)
	{
		foreach (var item in expected)
		{
			@this.Add((typeof(SourceGenerator), item.FileName, item.SourceCode));
		}
	}
}
