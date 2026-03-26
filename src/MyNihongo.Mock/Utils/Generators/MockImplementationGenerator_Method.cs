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
			.AppendMethodDeclaration(methodSymbol)
			.AppendLine()
			.Indent(indent++).AppendLine("{");

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

		stringBuilder
			.Indent(--indent).Append('}');
	}

	public static void AppendProxyMethodDummyImplementation(StringBuilder stringBuilder, ISymbol memberSymbol, int indent)
	{
		if (memberSymbol is not IMethodSymbol methodSymbol)
			return;

		stringBuilder
			.Indent(indent)
			.AppendMethodDeclaration(methodSymbol)
			.Append(" {");

		foreach (var parameter in methodSymbol.Parameters)
		{
			if (parameter.RefKind != RefKind.Out)
				continue;

			stringBuilder
				.Append(parameter.Name)
				.Append(MockGeneratorConst.Suffixes.DefaultAssign);
		}

		var returnTypeMetadata = methodSymbol.GetReturnTypeMetadata();

		stringBuilder
			.TryAppendDefaultReturn(returnTypeMetadata)
			.Append('}');
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

		private StringBuilder AppendMethodDeclaration(IMethodSymbol methodSymbol)
		{
			return stringBuilder
				.AppendDeclaredAccessibility(methodSymbol)
				.Append(' ')
				.TryAppendOverride(methodSymbol)
				.Append(methodSymbol.ReturnType)
				.Append(' ')
				.Append(methodSymbol.Name)
				.AppendGenericTypes(methodSymbol.TypeArguments)
				.Append('(')
				.AppendParameters(methodSymbol.Parameters)
				.Append(')');
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
				.Append(".Register(_mock._invocationIndex")
				.AppendParameterNamesOrDefault(methodSymbol.Parameters)
				.Append(");");
		}

		private StringBuilder AppendInvokeMethod(IMethodSymbol methodSymbol, MockedMemberSymbol memberSymbol, ImmutableArray<string> genericTypeNames, int indent)
		{
			var setupVariableName = methodSymbol.Parameters.GetSafeVariableName("setup");
			var returnTypeMetadata = methodSymbol.GetReturnTypeMetadata();
			var parameterSplit = methodSymbol.Parameters.SplitParameters();

			if (!parameterSplit.OutputParameters.IsDefaultOrEmpty)
			{
				stringBuilder
					.Append("if (")
					.Append(FieldPrefix)
					.AppendFieldName(memberSymbol.MemberName);

				if (genericTypeNames.IsDefaultOrEmpty)
				{
					stringBuilder.AppendLine(" is not null)");
				}
				else
				{
					stringBuilder
						.Append("?.TryGetValue(")
						.AppendTypesDeclaration(genericTypeNames)
						.Append(", out var ")
						.Append(setupVariableName)
						.AppendLine(") == true)");
				}

				stringBuilder
					.Indent(indent++).AppendLine("{")
					.Indent(indent).AppendInvokeExecuteCall(methodSymbol, memberSymbol, genericTypeNames, returnTypeMetadata, parameterSplit, setupVariableName, appendNullCheck: false, indent).AppendLine()
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
					.Indent(indent).TryAppendDefaultReturn(returnTypeMetadata).AppendLine()
					.Indent(--indent).Append('}');
			}
			else
			{
				stringBuilder.AppendInvokeExecuteCall(methodSymbol, memberSymbol, genericTypeNames, returnTypeMetadata, parameterSplit, setupVariableName, appendNullCheck: true, indent);
			}

			return stringBuilder;
		}

		private StringBuilder AppendInvokeExecuteCall(
			IMethodSymbol methodSymbol,
			MockedMemberSymbol memberSymbol,
			ImmutableArray<string> genericTypeNames,
			ReturnTypeMetadata returnTypeMetadata,
			ParameterSplit parameterSplit,
			string setupVariableName,
			bool appendNullCheck,
			int indent)
		{
			var hasReturnType = returnTypeMetadata.ReturnType is not null;
			var hasGenericTypes = !genericTypeNames.IsDefaultOrEmpty;
			var hasGenericAndOut = !parameterSplit.OutputParameters.IsDefaultOrEmpty && hasGenericTypes;

			if (returnTypeMetadata.ReturnType is not null)
			{
				stringBuilder.Append("return ");

				if (!string.IsNullOrEmpty(returnTypeMetadata.StaticInitializer))
				{
					stringBuilder
						.AppendFromResult(returnTypeMetadata.StaticInitializer!, returnTypeMetadata.ReturnType)
						.Append('(');
				}
			}

			if (hasGenericTypes)
			{
				stringBuilder
					.Append("((")
					.AppendSetupType(methodSymbol)
					.AppendIf(!hasGenericAndOut, "?")
					.Append(")");
			}

			if (hasGenericAndOut)
			{
				stringBuilder
					.Append(setupVariableName)
					.Append(").");
			}
			else
			{
				stringBuilder
					.Append(FieldPrefix)
					.AppendFieldName(memberSymbol.MemberName)
					.AppendIf(appendNullCheck, "?")
					.Append('.');

				if (hasGenericTypes)
				{
					stringBuilder
						.Append("ValueOrDefault(")
						.AppendTypesDeclaration(genericTypeNames)
						.Append("))?.");
				}
			}

			stringBuilder
				.Append(hasReturnType ? "Execute" : "Invoke")
				.Append('(')
				.AppendParameterNames(methodSymbol.Parameters, appendRefModifier: true, appendComma: hasReturnType);

			if (hasReturnType)
			{
				var returnValueName = methodSymbol.Parameters.GetReturnValueName();

				stringBuilder
					.Append("out var ")
					.Append(returnValueName)
					.Append(") == true ? ")
					.Append(returnValueName)
					.Append("! : default!");
			}
			else
			{
				stringBuilder.Append(')');
			}

			if (!string.IsNullOrEmpty(returnTypeMetadata.StaticInitializer))
			{
				if (hasReturnType)
				{
					stringBuilder.Append(')');
				}
				else
				{
					stringBuilder
						.AppendLine(";")
						.Indent(indent)
						.Append("return ")
						.AppendCompletedTask(returnTypeMetadata.StaticInitializer!);
				}
			}

			return stringBuilder
				.Append(';');
		}

		private StringBuilder TryAppendDefaultReturn(ReturnTypeMetadata returnTypeMetadata)
		{
			if (!string.IsNullOrEmpty(returnTypeMetadata.StaticInitializer))
			{
				stringBuilder.Append("return ");

				if (returnTypeMetadata.ReturnType is not null)
				{
					stringBuilder
						.AppendFromResult(returnTypeMetadata.StaticInitializer!, returnTypeMetadata.ReturnType)
						.Append("(default);");
				}
				else
				{
					stringBuilder
						.AppendCompletedTask(returnTypeMetadata.StaticInitializer!)
						.Append(';');
				}
			}
			else if (returnTypeMetadata.ReturnType is not null)
			{
				stringBuilder.Append("return default;");
			}

			return stringBuilder;
		}

		private StringBuilder AppendFromResult(string staticInitializer, ITypeSymbol returnType)
		{
			return stringBuilder
				.Append(staticInitializer)
				.Append(".FromResult")
				.AppendGenericTypes([returnType]);
		}

		private StringBuilder AppendCompletedTask(string staticInitializer)
		{
			return stringBuilder
				.Append(staticInitializer)
				.Append(".CompletedTask");
		}
	}
}
