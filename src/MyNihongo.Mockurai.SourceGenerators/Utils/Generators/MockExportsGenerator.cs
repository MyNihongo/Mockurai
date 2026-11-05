namespace MyNihongo.Mockurai.Utils;

internal static class MockExportsGenerator
{
	/// <summary>
	/// Generates <c>[assembly: MockuraiExported(typeof(T), typeof(TMock))]</c> for every exported type, so that consuming assemblies can find and reuse the exported mocks.
	/// </summary>
	public static string GenerateMockExports(this IEnumerable<ITypeSymbol> exportedTypes, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder()
			.AppendLine("#nullable enable");

		foreach (var exportedType in exportedTypes.OrderBy(static x => x.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat), StringComparer.Ordinal))
		{
			stringBuilder
				.Append("[assembly: global::").Append(MockGeneratorConst.Namespace).Append(".MockuraiExported(")
				.Append("typeof(").Append(exportedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)).Append("), ")
				.Append("typeof(global::").Append(result.Options.RootNamespace).Append('.').AppendMockClassName(exportedType).AppendLine("))]");
		}

		return stringBuilder.ToString().TrimEnd();
	}
}
