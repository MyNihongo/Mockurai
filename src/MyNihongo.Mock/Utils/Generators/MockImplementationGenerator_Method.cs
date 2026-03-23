namespace MyNihongo.Mock.Utils;

internal static class MockImplementationMethodGenerator
{
	private const string FieldPrefix = MockGeneratorConst.Suffixes.MockVariableCall;

	public static IMethodSymbol? AppendMethodMockMethod(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IMethodSymbol methodSymbol)
			return null;

		stringBuilder.AppendNameComment(memberSymbol, indent);
		stringBuilder.AppendSetupInvocationFields(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
		stringBuilder.AppendMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);

		return TryGetMethodForSetupInvocationGeneration(methodSymbol);
	}

	private static IMethodSymbol? TryGetMethodForSetupInvocationGeneration(IMethodSymbol methodSymbol)
	{
		return methodSymbol.Parameters.Length >= 2
			? methodSymbol
			: null;
	}

	public static IEnumerable<IMethodSymbol> GetMethodMethods(MockedMemberSymbol memberSymbol)
	{
		if (memberSymbol.Symbol is IMethodSymbol methodSymbol)
			yield return methodSymbol;
	}

	public static void AppendProxyMethodImplementation(StringBuilder stringBuilder, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
	{
		if (memberSymbol.Symbol is not IMethodSymbol methodSymbol)
			return;

		stringBuilder
			.Indent(indent)
			.AppendInvocationDeclaration(methodSymbol, mockedTypeSymbol, memberSymbol, FieldPrefix, indent, out var genericTypeNames)
			.AppendLine();

		stringBuilder
			.Indent(indent)
			.AppendRegisterMethod(methodSymbol, memberSymbol, genericTypeNames)
			.AppendLine();

		stringBuilder
			.Indent(indent)
			.AppendInvokeMethod(methodSymbol, memberSymbol, genericTypeNames, indent)
			.AppendLine();
	}

	extension(StringBuilder stringBuilder)
	{
		private void AppendMethods(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			stringBuilder
				.AppendLine()
				.AppendSetupMethod(methodSymbol, mockedTypeSymbol, memberSymbol, indent)
				.AppendLine().AppendLine()
				.AppendVerifyMethods(methodSymbol, mockedTypeSymbol, memberSymbol, indent);
		}

		private StringBuilder AppendRegisterMethod(IMethodSymbol methodSymbol, MockedMemberSymbol memberSymbol, ImmutableArray<string> genericTypeNames)
		{
			if (genericTypeNames.IsDefaultOrEmpty)
			{
				stringBuilder
					.Append(FieldPrefix)
					.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind);
			}
			else
			{
				stringBuilder
					.AppendParameterName(memberSymbol.MemberName, methodSymbol.MethodKind, suffix: MockGeneratorConst.Suffixes.Invocation);
			}

			return stringBuilder
				.Append(".Register(_mock._invocationIndex, default);");
		}

		private StringBuilder AppendInvokeMethod(IMethodSymbol methodSymbol, MockedMemberSymbol memberSymbol, ImmutableArray<string> genericTypeNames, int indent)
		{
			var parameterSplit = methodSymbol.Parameters.SplitParameters();

			if (!parameterSplit.OutputParameters.IsDefaultOrEmpty)
			{
				stringBuilder
					.Append("if (")
					.Append(FieldPrefix)
					.AppendFieldName(memberSymbol.MemberName)
					.AppendLine(" is not null)");

				stringBuilder
					.Indent(indent++).AppendLine("{")
					.Indent(indent).AppendInvokeExecuteCall(methodSymbol, memberSymbol, indent).AppendLine()
					.Indent(--indent).AppendLine("}")
					.Indent(indent).AppendLine("else")
					.Indent(indent++).AppendLine("{");

				foreach (var parameter in parameterSplit.OutputParameters)
				{
					stringBuilder
						.Indent(indent)
						.Append(parameter.Name)
						.AppendLine(MockGeneratorConst.Suffixes.DefaultAssign);
				}

				stringBuilder
					.Indent(--indent).Append('}');
			}
			else
			{
				stringBuilder.AppendInvokeExecuteCall(methodSymbol, memberSymbol, indent);
			}

			return stringBuilder;
		}

		private StringBuilder AppendInvokeExecuteCall(IMethodSymbol methodSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			var hasReturnType = methodSymbol.TryGetReturnType() is not null;

			return stringBuilder
				.AppendIf(hasReturnType, "return ")
				.Append(FieldPrefix)
				.AppendFieldName(memberSymbol.MemberName)
				.Append("?.")
				.Append(hasReturnType ? "Execute" : "Invoke")
				.Append('(')
				.AppendParameterNames(methodSymbol.Parameters, appendRefModifier: true, appendComma: hasReturnType)
				.AppendIf(hasReturnType, "out var returnValue")
				.Append(")")
				.AppendIf(hasReturnType, " == true ? returnValue! : default")
				.Append(';');
		}
	}
}
