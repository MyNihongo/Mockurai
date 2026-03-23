namespace MyNihongo.Mock.Utils;

internal static class MockImplementationEventGenerator
{
	public static IMethodSymbol? AppendEventMockMethod(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IEventSymbol eventSymbol)
			return null;

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
			stringBuilder.AppendMockMethods(eventSymbol.AddMethod, mockedTypeSymbol, memberSymbol, indent);
		if (eventSymbol.RemoveMethod is not null)
			stringBuilder.AppendMockMethods(eventSymbol.RemoveMethod, mockedTypeSymbol, memberSymbol, indent);

		return null;
	}

	public static IEnumerable<IMethodSymbol> GetEventMethods(MockedMemberSymbol memberSymbol)
	{
		if (memberSymbol.Symbol is not IEventSymbol eventSymbol)
			yield break;

		if (eventSymbol.AddMethod is not null)
			yield return eventSymbol.AddMethod;
		if (eventSymbol.RemoveMethod is not null)
			yield return eventSymbol.RemoveMethod;
	}

	public static void AppendProxyEventImplementation(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IEventSymbol eventSymbol)
			return;

		stringBuilder
			.Indent(indent)
			.AppendDeclaredAccessibility(eventSymbol)
			.Append(" event ")
			.AppendType(eventSymbol.Type)
			.Append(' ')
			.AppendLine(eventSymbol.Name);

		stringBuilder
			.Indent(indent++).AppendLine("{");

		if (eventSymbol.AddMethod is not null)
			stringBuilder.AppendProxyImplementation(eventSymbol.AddMethod, mockedTypeSymbol, memberSymbol, indent);
		if (eventSymbol.RemoveMethod is not null)
			stringBuilder.AppendProxyImplementation(eventSymbol.RemoveMethod, mockedTypeSymbol, memberSymbol, indent);

		stringBuilder
			.Indent(--indent).Append('}');
	}

	extension(StringBuilder stringBuilder)
	{
		private void AppendMockMethods(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			stringBuilder
				.AppendLine().AppendLine()
				.AppendVerifyMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
		}

		private void AppendProxyImplementation(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			const string mockPrefix = MockGeneratorConst.Suffixes.MockVariableCall;

			(string Name, string Command)? methodProps = methodSymbol.MethodKind switch
			{
				MethodKind.EventAdd => ("add", "+="),
				MethodKind.EventRemove => ("remove", "-="),
				_ => null,
			};

			if (!methodProps.HasValue)
				return;

			stringBuilder
				.Indent(indent).AppendLine(methodProps.Value.Name)
				.Indent(indent++).AppendLine("{");

			stringBuilder
				.Indent(indent)
				.Append(mockPrefix)
				.AppendInvocationDeclaration(methodSymbol, mockedTypeSymbol, memberSymbol, indent)
				.AppendLine();

			stringBuilder
				.Indent(indent)
				.Append(mockPrefix)
				.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.AppendLine(".Register(_mock._invocationIndex, value);");

			stringBuilder
				.Indent(indent)
				.Append(mockPrefix)
				.AppendFieldName(memberSymbol.MemberName)
				.Append(' ')
				.Append(methodProps.Value.Command)
				.AppendLine(" value;");

			stringBuilder
				.Indent(--indent).AppendLine("}");
		}
	}
}
