namespace MyNihongo.Mock;

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
				var mockTypes = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
				foreach (var result in source.TransformResults)
				{
					var namedTypeSymbol = source.Compilation
						.TryGetNamedTypeSymbol(result?.MockContainerClass);

					if (namedTypeSymbol is null)
						continue;

					var mocks = namedTypeSymbol.CollectMocks();
					if (mocks.Count == 0)
						continue;

					var sourceCode = namedTypeSymbol.GenerateMockClass(mocks);
					context.AddSource($"{namedTypeSymbol.Name}.g.cs", sourceCode);
					mockTypes.AddAll(mocks);
				}

				foreach (var mockType in mockTypes)
				{
					var sourceCode = mockType.GenerateMockImplementation(source);
					var a = "";
				}

				var globalUsings = $"global using {source.Options.RootNamespace};";
				context.AddSource("_Usings.g.cs", globalUsings);
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
