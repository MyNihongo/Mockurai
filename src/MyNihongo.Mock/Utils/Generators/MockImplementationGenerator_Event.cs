namespace MyNihongo.Mock.Utils;

internal static class MockImplementationEventGenerator
{
	public static void AppendEventMockMethod(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IEventSymbol eventSymbol)
			return;

		var eventTypeString = eventSymbol.Type.ToString();
		var parameterSymbol = eventSymbol.GetDelegateParameter();

		stringBuilder.AppendNameComment(memberSymbol, indent);

		// Fields
		stringBuilder
			.Indent(indent)
			.Append("private ")
			.Append(eventTypeString)
			.Append(' ')
			.AppendFieldName(memberSymbol.MemberName)
			.AppendLine(";");

		if (eventSymbol.AddMethod is not null)
			stringBuilder.AppendInvocationField(eventSymbol.AddMethod, mockedTypeSymbol, memberSymbol, indent);
		if (eventSymbol.RemoveMethod is not null)
			stringBuilder.AppendInvocationField(eventSymbol.RemoveMethod, mockedTypeSymbol, memberSymbol, indent);

		// Raise method
		stringBuilder
			.AppendLine()
			.Indent(indent)
			.Append("public void Raise")
			.AppendPropertyName(memberSymbol.Symbol.Name)
			.Append('(')
			.TryAppendParameter(parameterSymbol)
			.AppendLine(")");

		stringBuilder
			.Indent(indent++).AppendLine("{")
			.Indent(indent)
			.AppendFieldName(memberSymbol.MemberName)
			.Append("?.Invoke(Object, ")
			.Append(parameterSymbol?.Name)
			.AppendLine(");");

		stringBuilder
			.Indent(--indent).Append("}");

		// Verify methods
		if (eventSymbol.AddMethod is not null)
			stringBuilder.AppendMethod(eventSymbol.AddMethod, mockedTypeSymbol, memberSymbol, indent);
		if (eventSymbol.RemoveMethod is not null)
			stringBuilder.AppendMethod(eventSymbol.RemoveMethod, mockedTypeSymbol, memberSymbol, indent);
	}

	extension(StringBuilder stringBuilder)
	{
		private void AppendMethod(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			stringBuilder
				.AppendLine().AppendLine()
				.AppendVerifyMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
		}
	}
}
