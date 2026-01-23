namespace MyNihongo.Mock.Utils;

internal static class MockSetupGenerator
{
	public static MockSetupResult GenerateMockSetup(this IMethodSymbol methodSymbol)
	{
		var stringBuilder = new StringBuilder();
		var className = CreateSetupClassName(stringBuilder, methodSymbol);

		var source =
			"""
			aaa
			""";

		return new MockSetupResult(
			name: className,
			source: source
		);
	}

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol)
	{
		stringBuilder.Clear();

		return stringBuilder
			.AppendSetupClassName(methodSymbol)
			.ToString();
	}
}
