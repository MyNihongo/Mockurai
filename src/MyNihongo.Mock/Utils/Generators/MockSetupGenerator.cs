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

	private static string CreateInvokeExecuteMethod(StringBuilder stringBuilder, IMethodSymbol methodSymbol, ITypeSymbol? returnType, int indent)
	{
		stringBuilder.Clear();
		stringBuilder.Indent(indent);

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

		return stringBuilder
			.Indent(--indent)
			.Append('}')
			.ToString();
	}

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol)
	{
		stringBuilder.Clear();

		return stringBuilder
			.AppendSetupClassName(methodSymbol, static (sb, _) => sb.Append("TReturns"))
			.ToString();
	}
}

file static class Extensions
{
	extension(StringBuilder @this)
	{
		public StringBuilder AppendInterface(string interfaceName, IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol)
		{
			@this
				.Append(interfaceName)
				.Append('<');

			if (returnTypeSymbol is null)
			{
				@this
					.Append("Action<")
					.AppendParameterTypes(methodSymbol.Parameters)
					.Append('>');
			}
			else
			{
				@this
					.Append("Action<")
					.AppendParameterTypes(methodSymbol.Parameters)
					.Append(">, TReturns, ");

				@this
					.Append("Func<")
					.AppendParameterTypes(methodSymbol.Parameters)
					.Append(", TReturns?>");
			}

			return @this.Append('>');
		}

		private StringBuilder AppendParameterTypes(ImmutableArray<IParameterSymbol> parameters)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				@this.AppendType(parameters[i].Type);
			}

			return @this;
		}
	}
}
