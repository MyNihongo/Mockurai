namespace MyNihongo.Mock.Utils;

internal static class MockImplementationMethodGenerator
{
	public static IMethodSymbol? AppendMethodMockMethod(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IMethodSymbol methodSymbol)
			return null;

		stringBuilder.AppendNameComment(memberSymbol, indent);
		stringBuilder.AppendSetupInvocationFields(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
		stringBuilder.AppendMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);

		return TryGetMethodForSetupInvocationGeneration(methodSymbol);
	}

	private static IMethodSymbol? TryGetMethodForSetupInvocationGeneration(IMethodSymbol methodSymbol)
	{
		return methodSymbol.Parameters.Length >= 2
			? methodSymbol
			: null;
	}

	public static IEnumerable<IMethodSymbol> GetMethodVerifyNoOtherCallMethods(MockedMemberSymbol memberSymbol)
	{
		if (memberSymbol.Symbol is IMethodSymbol methodSymbol)
			yield return methodSymbol;
	}

	extension(StringBuilder stringBuilder)
	{
		private void AppendMethods(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			stringBuilder
				.AppendLine()
				.AppendSetupMethod(methodSymbol, mockedTypeSymbol, memberSymbol, indent)
				.AppendLine().AppendLine()
				.AppendVerifyMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
		}
	}
}
