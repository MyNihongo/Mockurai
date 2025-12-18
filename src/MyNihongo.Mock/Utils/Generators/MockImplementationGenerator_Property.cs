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

		// Setup method
		if (propertySymbol.GetMethod is not null)
		{
			stringBuilder
				.AppendLine()
				.AppendSetupMethod(propertySymbol.GetMethod, member, indent)
				.AppendLine().AppendLine()
				.AppendVerifyMethods(propertySymbol.GetMethod, mockedTypeSymbol,  member, indent);
		}
	}
}
