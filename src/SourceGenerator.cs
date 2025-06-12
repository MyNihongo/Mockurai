namespace MyNihongo.Mock;

[Generator]
public sealed class SourceGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
#if DEBUG
		//if (!System.Diagnostics.Debugger.IsAttached)
		//	System.Diagnostics.Debugger.Launch();
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

					  internal sealed class MockTest
					  {
					  	public int Number { get; set; }
					  }
					  """;

				context.AddSource("Mock.g.cs", sample);
			});
	}

	private static bool SyntaxNodePredicate(SyntaxNode node, CancellationToken ct)
	{
		return false;
	}

	private static SyntaxNodeTransformResult SyntaxNodeTransform(GeneratorSyntaxContext context, CancellationToken ct)
	{
		return new SyntaxNodeTransformResult();
	}
}
