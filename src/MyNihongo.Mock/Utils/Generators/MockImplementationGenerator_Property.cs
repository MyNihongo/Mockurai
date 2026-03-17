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

	public static IEnumerable<IMethodSymbol> GetPropertyMethods(MockedMemberSymbol memberSymbol)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			yield break;

		if (propertySymbol.GetMethod is not null)
			yield return propertySymbol.GetMethod;
		if (propertySymbol.SetMethod is not null)
			yield return propertySymbol.SetMethod;
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
