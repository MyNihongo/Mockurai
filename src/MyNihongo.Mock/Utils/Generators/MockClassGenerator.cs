namespace MyNihongo.Mock.Utils;

internal static class MockClassGenerator
{
	public static string GenerateMockClass(this CompilationCombinedResult @this, INamedTypeSymbol classSymbol, List<MockClassDeclaration> mocks)
	{
		return
			$$"""
			  namespace {{classSymbol.ContainingNamespace}};

			  {{classSymbol.GetAccessibilityString()}} partial class {{classSymbol.Name}}
			  {
			  }
			  """;
	}
}
