namespace MyNihongo.Mockurai.Utils;

internal static class MockImplementationPropertyGenerator
{
	public static ImmutableArray<IMethodSymbol> AppendPropertyMockMethod(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return default;

		stringBuilder.AppendNameComment(memberSymbol, indent);

		// Fields
		if (propertySymbol.GetMethod.TryGetNonPrivate(out var get))
			stringBuilder.AppendSetupInvocationFields(get!, mockedTypeSymbol, memberSymbol, indent);

		if (propertySymbol.SetMethod.TryGetNonPrivate(out var set))
			stringBuilder.AppendSetupInvocationFields(set!, mockedTypeSymbol, memberSymbol, indent);

		// Methods
		var methodBuilder = ImmutableArray.CreateBuilder<IMethodSymbol>();
		if (get is not null)
		{
			stringBuilder.AppendMethods(get, mockedTypeSymbol, memberSymbol, indent);
			methodBuilder.Add(get);
		}

		if (set is not null)
		{
			if (get is not null)
				stringBuilder.AppendLine();

			stringBuilder.AppendMethods(set, mockedTypeSymbol, memberSymbol, indent);
			methodBuilder.Add(set);
		}

		return methodBuilder.ToImmutable();
	}

	public static IEnumerable<IMethodSymbol> GetPropertyMethods(MockedMemberSymbol memberSymbol)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			yield break;

		if (propertySymbol.GetMethod.TryGetNonPrivate(out var get))
			yield return get!;
		if (propertySymbol.SetMethod.TryGetNonPrivate(out var set))
			yield return set!;
	}

	public static void AppendProxyPropertyImplementation(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return;

		stringBuilder
			.Indent(indent).AppendPropertyDeclaration(propertySymbol).AppendLine()
			.Indent(indent++).AppendLine("{");

		if (propertySymbol.GetMethod.TryGetNonPrivate(out var get))
			stringBuilder.AppendProxyImplementation(get!, mockedTypeSymbol, memberSymbol, indent);
		if (propertySymbol.SetMethod.TryGetNonPrivate(out var set))
			stringBuilder.AppendProxyImplementation(set!, mockedTypeSymbol, memberSymbol, indent);

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
			.Append(" { ");

		if (propertySymbol.GetMethod.TryGetNonPrivate(out var get))
			stringBuilder.Append("get; ");

		if (propertySymbol.SetMethod.TryGetNonPrivate(out var set))
		{
			var name = set!.IsInitOnly ? "init" : "set";

			stringBuilder
				.Append(name)
				.Append("; ");
		}

		stringBuilder
			.Append('}');

		if (get is not null)
			stringBuilder.Append(" = default!;");
	}

	public static void AppendPropertyMockExtensions(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, string mockClassName, Accessibility accessibility, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return;

		if (propertySymbol.GetMethod.TryGetNonPrivate(out var get))
			stringBuilder.AppendSetupVerifyExtensionMethods(get!, mockedTypeSymbol, mockClassName, accessibility, indent);

		if (propertySymbol.SetMethod.TryGetNonPrivate(out var set))
		{
			var prependNewLines = get is not null;
			stringBuilder.AppendSetupVerifyExtensionMethods(set!, mockedTypeSymbol, mockClassName, accessibility, indent, prependNewLines);
		}
	}

	public static void AppendPropertyMockSequenceExtensions(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, string mockClassName, Accessibility accessibility, int indent)
	{
		if (memberSymbol.Symbol is not IPropertySymbol propertySymbol)
			return;

		if (propertySymbol.GetMethod.TryGetNonPrivate(out var get))
			stringBuilder.AppendVerifySequenceExtensionMethods(get!, mockedTypeSymbol, mockClassName, accessibility, indent);

		if (propertySymbol.SetMethod.TryGetNonPrivate(out var set))
		{
			var prependNewLines = get is not null;
			stringBuilder.AppendVerifySequenceExtensionMethods(set!, mockedTypeSymbol, mockClassName, accessibility, indent, prependNewLines);
		}
	}

	extension(StringBuilder stringBuilder)
	{
		private StringBuilder AppendPropertyDeclaration(IPropertySymbol propertySymbol)
		{
			stringBuilder
				.AppendDeclaredAccessibility(propertySymbol)
				.Append(' ')
				.TryAppendOverride(propertySymbol)
				.AppendType(propertySymbol.Type)
				.Append(' ');

			if (propertySymbol.IsIndexer)
			{
				return stringBuilder
					.Append("this[")
					.AppendParameters(propertySymbol.Parameters)
					.Append(']');
			}

			return stringBuilder
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
				.Append("?.Execute(")
				.AppendParameterNames(methodSymbol.Parameters, appendComma: true)
				.Append("out var returnValue) == true ? returnValue! : default!");
		}

		private void AppendSetImplementation(IMethodSymbol methodSymbol, MockedMemberSymbol memberSymbol)
		{
			stringBuilder
				.Append(MockGeneratorConst.Suffixes.MockVariableCall)
				.AppendFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append("?.Invoke(")
				.AppendParameterNames(methodSymbol.Parameters)
				.Append(')');
		}
	}

	private static bool TryGetNonPrivate(this IMethodSymbol? @this, out IMethodSymbol? methodSymbol)
	{
		if (@this is { DeclaredAccessibility: not Accessibility.Private })
		{
			methodSymbol = @this;
			return true;
		}

		methodSymbol = null;
		return false;
	}
}
