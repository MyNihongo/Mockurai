using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class TypeSymbolEx
{
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
