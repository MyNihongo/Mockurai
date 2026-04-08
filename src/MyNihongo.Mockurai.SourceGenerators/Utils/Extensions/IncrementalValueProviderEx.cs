using Microsoft.CodeAnalysis.Diagnostics;

namespace MyNihongo.Mockurai.Utils;

internal static class IncrementalValueProviderEx
{
	public static IncrementalValueProvider<ConfigurationOptions> SelectOptions(this IncrementalValueProvider<AnalyzerConfigOptionsProvider> @this)
	{
		return @this
			.Select(static (x, _) =>
			{
				if (!x.GlobalOptions.TryGetValue("build_property.RootNamespace", out var rootNamespace))
					rootNamespace = MockGeneratorConst.Namespace;

				return new ConfigurationOptions(rootNamespace);
			});
	}

	public static IncrementalValueProvider<CompilationCombinedResult> Combine(
		this IncrementalValueProvider<Compilation> @this,
		in IncrementalValueProvider<ImmutableArray<SyntaxNodeTransformResult?>> transformResult,
		in IncrementalValueProvider<ConfigurationOptions> options)
	{
		return @this
			.Combine(transformResult)
			.Combine(options)
			.Select(static (x, _) => new CompilationCombinedResult(
				compilation: x.Left.Left,
				transformResults: x.Left.Right,
				options: x.Right
			));
	}
}
