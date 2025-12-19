namespace MyNihongo.Mock.Utils;

internal static class MockImplementationEventGenerator
{
	public static void AppendEventMockMethod(StringBuilder stringBuilder, ITypeSymbol mockedTypeSymbol, MemberSymbol memberSymbol, int indent)
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
			stringBuilder.AppendInvocationField(eventSymbol.AddMethod, memberSymbol, indent);
		if (eventSymbol.RemoveMethod is not null)
			stringBuilder.AppendInvocationField(eventSymbol.RemoveMethod, memberSymbol, indent);

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
			.Indent(--indent).AppendLine("}");

		// Verify methods
		stringBuilder
			.AppendVerifyMethods(mockedTypeSymbol, memberSymbol, type: "add", eventTypeString, indent)
			.AppendLine()
			.AppendVerifyMethods(mockedTypeSymbol, memberSymbol, type: "remove", eventTypeString, indent);
	}

	extension(StringBuilder stringBuilder)
	{
		private StringBuilder AppendVerifyMethods(ITypeSymbol mockedTypeSymbol, MemberSymbol member, string type, string eventTypeString, int indent)
		{
			// Verify method - times
			stringBuilder
				.AppendLine()
				.Indent(indent)
				.Append("public void Verify")
				.AppendPropertyName(type)
				.AppendPropertyName(member.Symbol.Name)
				.Append("(in ")
				.Append(eventTypeString)
				.AppendLine(" handler, in Times times)");

			stringBuilder
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendInvocationDeclaration(mockedTypeSymbol, member, type, eventTypeString)
				.Indent(indent)
				.AppendFieldName(member.MemberName)
				.AppendPropertyName(type)
				.AppendLine("Invocation.Verify(handler, times, _invocationProviders);")
				.Indent(--indent).AppendLine("}");

			// Verify method - index
			stringBuilder
				.AppendLine()
				.Indent(indent)
				.Append("public long Verify")
				.AppendPropertyName(type)
				.AppendPropertyName(member.Symbol.Name)
				.Append("(in ")
				.Append(eventTypeString)
				.AppendLine(" handler, long index)");

			return stringBuilder
				.Indent(indent++).AppendLine("{")
				.Indent(indent).AppendInvocationDeclaration(mockedTypeSymbol, member, type, eventTypeString)
				.Indent(indent)
				.Append("return ")
				.AppendFieldName(member.MemberName)
				.AppendPropertyName(type)
				.AppendLine("Invocation.Verify(handler, index, _invocationProviders);")
				.Indent(--indent).Append('}');
		}

		// TODO: use from verify
		[Obsolete("Use a generic implementation from ParameterSymbolEx")]
		private StringBuilder AppendInvocationDeclaration(ITypeSymbol mockedTypeSymbol, MemberSymbol member, string type, string eventTypeString)
		{
			return stringBuilder
				.AppendFieldName(member.MemberName)
				.AppendPropertyName(type)
				.Append("Invocation ??= new Invocation<")
				.Append(eventTypeString)
				.Append(">(\"")
				.Append(mockedTypeSymbol.Name)
				.AppendGenericTypes(mockedTypeSymbol)
				.Append('.')
				.AppendPropertyName(member.Symbol.Name)
				.Append('.')
				.Append(type)
				.AppendLine("\");");
		}
	}
}
