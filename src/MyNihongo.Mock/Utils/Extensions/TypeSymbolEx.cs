using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class TypeSymbolEx
{
	extension(ISymbol? typeSymbol)
	{
		public T? GetAttributeValue<T>(string attributeName, string propertyName, T defaultValue)
		{
			if (typeSymbol is null)
				return defaultValue;

			foreach (var attribute in typeSymbol.GetAttributes())
			{
				if (attribute.AttributeClass?.Name != attributeName)
					continue;

				foreach (var namedArgument in attribute.NamedArguments)
				{
					if (namedArgument.Key != propertyName)
						continue;

					return (T?)namedArgument.Value.Value;
				}
			}

			return defaultValue;
		}
	}

	extension(StringBuilder @this)
	{
		public StringBuilder AppendMockClassName(ITypeSymbol typeSymbol)
		{
			var name = typeSymbol.Name;
			if (name.Length > 0 && name[0] == 'I')
				@this.Append(name.Substring(1));
			else
				@this.Append(name);

			@this.Append("Mock");

			if (typeSymbol is INamedTypeSymbol { TypeArguments.Length: > 0 } namedTypeSymbol)
			{
				@this.Append('<');

				foreach (var typeArgument in namedTypeSymbol.TypeArguments)
					@this.Append(typeArgument);

				@this.Append('>');
			}

			return @this;
		}
	}
}
