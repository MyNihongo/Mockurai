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
		if (node is FieldDeclarationSyntax { Declaration.Type: GenericNameSyntax type })
			return type.Identifier.ValueText is "IMock" or "Mock";

		return false;
	}

	private static SyntaxNodeTransformResult? SyntaxNodeTransform(GeneratorSyntaxContext context, CancellationToken ct)
	{
		if (context.Node is FieldDeclarationSyntax fieldDeclarationSyntax)
			return new SyntaxNodeTransformResult(fieldDeclarationSyntax);

		return null;
	}
}
