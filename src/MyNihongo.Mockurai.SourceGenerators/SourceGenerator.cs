using Microsoft.CodeAnalysis.CSharp;

namespace MyNihongo.Mockurai;

[Generator]
public sealed class SourceGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
#if DEBUG
//		if (!System.Diagnostics.Debugger.IsAttached)
//		 	System.Diagnostics.Debugger.Launch();
#endif
		var testClassProvider = context.SyntaxProvider
			.CreateSyntaxProvider(TestClassPredicate, TestClassTransform)
			.Collect();

		var mockFactoryProvider = context.SyntaxProvider
			.CreateSyntaxProvider(MockFactoryPredicate, MockFactoryTransform)
			.Collect();

		var configurationOptions = context.AnalyzerConfigOptionsProvider
			.SelectOptions();

		var valueProvider = context.CompilationProvider
			.Combine(testClassProvider, mockFactoryProvider, configurationOptions);

		context
			.RegisterSourceOutput(valueProvider, static (context, source) =>
			{
				const string globalUsings = $"global using {MockGeneratorConst.Namespace};";
				var compilation = context.AddSourceToSyntaxTree("_Usings.g.cs", globalUsings, source.Compilation);
				var exports = MockExports.Collect(compilation);

				// Exported mocks are always generated, even if they are not used in the current assembly
				var mockTypes = new HashSet<ITypeSymbol>(exports.Own, TypeSymbolNameComparer.Default);
				foreach (var classNode in new ClassInheritanceTree<TestClassTransformResult>(source.TestClassTransformResults, compilation))
				{
					var namedTypeSymbol = classNode?.GetNamedTypeSymbol(compilation);
					if (namedTypeSymbol is null)
						continue;

					var mocks = namedTypeSymbol.CollectMocks();
					if (mocks.Count == 0)
						continue;

					var sourceCode = namedTypeSymbol.GenerateMockClass(mocks, exports);
					compilation = context.AddSourceToSyntaxTree($"{namedTypeSymbol.Name}.g.cs", sourceCode, compilation);
					mockTypes.AddAll(mocks);
				}

				var mockFactoryInvocations = new Dictionary<ITypeSymbol, List<InterceptableLocation>>(TypeSymbolNameComparer.Default);
				foreach (var mockFactoryResult in source.MockFactoryResults)
				{
					var mockFactoryInvocation = mockFactoryResult?.GetMockFactoryInvocation(compilation);
					if (mockFactoryInvocation is null)
						continue;

					mockTypes.AddSingle(mockFactoryInvocation.Type);

					if (!mockFactoryInvocations.TryGetValue(mockFactoryInvocation.Type, out var locations))
						mockFactoryInvocations.Add(mockFactoryInvocation.Type, locations = []);

					locations.Add(mockFactoryInvocation.Location);
				}

				var methodSetups = new HashSet<IMethodSymbol>(MethodSymbolSetupComparer.Default);
				var methodInvocations = new HashSet<IMethodSymbol>(MethodSymbolInvocationComparer.Default);
				foreach (var mockType in mockTypes)
				{
					// Mocks exported from referenced assemblies are reused as they are
					if (exports.TryGetReferenced(mockType) is not null)
						continue;

					var sourceCodeResult = mockType.GenerateMockImplementation(source, exports);
					context.AddSanitisedSource($"{sourceCodeResult.Name}.g.cs", sourceCodeResult.Source);

					methodSetups.TryAddAll(sourceCodeResult.MethodSymbols);
				}

				if (exports.Own.Count > 0)
				{
					var exportsSourceCode = exports.Own.GenerateMockExports(source);
					context.AddSanitisedSource("MockExports.g.cs", exportsSourceCode);
				}

				if (mockFactoryInvocations.Count > 0)
				{
					var interceptorsSourceCode = mockFactoryInvocations.GenerateMockFactoryInterceptors(source, exports);
					context.AddSanitisedSource("MockFactoryInterceptors.g.cs", interceptorsSourceCode);
				}

				foreach (var methodSetup in methodSetups)
				{
					var setupSourceCode = methodSetup.GenerateMockSetup(source);
					context.AddSanitisedSource($"{setupSourceCode.Name}.g.cs", setupSourceCode.Source);

					if (!methodInvocations.Add(methodSetup))
						continue;

					var invocationSourceCode = methodSetup.GenerateMockInvocation(source);
					context.AddSanitisedSource($"{invocationSourceCode.Name}.g.cs", invocationSourceCode.Source);
				}
			});
	}

	private static bool TestClassPredicate(SyntaxNode node, CancellationToken ct)
	{
		if (node is ClassDeclarationSyntax { AttributeLists.Count: > 0 } classSyntax)
			return classSyntax.AttributeLists
				.SelectMany(static x => x.Attributes)
				.Any(static x => x.Name switch
				{
					IdentifierNameSyntax y => y.Identifier.ValueText == MockGeneratorConst.GenerateAttributeName,
					QualifiedNameSyntax y => y.Right.Identifier.Text == MockGeneratorConst.GenerateAttributeName,
					_ => false,
				});

		return false;
	}

	private static TestClassTransformResult? TestClassTransform(GeneratorSyntaxContext context, CancellationToken ct)
	{
		if (context.Node is ClassDeclarationSyntax mockContainerClass)
			return new TestClassTransformResult(mockContainerClass);

		return null;
	}

	private static bool MockFactoryPredicate(SyntaxNode node, CancellationToken ct)
	{
		return node is InvocationExpressionSyntax
		{
			ArgumentList.Arguments.Count: 0,
			Expression: MemberAccessExpressionSyntax
			{
				Name: GenericNameSyntax { Identifier.ValueText: MockGeneratorConst.FactoryMethodName, TypeArgumentList.Arguments.Count: 1 },
				Expression: IdentifierNameSyntax { Identifier.ValueText: MockGeneratorConst.FactoryClassName }
				or MemberAccessExpressionSyntax { Name.Identifier.ValueText: MockGeneratorConst.FactoryClassName }
				or AliasQualifiedNameSyntax { Name.Identifier.ValueText: MockGeneratorConst.FactoryClassName },
			},
		};
	}

	private static MockFactoryTransformResult? MockFactoryTransform(GeneratorSyntaxContext context, CancellationToken ct)
	{
		if (context.Node is InvocationExpressionSyntax invocation)
			return new MockFactoryTransformResult(invocation);

		return null;
	}
}
