namespace MyNihongo.Mockurai.Utils;

internal static class SymbolEx
{
	extension(ISymbol @this)
	{
		public bool IsPublic => @this.DeclaredAccessibility == Accessibility.Public;
	}

	extension<T>(T @this)
		where T : ISymbol
	{
		public string GetSymbolName(Func<T, string>? defaultFunc = null)
		{
			return @this switch
			{
				IPropertySymbol { IsIndexer: true } => MockGeneratorConst.Suffixes.Indexer,
				_ => defaultFunc?.Invoke(@this) ?? @this.Name,
			};
		}
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

		public NullableAnnotation GetNullableAnnotation()
		{
			return @this switch
			{
				IPropertySymbol x => x.Type.NullableAnnotation,
				IFieldSymbol x => x.Type.NullableAnnotation,
				_ => NullableAnnotation.NotAnnotated,
			};
		}
	}

	extension(IEnumerable<ISymbol> @this)
	{
		public IEnumerable<ISymbol> FilterMockableSymbols()
		{
			return @this.Where(static x => x.Kind switch
			{
				SymbolKind.Event => true,
				SymbolKind.Property => true,
				SymbolKind.Method => x is IMethodSymbol { MethodKind: MethodKind.Ordinary },
				_ => false,
			});
		}
	}
}
