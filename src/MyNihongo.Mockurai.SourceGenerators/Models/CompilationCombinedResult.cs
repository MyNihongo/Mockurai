namespace MyNihongo.Mockurai.Models;

internal readonly struct CompilationCombinedResult(
	in Compilation compilation,
	in ImmutableArray<TestClassTransformResult?> testClassTransformResults,
	in ImmutableArray<MockFactoryTransformResult?> mockFactoryResults,
	in ConfigurationOptions options)
{
	public readonly Compilation Compilation = compilation;
	public readonly ImmutableArray<TestClassTransformResult?> TestClassTransformResults = testClassTransformResults;
	public readonly ImmutableArray<MockFactoryTransformResult?> MockFactoryResults = mockFactoryResults;
	public readonly ConfigurationOptions Options = options;
}
