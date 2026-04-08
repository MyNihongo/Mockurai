namespace MyNihongo.Mockurai.Utils;

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

	public static void AppendProxyPropertyImplementation(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return;

		stringBuilder
			.Indent(indent).AppendPropertyDeclaration(propertySymbol).AppendLine()
			.Indent(indent++).AppendLine("{");

		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendProxyImplementation(propertySymbol.GetMethod, mockedTypeSymbol, memberSymbol, indent);
		if (propertySymbol.SetMethod is not null)
			stringBuilder.AppendProxyImplementation(propertySymbol.SetMethod, mockedTypeSymbol, memberSymbol, indent);

		stringBuilder
			.Indent(--indent).Append('}');
	}

	public static void AppendProxyPropertyDummyImplementation(StringBuilder stringBuilder, ISymbol memberSymbol, int indent)
	{
		if (memberSymbol is not IPropertySymbol propertySymbol)
			return;

		stringBuilder
			.Indent(indent)
			.AppendPropertyDeclaration(propertySymbol)
			.Append(" { get; ");

		if (propertySymbol.SetMethod is not null)
		{
			var name = propertySymbol.SetMethod.IsInitOnly ? "init" : "set";

			stringBuilder
				.Append(name)
				.Append("; ");
		}

		stringBuilder
			.Append('}');
	}

	public static void AppendPropertyMockExtensions(StringBuilder stringBuilder, MockedMemberSymbol memberSymbol, string mockClassName, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return;

		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendSetupVerifyExtensionMethods(propertySymbol.GetMethod, mockClassName, indent);

		if (propertySymbol.SetMethod is not null)
			stringBuilder.AppendSetupVerifyExtensionMethods(propertySymbol.SetMethod, mockClassName, indent, prependNewLines: propertySymbol.GetMethod is not null);
	}

	public static void AppendPropertyMockSequenceExtensions(StringBuilder stringBuilder, MockedMemberSymbol memberSymbol, string mockClassName, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return;

		if (propertySymbol.GetMethod is not null)
			stringBuilder.AppendVerifySequenceExtensionMethods(propertySymbol.GetMethod, mockClassName, indent);

		if (propertySymbol.SetMethod is not null)
			stringBuilder.AppendVerifySequenceExtensionMethods(propertySymbol.SetMethod, mockClassName, indent, prependNewLines: propertySymbol.GetMethod is not null);
	}

	extension(StringBuilder stringBuilder)
	{
		private StringBuilder AppendPropertyDeclaration(IPropertySymbol propertySymbol)
		{
			return stringBuilder
				.AppendDeclaredAccessibility(propertySymbol)
				.Append(' ')
				.TryAppendOverride(propertySymbol)
				.AppendType(propertySymbol.Type)
				.Append(' ')
				.Append(propertySymbol.Name);
		}

		private void AppendMethods(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			stringBuilder
				.AppendLine()
				.AppendSetupMethod(methodSymbol, mockedTypeSymbol, memberSymbol, indent)
				.AppendLine().AppendLine()
				.AppendVerifyMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
		}

		private void AppendProxyImplementation(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			const string mockPrefix = MockGeneratorConst.Suffixes.MockVariableCall;

			(string Name, Action<StringBuilder, IMethodSymbol, MockedMemberSymbol> Action)? methodProps = methodSymbol.MethodKind switch
			{
				MethodKind.PropertyGet => ("get", AppendGetImplementation),
				MethodKind.PropertySet => (methodSymbol.IsInitOnly ? "init" : "set", AppendSetImplementation),
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
				.Append(".Register(_mock._invocationIndex")
				.AppendParameterNamesOrDefault(methodSymbol.Parameters)
				.AppendLine(");");

			stringBuilder
				.Indent(indent)
				.AppendInvokeHandler(methodProps.Value.Action, methodSymbol, memberSymbol)
				.AppendLine(";");

			stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		private StringBuilder AppendInvokeHandler(Action<StringBuilder, IMethodSymbol, MockedMemberSymbol> action, IMethodSymbol methodSymbol, MockedMemberSymbol memberSymbol)
		{
			action(stringBuilder, methodSymbol, memberSymbol);
			return stringBuilder;
		}

		private void AppendGetImplementation(IMethodSymbol methodSymbol, MockedMemberSymbol memberSymbol)
		{
			stringBuilder
				.Append("return ")
				.Append(MockGeneratorConst.Suffixes.MockVariableCall)
				.AppendFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append("?.Execute(out var returnValue) == true ? returnValue! : default!");
		}

		private void AppendSetImplementation(IMethodSymbol methodSymbol, MockedMemberSymbol memberSymbol)
		{
			stringBuilder
				.Append(MockGeneratorConst.Suffixes.MockVariableCall)
				.AppendFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append("?.Invoke(value)");
		}
	}
}
