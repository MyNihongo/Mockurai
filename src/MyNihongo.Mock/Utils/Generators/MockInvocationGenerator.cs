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
			  {{CreateRegister(stringBuilder, methodSymbol, indent: 1)}}
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

	private static string CreateRegister(StringBuilder stringBuilder, IMethodSymbol methodSymbol, int indent)
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

	private static string CreateSetupClassName(StringBuilder stringBuilder, IMethodSymbol methodSymbol)
	{
		stringBuilder.Clear();

		return stringBuilder
			.AppendInvocationClassName(methodSymbol.Parameters)
			.ToString();
	}
}
