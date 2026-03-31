namespace MyNihongo.Mock.Utils;

internal static class MockClassGenerator
{
	public static string GenerateMockClass(this INamedTypeSymbol classSymbol, List<MockClassDeclaration> mocks)
	{
		var stringBuilder = new StringBuilder();
		const int indent = 1;

		return
			$$"""
			  #nullable enable
			  namespace {{classSymbol.ContainingNamespace}};

			  {{classSymbol.DeclaredAccessibility.GetString()}} partial class {{classSymbol.Name}}
			  {
			  {{CreateProperties(stringBuilder, mocks, indent)}}
			  {{CreateVerifyNoOtherCalls(stringBuilder, mocks, indent)}}
			  {{CreateVerifyInSequence(stringBuilder, mocks, indent)}}
			  {{CreateVerifySequenceContext(stringBuilder, mocks, indent)}}
			  }
			  """;
	}

	private static string CreateProperties(StringBuilder stringBuilder, List<MockClassDeclaration> mocks, int indent)
	{
		stringBuilder.Clear();

		for (int i = 0, lastIndex = mocks.Count - 1; i < mocks.Count; i++)
		{
			var mock = mocks[i];
			if (mock.Property is null || !mock.Property.IsPartialDefinition)
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

		if (stringBuilder.Length == 0)
			return string.Empty;

		return stringBuilder
			.AppendLine()
			.ToString();
	}

	private static string CreateVerifyNoOtherCalls(StringBuilder stringBuilder, List<MockClassDeclaration> mocks, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent).AppendLine("protected void VerifyNoOtherCalls()")
			.Indent(indent++).AppendLine("{");

		foreach (var mock in mocks)
		{
			var symbol = mock.PropertyOrField;

			var skipVerifyNoOtherCalls = symbol.GetAttributeValue(
				MockGeneratorConst.BehavriorAttribute,
				MockGeneratorConst.SkipVerifyNoOtherCallsPropertyName,
				defaultValue: false
			);

			if (skipVerifyNoOtherCalls)
				continue;

			stringBuilder
				.Indent(indent)
				.AppendFieldOrPropertyName(symbol)
				.TryAppendNullableAnnotation(symbol)
				.AppendLine(".VerifyNoOtherCalls();");
		}

		return stringBuilder
			.Indent(--indent).AppendLine("}")
			.ToString();
	}

	private static string CreateVerifyInSequence(StringBuilder stringBuilder, List<MockClassDeclaration> mocks, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent).AppendLine("protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)")
			.Indent(indent++).AppendLine("{")
			.Indent(indent++).AppendLine("var ctx = new VerifySequenceContext(");

		for (int i = 0, lastIndex = mocks.Count - 1; i < mocks.Count; i++)
		{
			var symbol = mocks[i].PropertyOrField;
			var symbolName = symbol?.Name;

			stringBuilder
				.Indent(indent)
				.AppendParameterName(symbolName)
				.Append(": ")
				.AppendFieldOrPropertyName(symbol);

			if (i < lastIndex)
				stringBuilder.Append(',');

			stringBuilder.AppendLine();
		}

		return stringBuilder
			.Indent(--indent).AppendLine(");").AppendLine()
			.Indent(indent).AppendLine("verify(ctx);")
			.Indent(--indent).AppendLine("}")
			.ToString();
	}

	private static string CreateVerifySequenceContext(StringBuilder stringBuilder, List<MockClassDeclaration> mocks, int indent)
	{
		stringBuilder.Clear();

		stringBuilder
			.Indent(indent).AppendLine("protected sealed class VerifySequenceContext")
			.Indent(indent++).AppendLine("{")
			.Indent(indent).AppendLine("private readonly VerifyIndex _verifyIndex = new();");

		// Generate properties
		foreach (var mock in mocks)
		{
			var symbol = mock.PropertyOrField;
			var symbolName = symbol?.Name;

			stringBuilder
				.Indent(indent)
				.Append("public readonly IMockSequence<")
				.Append(mock.Type)
				.Append('>')
				.TryAppendNullableAnnotation(symbol)
				.Append(' ')
				.AppendPropertyName(symbolName)
				.AppendLine(";");
		}

		// Generate constructor
		stringBuilder
			.AppendLine()
			.Indent(indent).Append("public VerifySequenceContext(");

		// Constructor - parameters
		for (int i = 0, lastIndex = mocks.Count - 1; i < mocks.Count; i++)
		{
			var mock = mocks[i];

			if (mock.Property is not null)
			{
				stringBuilder
					.Append(mock.Property.Type)
					.Append(' ')
					.AppendParameterName(mock.Property.Name);
			}
			else if (mock.Field is not null)
			{
				stringBuilder
					.Append(mock.Field.Type)
					.Append(' ')
					.AppendParameterName(mock.Field.Name);
			}

			if (i < lastIndex)
				stringBuilder.Append(", ");
		}

		stringBuilder
			.AppendLine(")")
			.Indent(indent++)
			.AppendLine("{");

		// Constructor - initialize properties
		foreach (var mock in mocks)
		{
			var symbol = mock.PropertyOrField;
			var symbolName = symbol?.Name;
			var nullableAnnotation = symbol.GetNullableAnnotation();

			if (nullableAnnotation == NullableAnnotation.Annotated)
			{
				stringBuilder
					.Indent(indent)
					.Append("if (")
					.AppendParameterName(symbolName)
					.AppendLine(" is not null)");

				stringBuilder
					.Indent(indent)
					.AppendLine("{");

				indent++;
			}

			stringBuilder
				.Indent(indent)
				.AppendPropertyName(symbolName)
				.Append(" = new MockSequence<")
				.Append(mock.Type)
				.AppendLine(">");

			stringBuilder
				.Indent(indent++)
				.AppendLine("{");

			stringBuilder
				.Indent(indent)
				.AppendLine("VerifyIndex = _verifyIndex,")
				.Indent(indent)
				.Append("Mock = ").AppendParameterName(symbolName)
				.AppendLine(",");

			stringBuilder
				.Indent(--indent)
				.AppendLine("};");

			if (nullableAnnotation == NullableAnnotation.Annotated)
			{
				stringBuilder
					.Indent(--indent)
					.AppendLine("}");
			}
		}

		stringBuilder
			.Indent(--indent)
			.AppendLine("}");

		return stringBuilder
			.Indent(--indent).Append('}')
			.ToString();
	}
}
