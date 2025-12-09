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

					var a = namedTypeSymbol.CollectMocks();
				}

				var sample =
					$$"""
					  namespace {{source.Options.RootNamespace}};

					  public interface ITest
					  {
					  }
					  """;

				context.AddSource("Mock.g.cs", sample);
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
