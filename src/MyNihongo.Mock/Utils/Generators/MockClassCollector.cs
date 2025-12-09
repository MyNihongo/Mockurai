namespace MyNihongo.Mock.Utils;

internal static class MockClassCollector
{
	public static HashSet<ITypeSymbol>? CollectMocks(this INamedTypeSymbol? @this)
	{
		if (@this is null)
			return null;

		var output = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
		foreach (var member in @this.GetMembers())
		{
			var mockedType = member switch
			{
				IFieldSymbol x => x.TryGetMockedType(),
				IPropertySymbol x => x.TryGetMockedType(),
				_ => null,
			};

			if (mockedType is not null)
				output.Add(mockedType);
		}

		return output;
	}

	private static ITypeSymbol? TryGetMockedType(this IFieldSymbol @this)
	{
		// Here we skip backing fields for auto properties
		return !@this.IsImplicitlyDeclared
			? @this.Type.TryGetMockedType()
			: null;
	}

	private static ITypeSymbol? TryGetMockedType(this IPropertySymbol @this)
	{
		return !@this.IsPartialDefinition
			? @this.Type.TryGetMockedType()
			: null;
	}

	private static ITypeSymbol? TryGetMockedType(this ITypeSymbol @this)
	{
		if (@this.Name == "IMock" && @this is INamedTypeSymbol { TypeArguments.Length: 1 } namedType)
			return namedType.TypeArguments[0];

		return null;
	}
}
