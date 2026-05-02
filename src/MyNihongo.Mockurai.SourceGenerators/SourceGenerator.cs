namespace MyNihongo.Mockurai;

[Generator]
public sealed class SourceGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
#if DEBUG
		// if (!System.Diagnostics.Debugger.IsAttached)
		// 	System.Diagnostics.Debugger.Launch();
#endif
		var syntaxProvider = context.SyntaxProvider
			.CreateSyntaxProvider(SyntaxNodePredicate, SyntaxNodeTransform)
			.Collect();

		var configurationOptions = context.AnalyzerConfigOptionsProvider
			.SelectOptions();

		var valueProvider = context.CompilationProvider
			.Combine(syntaxProvider, configurationOptions);

		context
			.RegisterSourceOutput(valueProvider, static (context, source) =>
			{
				const string globalUsings = $"global using {MockGeneratorConst.Namespace};";
				var compilation = context.AddSourceToSyntaxTree("_Usings.g.cs", globalUsings, source.Compilation);

				var mockTypes = new HashSet<ITypeSymbol>(TypeSymbolNameComparer.Default);
				foreach (var namedTypeSymbol in new ClassInheritanceTree(source.TransformResults, compilation))
				{
					var mocks = namedTypeSymbol.CollectMocks();
					if (mocks.Count == 0)
						continue;

					var sourceCode = namedTypeSymbol.GenerateMockClass(mocks);
					compilation = context.AddSourceToSyntaxTree($"{namedTypeSymbol.Name}.g.cs", sourceCode, compilation);
					mockTypes.AddAll(mocks);
				}

				var methodSetups = new HashSet<IMethodSymbol>(MethodSymbolSetupComparer.Default);
				var methodInvocations = new HashSet<IMethodSymbol>(MethodSymbolInvocationComparer.Default);
				foreach (var mockType in mockTypes)
				{
					var sourceCodeResult = mockType.GenerateMockImplementation(source);
					context.AddSanitisedSource($"{sourceCodeResult.Name}.g.cs", sourceCodeResult.Source);

					methodSetups.TryAddAll(sourceCodeResult.MethodSymbols);
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

	private static bool SyntaxNodePredicate(SyntaxNode node, CancellationToken ct)
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

	private static SyntaxNodeTransformResult? SyntaxNodeTransform(GeneratorSyntaxContext context, CancellationToken ct)
	{
		if (context.Node is ClassDeclarationSyntax mockContainerClass)
			return new SyntaxNodeTransformResult(mockContainerClass);

		return null;
	}
}
