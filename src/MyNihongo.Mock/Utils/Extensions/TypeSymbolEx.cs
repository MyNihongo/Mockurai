using System.Text;

namespace MyNihongo.Mock.Utils;

internal static class TypeSymbolEx
{
	extension(ISymbol @this)
	{
		public bool IsPublic => @this.DeclaredAccessibility == Accessibility.Public;
	}

	extension(ISymbol? @this)
	{
		public T? GetAttributeValue<T>(string attributeName, string propertyName, T defaultValue)
		{
			if (@this is null)
				return defaultValue;

			foreach (var attribute in @this.GetAttributes())
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

	extension(ITypeSymbol @this)
	{
		public IEnumerable<ISymbol> GetOverridableMembers()
		{
			return @this.GetMembers()
				.Where(static x => x.IsPublic && x is { IsStatic: false, IsSealed: false } && (x.IsOverride || x.IsVirtual || x.IsAbstract));
		}
	}

	extension(StringBuilder @this)
	{
		public StringBuilder AppendMockClassName(ITypeSymbol typeSymbol, bool appendGenericTypes = true)
		{
			var name = typeSymbol.Name;
			if (name.Length > 0 && name[0] == 'I')
				@this.Append(name.Substring(1));
			else
				@this.Append(name);

			@this.Append("Mock");

			return appendGenericTypes
				? @this.AppendGenericTypes(typeSymbol)
				: @this;
		}

		public StringBuilder AppendGenericTypes(ITypeSymbol typeSymbol)
		{
			if (typeSymbol is INamedTypeSymbol { TypeArguments.Length: > 0 } namedTypeSymbol)
			{
				@this.Append('<');

				foreach (var typeArgument in namedTypeSymbol.TypeArguments)
					@this.Append(typeArgument);

				@this.Append('>');
			}

			return @this;
		}

		public StringBuilder AppendParameter(IParameterSymbol? parameter)
		{
			return parameter is not null
				? @this.Append(parameter)
				: @this;
		}

		public StringBuilder TryAppendOverride(ISymbol symbol)
		{
			return symbol.ContainingType.TypeKind == TypeKind.Class && (symbol.IsAbstract || symbol.IsVirtual)
				? @this.Append("override ")
				: @this;
		}
	}
}
