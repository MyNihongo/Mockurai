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
			  }
			  """;
	}

	private static string CreateProperties(StringBuilder stringBuilder, List<MockClassDeclaration> mocks)
	{
		const int indent = 1;
		stringBuilder.Clear();

		foreach (var mock in mocks)
		{
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
				.Append(';').AppendLine().AppendLine();
		}

		return stringBuilder.ToString();
	}
}
