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
		{
			var methodKind = propertySymbol.GetMethod.MethodKind;

			stringBuilder
				.Indent(indent)
				.Append("private ")
				.AppendSetupType(propertySymbol.GetMethod)
				.Append("? ")
				.AppendFieldName(member.MemberName, methodKind)
				.AppendLine(";");

			stringBuilder
				.Indent(indent)
				.Append("private Invocation? ")
				.AppendFieldName(member.MemberName, methodKind)
				.AppendLine("Invocation;");
		}

		if (propertySymbol.SetMethod is not null)
		{
			var methodKind = propertySymbol.SetMethod.MethodKind;

			stringBuilder
				.Indent(indent)
				.Append("private ")
				.AppendSetupType(propertySymbol.SetMethod)
				.Append("? ")
				.AppendFieldName(member.MemberName, methodKind)
				.AppendLine(";");

			stringBuilder
				.Indent(indent)
				.Append("private Invocation<")
				.AppendParameters(propertySymbol.SetMethod.Parameters)
				.Append(">? ")
				.AppendFieldName(member.MemberName, methodKind)
				.AppendLine("Invocation;");
		}

		// Setup method
		if (propertySymbol.GetMethod is not null)
		{
			stringBuilder
				.AppendLine()
				.AppendSetupMethod(propertySymbol.GetMethod, member, indent);
		}
	}
}
