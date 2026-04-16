namespace MyNihongo.Mockurai.Utils;

internal static class MockInvocationGenerator
{
	private const string JsonSnapshotName = "jsonSnapshot";

	public static MockSetupResult GenerateMockInvocation(this IMethodSymbol methodSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var className = CreateSetupClassName(stringBuilder, methodSymbol, out var genericTypeOverride);

		var source =
			$$"""
			  #nullable enable
			  namespace {{result.Options.RootNamespace}};

			  public sealed class {{className}} : IInvocationVerify
			  {
			  	private readonly string _name;
			  	private readonly string? {{CreatePrefixFields(stringBuilder, methodSymbol)}};
			  	private readonly InvocationContainer<Item> _invocations = [];

			  {{CreateConstructor(stringBuilder, methodSymbol, indent: 1)}}
			  {{CreateRegisterMethod(stringBuilder, methodSymbol, genericTypeOverride, indent: 1)}}
			  {{CreateVerifyMethod(stringBuilder, methodSymbol, VerifyMethodType.Times, genericTypeOverride, indent: 1)}}
			  {{CreateVerifyMethod(stringBuilder, methodSymbol, VerifyMethodType.Index, genericTypeOverride, indent: 1, appendNewLine: false)}}

			  	public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
			  	{
			  		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
			  		if (unverifiedItems is null)
			  			return;

			  {{CreateVerifyNoOtherCalls(stringBuilder, methodSymbol, genericTypeOverride, indent: 2)}}
			  		throw new MockUnverifiedException(verifyName, unverifiedItems);
			  	}

			  	public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
			  	{
			  		return _invocations;
			  	}

			  	private sealed class Item : IInvocation
			  	{
			  {{CreateItemFields(stringBuilder, methodSymbol, genericTypeOverride, indent: 2)}}
			  		private readonly {{className}} _invocation;

			  {{CreateItemConstructor(stringBuilder, methodSymbol, genericTypeOverride, indent: 2)}}

			  		public long Index { get; }

			  		public bool IsVerified { get; set; }

			  {{CreateItemGetMethods(stringBuilder, methodSymbol, genericTypeOverride, indent: 2)}}

			  		public override string ToString()
			  		{
			  			var stringBuilder = new System.Text.StringBuilder();
			  {{CreateToStringMethod(stringBuilder, methodSymbol, indent: 3)}}
			  			return $"{Index}: {stringValue}";
			  		}
			  	}
			  }
			  """;

		return new MockSetupResult(
			name: className,
			source: source
		);
	}

	private static string CreatePrefixFields(StringBuilder stringBuilder, IMethodSymbol methodSymbol)
	{
		stringBuilder.Clear();

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			if (i != 0)
				stringBuilder.Append(", ");

			stringBuilder.AppendParameterPrefixFieldName(i);
		}

		return stringBuilder.ToString();
	}

	private static string CreateConstructor(StringBuilder stringBuilder, IMethodSymbol methodSymbol, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent)
			.Append("public ")
			.AppendInvocationClassName(methodSymbol.Parameters, appendGenericDeclaration: false)
			.Append("(string name, ");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			if (i != 0)
				stringBuilder.Append(", ");

			stringBuilder
				.Append("string? ")
				.AppendParameterPrefixVariableName(i)
				.Append(" = null");
		}

		stringBuilder
			.AppendLine(")")
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendLine("_name = name;");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			stringBuilder
				.Indent(indent)
				.AppendParameterPrefixFieldName(i)
				.Append(" = ")
				.AppendParameterPrefixVariableName(i)
				.AppendLine(";");
		}

		return stringBuilder
			.Indent(--indent).AppendLine("}")
			.ToString();
	}

	private static string CreateRegisterMethod(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent)
			.Append("public void Register(in InvocationIndex.Counter index, ")
			.AppendParameters(methodSymbol.Parameters, parameterTypeOverride: genericTypeOverride, appendRefKind: false, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
			.AppendLine(")");

		stringBuilder
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendLine("var invokedIndex = index.Increment();")
			.Indent(indent).Append("_invocations.Add(new Item(invokedIndex, ")
			.AppendParameterNames(methodSymbol.Parameters, appendComma: true, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
			.AppendLine("invocation: this));")
			.Indent(--indent).AppendLine("}");

		return stringBuilder
			.ToString();
	}

	private static string CreateVerifyMethod(StringBuilder stringBuilder, IMethodSymbol methodSymbol, VerifyMethodType type, ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride, int indent, bool appendNewLine = true)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent)
			.Append("public ")
			.AppendVerifyReturnType(type)
			.Append(" Verify(")
			.AppendItSetupParameters(methodSymbol.Parameters, appendComma: true, parameterTypeOverride: genericTypeOverride, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
			.AppendVerifyParameter(type)
			.AppendLine(", System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)");

		stringBuilder
			.Indent(indent++).AppendLine("{")
			.Indent(indent).Append("var span = _invocations.")
			.AppendVerifySpan(type)
			.AppendLine(";").AppendLine();

		stringBuilder
			.Indent(indent).AppendLine("var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();")
			.Indent(indent).AppendLine("System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);")
			.AppendLine();

		if (type == VerifyMethodType.Times)
		{
			stringBuilder
				.Indent(indent)
				.AppendLine("var count = 0;");
		}

		stringBuilder
			.Indent(indent).AppendLine("for (var i = 0; i < span.Length; i++)")
			.Indent(indent++).AppendLine("{");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			stringBuilder
				.Indent(indent)
				.Append("var verify")
				.AppendParameterPropertyName(i)
				.Append(" = span[i].Get")
				.AppendParameterPropertyName(i)
				.Append('(')
				.AppendParameterVariableName(i)
				.AppendLine(".Type);");
		}

		stringBuilder
			.Indent(indent)
			.AppendLine("(string, ComparisonResult?)[]? verifyResults = null;")
			.AppendLine();

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			stringBuilder
				.Indent(indent)
				.Append("if (!")
				.AppendParameterVariableName(i)
				.Append(".Check(verify")
				.AppendParameterPropertyName(i)
				.Append(", out ");

			if (i == 0)
				stringBuilder.Append("var ");

			stringBuilder.AppendLine("result))");

			stringBuilder
				.Indent(indent++).AppendLine("{")
				.Indent(indent).Append("verifyResults = ");

			if (i > 0)
			{
				stringBuilder
					.AppendLine("verifyResults is not null")
					.Indent(indent + 1).Append("? [..verifyResults, ")
					.AppendVerifyResult(i)
					.AppendLine("]");

				stringBuilder
					.Indent(indent + 1).Append(": ");
			}

			stringBuilder
				.Append('[')
				.AppendVerifyResult(i)
				.AppendLine("];");

			stringBuilder
				.Indent(--indent)
				.AppendLine("}");
		}

		stringBuilder
			.AppendLine()
			.Indent(indent)
			.AppendLine("if (verifyResults is not null)")
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendLine("verifyOutput[i] = (span[i], verifyResults);")
			.Indent(indent).AppendLine("continue;")
			.Indent(--indent).AppendLine("}");

		// for loop end
		stringBuilder
			.AppendLine()
			.Indent(indent).AppendLine("verifyOutput[i] = (span[i], null);")
			.Indent(indent).AppendLine("span[i].IsVerified = true;")
			.Indent(indent).AppendVerifyLoopEnd(type).AppendLine()
			.Indent(--indent).AppendLine("}");

		switch (type)
		{
			case VerifyMethodType.Times:
				stringBuilder
					.AppendLine()
					.Indent(indent).AppendLine("if (times.Predicate(count))")
					.Indent(indent + 1).AppendLine("return;");

				break;
			case VerifyMethodType.Index:
				stringBuilder
					.AppendLine()
					.Indent(indent).AppendLine("if (invocationProviders is null)")
					.Indent(indent++).AppendLine("{")
					.Indent(indent).AppendLine("span = _invocations.GetItemsSpanBefore(index);")
					.Indent(indent).AppendLine("for (var i = 0; i < span.Length; i++)")
					.Indent(indent + 1).AppendLine("verifyOutput.Insert(i, (span[i], null));")
					.Indent(--indent).AppendLine("}");
				break;
		}

		stringBuilder
			.AppendLine()
			.Indent(indent).AppendLine("var invocations = verifyOutput.GetStrings(invocationProviders);")
			.Indent(indent).Append("var verifyName = string.Format(_name, ");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			if (i != 0)
				stringBuilder.Append(", ");

			stringBuilder
				.AppendParameterVariableName(i)
				.Append(".ToString(")
				.AppendParameterPrefixFieldName(i)
				.Append(')');
		}

		stringBuilder.AppendLine(");");

		stringBuilder
			.Indent(indent).AppendVerifyThrow(type).AppendLine()
			.Indent(--indent).Append('}');

		return appendNewLine
			? stringBuilder.AppendLine().ToString()
			: stringBuilder.ToString();
	}

	private static string CreateVerifyNoOtherCalls(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride, int indent)
	{
		stringBuilder.Clear();

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			var parameter = methodSymbol.Parameters[i];

			var template = genericTypeOverride.GetValueOrDefault(parameter);
			var typeOverride = template.Build(Fuck);

			stringBuilder
				.Indent(indent)
				.Append("var typeName")
				.AppendParameterPropertyName(i)
				.Append(" = !string.IsNullOrEmpty(")
				.AppendParameterPrefixFieldName(i)
				.Append(") ? $\"{")
				.AppendParameterPrefixFieldName(i)
				.Append("} ")
				.AppendTypeInsideString(parameter.Type, typeOverride)
				.Append("\" : ")
				.AppendTypeSeparate(parameter.Type, template, typeOverride)
				.AppendLine(";");
		}

		stringBuilder
			.Indent(indent).Append("var verifyName = string.Format(_name, ");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			if (i != 0)
				stringBuilder.Append(", ");

			stringBuilder
				.Append("typeName")
				.AppendParameterPropertyName(i);
		}

		return stringBuilder
			.Append(");")
			.ToString();

		static object Fuck(object x)
		{
			return $"{{typeof({x}).Name}}";
		}
	}

	private static string CreateItemFields(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride, int indent)
	{
		stringBuilder.Clear();

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			var parameter = methodSymbol.Parameters[i];

			var typeOverride = genericTypeOverride
				.GetValueOrDefault(parameter)
				.Build();

			stringBuilder
				.Indent(indent)
				.Append("private readonly ")
				.AppendType(parameter.Type, typeOverride)
				.Append(' ')
				.AppendParameterFieldName(i)
				.AppendLine(";");
		}

		stringBuilder
			.Indent(indent)
			.Append("private readonly string? ");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			if (i != 0)
				stringBuilder.Append(", ");

			stringBuilder
				.AppendFieldName(JsonSnapshotName)
				.AppendParameterPropertyName(i);
		}

		return stringBuilder
			.Append(';')
			.ToString();
	}

	private static string CreateItemConstructor(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent)
			.Append("public Item(long index, ")
			.AppendParameters(methodSymbol.Parameters, appendComma: true, parameterTypeOverride: genericTypeOverride, appendRefKind: false, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
			.AppendInvocationClassName(methodSymbol.Parameters, appendGenericDeclaration: true)
			.AppendLine(" invocation)");

		stringBuilder
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendLine("_invocation = invocation;")
			.Indent(indent).AppendLine("Index = index;");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			stringBuilder.AppendLine();

			stringBuilder
				.Indent(indent)
				.AppendParameterFieldName(i)
				.Append(" = ")
				.AppendParameterVariableName(i)
				.AppendLine(";");

			stringBuilder
				.Indent(indent)
				.AppendLine("try")
				.Indent(indent++).AppendLine("{");

			stringBuilder
				.Indent(indent)
				.AppendFieldName(JsonSnapshotName)
				.AppendParameterPropertyName(i)
				.Append(" = System.Text.Json.JsonSerializer.Serialize(")
				.AppendParameterVariableName(i)
				.AppendLine(");");

			stringBuilder
				.Indent(--indent)
				.AppendLine("}")
				.Indent(indent).AppendLine("catch")
				.Indent(indent).AppendLine("{")
				.Indent(indent + 1).AppendLine("// Swallow")
				.Indent(indent).AppendLine("}");
		}

		return stringBuilder
			.Indent(--indent).Append('}')
			.ToString();
	}

	private static string CreateItemGetMethods(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride, int indent)
	{
		stringBuilder.Clear();

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			var parameter = methodSymbol.Parameters[i];

			var typeOverride = genericTypeOverride
				.GetValueOrDefault(parameter)
				.Build();

			if (i != 0)
				stringBuilder.AppendLine().AppendLine();

			stringBuilder
				.Indent(indent)
				.Append("public ")
				.AppendType(parameter.Type, typeOverride)
				.Append(" Get")
				.AppendParameterPropertyName(i)
				.AppendLine("(SetupType setupType)")
				.Indent(indent++).AppendLine("{");

			stringBuilder
				.Indent(indent)
				.Append("return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(")
				.AppendFieldName(JsonSnapshotName)
				.AppendParameterPropertyName(i)
				.AppendLine(")");

			stringBuilder
				.Indent(indent + 1)
				.Append("? System.Text.Json.JsonSerializer.Deserialize<")
				.AppendType(parameter.Type, typeOverride)
				.Append(">(")
				.AppendFieldName(JsonSnapshotName)
				.AppendParameterPropertyName(i)
				.AppendLine(")!");

			stringBuilder
				.Indent(indent + 1)
				.Append(": ")
				.AppendParameterFieldName(i)
				.AppendLine(";");

			stringBuilder
				.Indent(--indent)
				.Append('}');
		}

		return stringBuilder
			.ToString();
	}

	private static string CreateToStringMethod(StringBuilder stringBuilder, IMethodSymbol methodSymbol, int indent)
	{
		stringBuilder.Clear();

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			stringBuilder
				.AppendLine()
				.Indent(indent)
				.Append("// ")
				.AppendParameterVariableName(i)
				.AppendLine();

			if (i != 0)
				stringBuilder
					.Indent(indent)
					.AppendLine("stringBuilder.Clear();");

			stringBuilder
				.Indent(indent)
				.Append("if (!string.IsNullOrEmpty(_invocation.")
				.AppendParameterPrefixFieldName(i)
				.AppendLine("))");

			stringBuilder
				.Indent(indent + 1)
				.Append("stringBuilder.Append($\"{_invocation.")
				.AppendParameterPrefixFieldName(i)
				.AppendLine("} \");");

			stringBuilder
				.Indent(indent)
				.Append("if (!string.IsNullOrEmpty(")
				.AppendFieldName(JsonSnapshotName)
				.AppendParameterPropertyName(i)
				.AppendLine("))");

			stringBuilder
				.Indent(indent + 1)
				.Append("stringBuilder.Append(")
				.AppendFieldName(JsonSnapshotName)
				.AppendParameterPropertyName(i)
				.AppendLine(");");

			stringBuilder
				.Indent(indent)
				.AppendLine("else");

			stringBuilder
				.Indent(indent + 1)
				.Append("stringBuilder.Append(")
				.AppendParameterFieldName(i)
				.AppendLine(");");

			stringBuilder
				.Indent(indent)
				.Append("var ")
				.AppendParameterVariableName(i)
				.AppendLine(" = stringBuilder.ToString();");
		}

		return stringBuilder
			.AppendLine()
			.Indent(indent)
			.Append("var stringValue = string.Format(_invocation._name, ")
			.AppendParameterNames(methodSymbol.Parameters, appendParameterName: MethodSymbolEx.AppendParameterVariableName)
			.Append(");")
			.ToString();
	}

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol, out ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride)
	{
		stringBuilder.Clear();

		return stringBuilder
			.AppendInvocationClassName(methodSymbol.Parameters, out genericTypeOverride)
			.ToString();
	}

	private enum VerifyMethodType
	{
		Times,
		Index,
	}

	extension(StringBuilder @this)
	{
		private StringBuilder AppendInvocationClassName(ImmutableArray<IParameterSymbol> parameters, bool appendGenericDeclaration)
		{
			return @this.AppendInvocationClassName(parameters, useOverriddenGenericNames: true, appendGenericDeclaration);
		}

		private StringBuilder AppendInvocationClassName(ImmutableArray<IParameterSymbol> parameters, out ImmutableDictionary<IParameterSymbol, StringTemplate> genericTypeOverride)
		{
			return @this.AppendInvocationClassName(parameters, useOverriddenGenericNames: true, out genericTypeOverride);
		}

		private StringBuilder AppendVerifyReturnType(VerifyMethodType type)
		{
			var returnType = type switch
			{
				VerifyMethodType.Times => "void",
				VerifyMethodType.Index => "long",
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Unknown verify method type: {type}"),
			};

			return @this.Append(returnType);
		}

		private StringBuilder AppendVerifyParameter(VerifyMethodType type)
		{
			var parameter = type switch
			{
				VerifyMethodType.Times => "in Times times",
				VerifyMethodType.Index => "long index",
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Unknown verify method type: {type}"),
			};

			return @this.Append(parameter);
		}

		private StringBuilder AppendVerifySpan(VerifyMethodType type)
		{
			var parameter = type switch
			{
				VerifyMethodType.Times => "GetItemsSpan()",
				VerifyMethodType.Index => "GetItemsSpanFrom(index)",
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Unknown verify method type: {type}"),
			};

			return @this.Append(parameter);
		}

		private StringBuilder AppendVerifyLoopEnd(VerifyMethodType type)
		{
			var parameter = type switch
			{
				VerifyMethodType.Times => "count++;",
				VerifyMethodType.Index => "return span[i].Index + 1;",
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Unknown verify method type: {type}"),
			};

			return @this.Append(parameter);
		}

		private StringBuilder AppendVerifyThrow(VerifyMethodType type)
		{
			var parameter = type switch
			{
				VerifyMethodType.Times => "throw new MockVerifyCountException(verifyName, times, count, invocations);",
				VerifyMethodType.Index => "throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);",
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Unknown verify method type: {type}"),
			};

			return @this.Append(parameter);
		}

		private StringBuilder AppendVerifyResult(int index)
		{
			return @this
				.Append("(\"")
				.AppendParameterVariableName(index)
				.Append("\", result)");
		}

		private StringBuilder AppendTypeInsideString(ITypeSymbol typeSymbol, string? typeOverride)
		{
			if (typeSymbol is ITypeParameterSymbol typeParameterSymbol)
			{
				@this
					.Append('{')
					.AppendTypeofName(typeParameterSymbol, typeOverride)
					.Append('}');
			}
			else
			{
				@this.AppendType(typeSymbol, typeOverride);
			}

			return @this;
		}

		private StringBuilder AppendTypeSeparate(ITypeSymbol typeSymbol, StringTemplate template, string? typeOverride)
		{
			if (typeSymbol is ITypeParameterSymbol typeParameterSymbol)
			{
				@this.AppendTypeofName(typeParameterSymbol, typeOverride);
			}
			else
			{
				@this
					.AppendIf(template.HasParameters, "$")
					.Append('"')
					.AppendType(typeSymbol, typeOverride)
					.Append('"');
			}

			return @this;
		}
	}
}
