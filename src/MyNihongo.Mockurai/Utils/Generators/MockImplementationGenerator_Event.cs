namespace MyNihongo.Mockurai.Utils;

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
			.Indent(indent).AppendRaiseMethodDeclaration(memberSymbol, parameterSymbol).AppendLine()
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
			.Indent(indent).AppendEventDeclaration(eventSymbol).AppendLine()
			.Indent(indent++).AppendLine("{");

		if (eventSymbol.AddMethod is not null)
			stringBuilder.AppendProxyImplementation(eventSymbol.AddMethod, mockedTypeSymbol, memberSymbol, indent);
		if (eventSymbol.RemoveMethod is not null)
			stringBuilder.AppendProxyImplementation(eventSymbol.RemoveMethod, mockedTypeSymbol, memberSymbol, indent);

		stringBuilder
			.Indent(--indent).Append('}');
	}

	public static void AppendProxyEventDummyImplementation(StringBuilder stringBuilder, ISymbol memberSymbol, int indent)
	{
		if (memberSymbol is not IEventSymbol eventSymbol)
			return;

		stringBuilder
			.Indent(indent)
			.AppendEventDeclaration(eventSymbol)
			.Append(';');
	}

	public static void AppendEventMockExtensions(StringBuilder stringBuilder, MockedMemberSymbol memberSymbol, string mockClassName, int indent)
	{
		if (memberSymbol.Symbol is not IEventSymbol eventSymbol)
			return;

		var parameterSymbol = eventSymbol.GetDelegateParameter();

		stringBuilder
			.Indent(indent)
			.AppendRaiseMethodDeclaration(memberSymbol, parameterSymbol)
			.AppendLine(" =>");

		stringBuilder
			.Indent(indent + 1)
			.AppendCastCall(mockClassName)
			.AppendRaiseMethodName(memberSymbol)
			.Append('(')
			.AppendParameterNames([parameterSymbol])
			.Append(");");

		if (eventSymbol.AddMethod is not null)
			stringBuilder.AppendVerifyExtensionMethods(eventSymbol.AddMethod, mockClassName, indent, prependNewLines: true);

		if (eventSymbol.RemoveMethod is not null)
			stringBuilder.AppendVerifyExtensionMethods(eventSymbol.RemoveMethod, mockClassName, indent, prependNewLines: true);
	}

	public static void AppendEventMockSequenceExtensions(StringBuilder stringBuilder, MockedMemberSymbol memberSymbol, string mockClassName, int indent)
	{
		if (memberSymbol.Symbol is not IEventSymbol eventSymbol)
			return;

		if (eventSymbol.AddMethod is not null)
			stringBuilder.AppendVerifySequenceExtensionMethods(eventSymbol.AddMethod, mockClassName, indent);

		if (eventSymbol.RemoveMethod is not null)
			stringBuilder.AppendVerifySequenceExtensionMethods(eventSymbol.RemoveMethod, mockClassName, indent, prependNewLines: true);
	}

	extension(StringBuilder stringBuilder)
	{
		private StringBuilder AppendRaiseMethodDeclaration(MockedMemberSymbol memberSymbol, IParameterSymbol? parameterSymbol)
		{
			return stringBuilder
				.Append("public void ")
				.AppendRaiseMethodName(memberSymbol)
				.Append('(')
				.TryAppendParameter(parameterSymbol)
				.Append(")");
		}

		private StringBuilder AppendRaiseMethodName(MockedMemberSymbol memberSymbol)
		{
			return stringBuilder
				.Append("Raise")
				.AppendPropertyName(memberSymbol.Symbol.Name);
		}

		private StringBuilder AppendEventDeclaration(IEventSymbol eventSymbol)
		{
			return stringBuilder
				.AppendDeclaredAccessibility(eventSymbol)
				.Append(' ')
				.TryAppendOverride(eventSymbol)
				.Append("event ")
				.AppendType(eventSymbol.Type)
				.Append(' ')
				.Append(eventSymbol.Name);
		}

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
