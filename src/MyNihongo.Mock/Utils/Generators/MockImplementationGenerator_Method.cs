namespace MyNihongo.Mock.Utils;

internal static class MockImplementationMethodGenerator
{
	public static void AppendPropertyMockMethod(StringBuilder stringBuilder, ITypeSymbol mockedTypeSymbol, MemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IMethodSymbol methodSymbol)
			return;

		stringBuilder.AppendNameComment(memberSymbol, indent);
		stringBuilder.AppendSetupInvocationFields(methodSymbol, memberSymbol, indent);
		stringBuilder.AppendMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
	}

	extension(StringBuilder stringBuilder)
	{
		private void AppendMethods(IMethodSymbol methodSymbol, ITypeSymbol mockedTypeSymbol, MemberSymbol memberSymbol, int indent)
		{
			stringBuilder
				.AppendLine()
				.AppendSetupMethod(methodSymbol, memberSymbol, indent)
				.AppendLine().AppendLine()
				.AppendVerifyMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
		}
	}
}
