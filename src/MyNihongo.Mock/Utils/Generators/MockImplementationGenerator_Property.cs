namespace MyNihongo.Mock.Utils;

internal static class MockImplementationPropertyGenerator
{
	public static void AppendPropertyMockMethod(StringBuilder stringBuilder, ITypeSymbol mockedTypeSymbol, MemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return;

		stringBuilder.AppendNameComment(memberSymbol, indent);

		// Fields
		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendSetupInvocationFields(propertySymbol.GetMethod, memberSymbol, indent);

		if (propertySymbol.SetMethod is not null)
			stringBuilder.AppendSetupInvocationFields(propertySymbol.SetMethod, memberSymbol, indent);

		// Methods
		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendMethods(propertySymbol.GetMethod, mockedTypeSymbol, memberSymbol, indent);

		if (propertySymbol.SetMethod is not null)
		{
			if (propertySymbol.GetMethod is not null)
				stringBuilder.AppendLine();

			stringBuilder.AppendMethods(propertySymbol.SetMethod, mockedTypeSymbol, memberSymbol, indent);
		}
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
