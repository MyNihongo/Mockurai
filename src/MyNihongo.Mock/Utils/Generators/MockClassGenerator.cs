using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class MockClassGenerator
{
	public static string GenerateMockClass(this CompilationCombinedResult @this, INamedTypeSymbol classSymbol, List<MockClassDeclaration> mocks)
	{
		var stringBuilder = new StringBuilder();

		return
			$$"""
			  namespace {{classSymbol.ContainingNamespace}};

			  {{classSymbol.DeclaredAccessibility.GetString()}} partial class {{classSymbol.Name}}
			  {
			  {{CreateProperties(stringBuilder, mocks)}}

			  {{CreateVerifyInSequence(stringBuilder, mocks)}}
			  }
			  """;
	}

	private static string CreateProperties(StringBuilder stringBuilder, List<MockClassDeclaration> mocks)
	{
		const int indent = 1;
		stringBuilder.Clear();

		for (int i = 0, lastIndex = mocks.Count - 1; i < mocks.Count; i++)
		{
			var mock = mocks[i];
			if (mock.Property is null)
				continue;

			var propertyName = mock.Property.Name;

			stringBuilder
				.Indent(indent)
				.Append("// ")
				.Append(propertyName)
				.AppendLine();

			stringBuilder
				.Indent(indent)
				.Append("private readonly ")
				.AppendMockClassName(mock.Type)
				.Append(' ')
				.AppendFieldName(propertyName)
				.AppendLine(" = new(InvocationIndex.CounterValue);");

			stringBuilder
				.Indent(indent)
				.Append(mock.Property.DeclaredAccessibility.GetString())
				.Append(" partial ")
				.Append(mock.Property.Type)
				.Append(' ')
				.Append(propertyName)
				.Append(" => ")
				.AppendFieldName(propertyName)
				.Append(';');

			if (i < lastIndex)
				stringBuilder.AppendLine().AppendLine();
		}

		return stringBuilder.ToString();
	}

	private static string CreateVerifyInSequence(StringBuilder stringBuilder, List<MockClassDeclaration> mocks)
	{
		stringBuilder.Clear();
		var indent = 1;

		stringBuilder
			.Indent(indent).AppendLine("protected void VerifyInSequence(Action<VerifySequenceContext> verify)")
			.Indent(indent++).AppendLine("{")
			.Indent(indent++).AppendLine("var ctx = new VerifySequenceContext(");

		for (int i = 0, lastIndex = mocks.Count - 1; i < mocks.Count; i++)
		{
			var mock = mocks[i];
			if (mock.Property is null)
				continue;

			var propertyName = mock.Property.Name;

			stringBuilder
				.Indent(indent)
				.AppendParameterName(propertyName)
				.Append(": ")
				.AppendFieldName(propertyName);

			if (i < lastIndex)
				stringBuilder.Append(',');

			stringBuilder.AppendLine();
		}

		return stringBuilder.AppendLine()
			.Indent(--indent).AppendLine("verify(ctx);")
			.Indent(--indent).AppendLine("}")
			.ToString();
	}
}
