namespace MyNihongo.Mock.Utils;

internal static class MockSetupGenerator
{
	public static MockSetupResult GenerateMockSetup(this IMethodSymbol methodSymbol)
	{
		var stringBuilder = new StringBuilder();
		var className = CreateSetupClassName(stringBuilder, methodSymbol);
		var returnType = methodSymbol.TryGetReturnType();

		var source =
			$$"""
			  public sealed class {{className}} : {{CreateInterfaceDerivedFrom(stringBuilder, methodSymbol, returnType)}}
			  {
			  	private static readonly Comparer SortComparer = new();
			  	private SetupContainer<Item>? _setups;
			  	private Item? _currentSetup;

			  {{CreateDelegates(stringBuilder, methodSymbol, returnType, indent: 1)}}
			  {{CreateInvokeExecuteMethod(stringBuilder, methodSymbol, returnType, indent: 1)}}
			  }
			  """;

		return new MockSetupResult(
			name: className,
			source: source
		);
	}

	private static string CreateInterfaceDerivedFrom(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType)
	{
		string setupThrowsJoin, setupThrowsReset;
		if (returnType is null)
		{
			setupThrowsJoin = "ISetupThrowsJoin";
			setupThrowsReset = "ISetupThrowsReset";
		}
		else
		{
			setupThrowsJoin = "ISetupReturnsThrowsJoin";
			setupThrowsReset = "ISetupReturnsThrowsReset";
		}

		return stringBuilder.Clear()
			.AppendInterface("ISetupCallbackJoin", methodSymbol, returnType)
			.Append(", ").AppendInterface("ISetupCallbackReset", methodSymbol, returnType)
			.Append(", ").AppendInterface(setupThrowsJoin, methodSymbol, returnType)
			.Append(", ").AppendInterface(setupThrowsReset, methodSymbol, returnType)
			.ToString();
	}

	private static string CreateDelegates(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent).Append("public delegate void CallbackDelegate(")
			.AppendParameters(methodSymbol.Parameters)
			.AppendLine(");");

		if (returnType is not null)
		{
			stringBuilder
				.Indent(indent).Append("public delegate TReturns? ReturnsCallbackDelegate(")
				.AppendParameters(methodSymbol.Parameters)
				.AppendLine(");");
		}

		return stringBuilder.ToString();
	}

	private static string CreateInvokeExecuteMethod(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, int indent)
	{
		stringBuilder.Clear();
		stringBuilder.Indent(indent);

		// Method name
		if (returnType is null)
		{
			stringBuilder
				.Append("public void Invoke(")
				.AppendParameters(methodSymbol.Parameters)
				.AppendLine(")");
		}
		else
		{
			stringBuilder
				.Append("public bool Execute(")
				.AppendParameters(methodSymbol.Parameters)
				.AppendLine(", out TReturns? returnValue)");
		}

		stringBuilder
			.Indent(indent++).AppendLine("{");

		// Method body - early return
		stringBuilder
			.Indent(indent).AppendLine("if (_setups is null)")
			.Indent(indent + 1).AppendLine(returnType is null ? "return;" : "goto Default;").AppendLine();

		// Method body - setup check
		stringBuilder
			.Indent(indent).AppendLine("foreach (var setup in _setups)")
			.Indent(indent++).AppendLine("{");

		foreach (var parameter in methodSymbol.Parameters)
		{
			stringBuilder
				.Indent(indent)
				.Append("if (setup.")
				.AppendPropertyName(parameter.Name)
				.Append(".HasValue && !setup.")
				.AppendPropertyName(parameter.Name)
				.Append(".Value.Check(")
				.Append(parameter.Name)
				.AppendLine("))");

			stringBuilder
				.Indent(indent + 1)
				.AppendLine("continue;");
		}

		stringBuilder
			.AppendLine()
			.Indent(indent)
			.AppendLine("var x = setup.GetSetup();");

		stringBuilder
			.Indent(indent)
			.Append("x.Callback?.Invoke(")
			.AppendParameterNames(methodSymbol.Parameters)
			.AppendLine(");").AppendLine();

		stringBuilder
			.Indent(indent)
			.AppendLine("if (x.Exception is not null)")
			.Indent(indent + 1).AppendLine("throw x.Exception;");

		if (returnType is not null)
		{
			stringBuilder
				.AppendLine()
				.Indent(indent)
				.AppendLine("if (x.Returns is not null)")
				.Indent(indent++).AppendLine("{");

			stringBuilder
				.Indent(indent)
				.Append("returnValue = x.Returns(")
				.AppendParameterNames(methodSymbol.Parameters)
				.AppendLine(");");

			stringBuilder
				.Indent(indent)
				.AppendLine("return true;");

			stringBuilder
				.Indent(--indent).AppendLine("}");
		}

		stringBuilder
			.Indent(--indent)
			.AppendLine("}");

		// Method body - return value
		if (returnType is not null)
		{
			stringBuilder
				.AppendLine()
				.Indent(indent).AppendLine("Default:")
				.Indent(indent).AppendLine("returnValue = default;")
				.Indent(indent).AppendLine("return false;");
		}

		return stringBuilder
			.Indent(--indent)
			.Append('}')
			.ToString();
	}

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol)
	{
		stringBuilder.Clear();

		return stringBuilder
			.AppendSetupClassName(methodSymbol, OverrideReturnType)
			.ToString();
	}

	private static void OverrideReturnType(StringBuilder stringBuilder, ITypeSymbol typeSymbol)
	{
		stringBuilder.Append("TReturns");
	}

	extension(StringBuilder @this)
	{
		private StringBuilder AppendInterface(string interfaceName, IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol)
		{
			@this
				.Append(interfaceName)
				.Append('<');

			if (returnTypeSymbol is null)
			{
				@this
					.AppendSetupClassName(methodSymbol, OverrideReturnType)
					.Append(".CallbackDelegate");
			}
			else
			{
				@this
					.AppendSetupClassName(methodSymbol, OverrideReturnType)
					.Append(".CallbackDelegate, TReturns, ")
					.AppendSetupClassName(methodSymbol, OverrideReturnType)
					.Append(".ReturnsCallbackDelegate");
			}

			return @this.Append('>');
		}
	}
}
