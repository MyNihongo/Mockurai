namespace MyNihongo.Mock.Utils;

internal static class MockInvocationGenerator
{
	private const string PrefixName = "prefix";

	public static MockSetupResult GenerateMockInvocation(this IMethodSymbol methodSymbol, CompilationCombinedResult result)
	{
		var stringBuilder = new StringBuilder();
		var className = CreateSetupClassName(stringBuilder, methodSymbol);

		var source =
			$$"""
			  namespace {{result.Options.RootNamespace}};

			  public sealed class {{className}} : IInvocationVerify
			  {
			  	private readonly string _name;
			  	private readonly string? {{CreatePrefixFields(stringBuilder, methodSymbol)}};
			  	private readonly InvocationContainer<Item> _invocations = [];

			  {{CreateConstructor(stringBuilder, methodSymbol, indent: 1)}}
			  {{CreateRegisterMethod(stringBuilder, methodSymbol, indent: 1)}}
			  {{CreateVerifyMethod(stringBuilder, methodSymbol, VerifyMethodType.Times, indent: 1)}}
			  {{CreateVerifyMethod(stringBuilder, methodSymbol, VerifyMethodType.Index, indent: 1, appendNewLine: false)}}

			  	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
			  	{
			  		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
			  		if (unverifiedItems is null)
			  			return;

			  {{CreateVerifyNoOtherCalls(stringBuilder, methodSymbol, indent: 2)}}
			  		throw new MockUnverifiedException(verifyName, unverifiedItems);
			  	}

			  	public IEnumerable<IInvocation> GetInvocations()
			  	{
			  		return _invocations;
			  	}
			  	
			  	private sealed class Item : IInvocation
			  	{
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

			stringBuilder
				.AppendFieldName(PrefixName)
				.Append(i + 1);
		}

		return stringBuilder.ToString();
	}

	private static string CreateConstructor(StringBuilder stringBuilder, IMethodSymbol methodSymbol, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent)
			.Append("public ")
			.AppendInvocationClassName(methodSymbol.Parameters)
			.Append("(string name, ");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			if (i != 0)
				stringBuilder.Append(", ");

			stringBuilder
				.Append("string? ")
				.AppendParameterName(PrefixName)
				.Append(i + 1)
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
				.AppendFieldName(PrefixName)
				.Append(i + 1)
				.Append(" = ")
				.AppendParameterName(PrefixName)
				.AppendLine(";");
		}

		return stringBuilder
			.Indent(--indent).AppendLine("}")
			.ToString();
	}

	private static string CreateRegisterMethod(StringBuilder stringBuilder, IMethodSymbol methodSymbol, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent)
			.Append("public void Register(in InvocationIndex.Counter index, ")
			.AppendParameters(methodSymbol.Parameters)
			.AppendLine(")");

		stringBuilder
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendLine("var invokedIndex = index.Increment();")
			.Indent(indent).Append("_invocations.Add(new Item(invokedIndex, ")
			.AppendParameterNames(methodSymbol.Parameters, appendComma: true)
			.AppendLine("invocation: this));")
			.Indent(--indent).AppendLine("}");

		return stringBuilder
			.ToString();
	}

	private static string CreateVerifyMethod(StringBuilder stringBuilder, IMethodSymbol methodSymbol, VerifyMethodType type, int indent, bool appendNewLine = true)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent)
			.Append("public ")
			.AppendVerifyReturnType(type)
			.Append(" Verify(")
			.AppendItSetupParameters(methodSymbol.Parameters, appendComma: true)
			.AppendVerifyParameter(type)
			.AppendLine(", Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)");

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

		foreach (var parameter in methodSymbol.Parameters)
		{
			stringBuilder
				.Indent(indent)
				.Append("var verify")
				.AppendPropertyName(parameter.Name)
				.Append(" = span[i].Get")
				.AppendPropertyName(parameter.Name)
				.Append('(')
				.Append(parameter.Name)
				.AppendLine(".Type);");
		}

		stringBuilder
			.Indent(indent)
			.AppendLine("(string, ComparisonResult?)[]? verifyResults = null;")
			.AppendLine();

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			var parameterName = methodSymbol.Parameters[i].Name;

			stringBuilder
				.Indent(indent)
				.Append("if (!")
				.Append(parameterName)
				.Append(".Check(verify")
				.AppendPropertyName(parameterName)
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
					.AppendVerifyResult(parameterName)
					.AppendLine("]");

				stringBuilder
					.Indent(indent + 1).Append(": ");
			}

			stringBuilder
				.Append('[')
				.AppendVerifyResult(parameterName)
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
			.Indent(indent).Append("var verifyName = string.Format(_name,");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			if (i != 0)
				stringBuilder.Append(", ");

			stringBuilder
				.AppendParameterName(methodSymbol.Parameters[i].Name)
				.Append(".ToString(")
				.AppendFieldName(PrefixName)
				.Append(i + 1)
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

	private static string CreateVerifyNoOtherCalls(StringBuilder stringBuilder, IMethodSymbol methodSymbol, int indent)
	{
		stringBuilder.Clear();

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			var parameterIndex = i + 1;
			var parameterType = methodSymbol.Parameters[i].Type;

			stringBuilder
				.Indent(indent)
				.Append("var typeName")
				.Append(parameterIndex)
				.Append(" = !string.IsNullOrEmpty(")
				.AppendFieldName(PrefixName)
				.Append(parameterIndex)
				.Append(") ? $\"{")
				.AppendFieldName(PrefixName)
				.Append("} ")
				.AppendType(parameterType)
				.Append("\" : \"")
				.AppendType(parameterType)
				.AppendLine("\";");
		}

		stringBuilder
			.Indent(indent).Append("var verifyName = string.Format(_name, ");

		for (var i = 0; i < methodSymbol.Parameters.Length; i++)
		{
			if (i != 0)
				stringBuilder.Append(", ");

			stringBuilder
				.Append("typeName")
				.Append(i + 1);
		}

		return stringBuilder
			.Append(");")
			.ToString();
	}

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol)
	{
		stringBuilder.Clear();

		return stringBuilder
			.AppendInvocationClassName(methodSymbol.Parameters)
			.ToString();
	}

	private enum VerifyMethodType
	{
		Times,
		Index,
	}

	extension(StringBuilder @this)
	{
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

		private StringBuilder AppendVerifyResult(string parameterName)
		{
			return @this
				.Append("(\"")
				.Append(parameterName)
				.Append("\", result)");
		}
	}
}
