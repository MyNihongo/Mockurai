namespace MyNihongo.Mock.Models;

internal readonly struct CompilationCombinedResult(
	in Compilation compilation,
	in ImmutableArray<SyntaxNodeTransformResult> transformResults,
	in ConfigurationOptions options)
{
	public readonly Compilation Compilation = compilation;
	public readonly ImmutableArray<SyntaxNodeTransformResult> TransformResults = transformResults;
	public readonly ConfigurationOptions Options = options;
}
