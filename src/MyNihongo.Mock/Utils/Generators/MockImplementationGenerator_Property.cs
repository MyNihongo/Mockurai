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
			stringBuilder
				.Indent(indent)
				.Append("private Setup<")
				.AppendType(propertySymbol.GetMethod.ReturnType)
				.Append(">? ")
				.AppendFieldName(member.MemberName)
				.AppendLine("Get;");
			
			stringBuilder
				.Indent(indent)
				.Append("private Invocation? ")
				.AppendFieldName(member.MemberName)
				.AppendLine("GetInvocation;");
		}

		if (propertySymbol.SetMethod is not null)
		{
			stringBuilder
				.Indent(indent)
				.Append("private SetupWithParameter<")
				.AppendType(propertySymbol.SetMethod.ReturnType)
				.Append(">? ")
				.AppendFieldName(member.MemberName)
				.AppendLine("Set;");
			
			stringBuilder
				.Indent(indent)
				.Append("private Invocation<")
				.AppendParameters(propertySymbol.SetMethod.Parameters)
				.Append(">? ")
				.AppendFieldName(member.MemberName)
				.AppendLine("SetInvocation;");
		}
	}
}
