namespace MyNihongo.Mock.Utils;

internal static class MockImplementationPropertyGenerator
{
	public static IMethodSymbol? AppendPropertyMockMethod(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return null;

		stringBuilder.AppendNameComment(memberSymbol, indent);

		// Fields
		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendSetupInvocationFields(propertySymbol.GetMethod, mockedTypeSymbol, memberSymbol, indent);

		if (propertySymbol.SetMethod is not null)
			stringBuilder.AppendSetupInvocationFields(propertySymbol.SetMethod, mockedTypeSymbol, memberSymbol, indent);

		// Methods
		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendMethods(propertySymbol.GetMethod, mockedTypeSymbol, memberSymbol, indent);

		if (propertySymbol.SetMethod is not null)
		{
			if (propertySymbol.GetMethod is not null)
				stringBuilder.AppendLine();

			stringBuilder.AppendMethods(propertySymbol.SetMethod, mockedTypeSymbol, memberSymbol, indent);
		}

		return null;
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
