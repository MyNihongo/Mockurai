namespace MyNihongo.Mock.Utils;

internal static class MockInvocationGenerator
{
	public static MockSetupResult GenerateMockInvocation(this IMethodSymbol methodSymbol)
	{
		var stringBuilder = new StringBuilder();
		var className = CreateSetupClassName(stringBuilder, methodSymbol);

		var source =
			"""
			TODO
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
			.AppendInvocationClassName(methodSymbol.Parameters)
			.ToString();
	}
}
