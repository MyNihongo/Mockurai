namespace MyNihongo.Mockurai.Utils;

internal static class SymbolEx
{
	extension(ISymbol @this)
	{
		public bool IsPublic => @this.DeclaredAccessibility == Accessibility.Public;

		public bool CanOverride => @this is { IsStatic: false, IsSealed: false } && (@this.IsOverride || @this.IsVirtual || @this.IsAbstract);

		public bool TryGetIndexerProperty(out IPropertySymbol indexerProperty)
		{
			if (@this is IPropertySymbol { IsIndexer: true } x)
			{
				indexerProperty = x;
				return true;
			}

			indexerProperty = null!;
			return false;
		}
	}

	extension<T>(T @this)
		where T : ISymbol
	{
		public string GetSymbolName()
		{
			return @this.TryGetIndexerProperty(out _)
				? MockGeneratorConst.Suffixes.Indexer
				: @this.Name;
		}
	}

	extension(ISymbol? @this)
	{
		public T? GetAttributeValue<T>(Func<string?, bool> attributePredicate, string propertyName, T defaultValue)
		{
			if (@this is null)
				return defaultValue;

			foreach (var attribute in @this.GetAttributes())
			{
				if (!attributePredicate(attribute.AttributeClass?.Name))
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

		public T? GetAttributeValue<T>(Func<string?, bool> attributePredicate, int index, T defaultValue)
		{
			if (@this is null)
				return defaultValue;

			foreach (var attribute in @this.GetAttributes())
			{
				if (!attributePredicate(attribute.AttributeClass?.Name))
					continue;

				return attribute.ConstructorArguments.Length > index
					? (T?)attribute.ConstructorArguments[index].Value
					: defaultValue;
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
