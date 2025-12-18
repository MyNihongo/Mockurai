namespace MyNihongo.Mock.Utils;

internal static class MockImplementationPropertyGenerator
{
	public static void AppendPropertyMockMethod(StringBuilder stringBuilder, ITypeSymbol mockedTypeSymbol, MemberSymbol member, int indent)
	{
		if (member.Symbol is not IPropertySymbol propertySymbol)
			return;

		stringBuilder.AppendNameComment(member, indent);

		// Fields
		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendSetupInvocationFields(propertySymbol.GetMethod, member, indent);

		if (propertySymbol.SetMethod is not null)
			stringBuilder.AppendSetupInvocationFields(propertySymbol.SetMethod, member, indent);

		// Methods
		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendMethods(propertySymbol.GetMethod, mockedTypeSymbol, member, indent);

		if (propertySymbol.SetMethod is not null)
			stringBuilder.AppendMethods(propertySymbol.SetMethod, mockedTypeSymbol, member, indent);
	}

	extension(StringBuilder stringBuilder)
	{
		private void AppendMethods(IMethodSymbol methodSymbol, ITypeSymbol mockedTypeSymbol, MemberSymbol member, int indent)
		{
			stringBuilder
				.AppendLine()
				.AppendSetupMethod(methodSymbol, member, indent)
				.AppendLine().AppendLine()
				.AppendVerifyMethods(methodSymbol, mockedTypeSymbol, member, indent);
		}
	}
}
