namespace MyNihongo.Mock.Utils;

internal static class MockClassCollector
{
	public static List<MockClassDeclaration> CollectMocks(this INamedTypeSymbol @this)
	{
		var output = new List<MockClassDeclaration>();
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

	private static MockClassDeclaration? TryGetMockedType(this IFieldSymbol @this)
	{
		// Here we skip backing fields for auto properties
		var type = !@this.IsImplicitlyDeclared
			? @this.Type.TryGetMockedType()
			: null;

		return type is not null
			? new MockClassDeclaration(type, @this)
			: null;
	}

	private static MockClassDeclaration? TryGetMockedType(this IPropertySymbol @this)
	{
		var type = @this.Type.TryGetMockedType();

		return type is not null
			? new MockClassDeclaration(type, @this)
			: null;
	}

	private static ITypeSymbol? TryGetMockedType(this ITypeSymbol @this)
	{
		if (@this.Name == "IMock" && @this is INamedTypeSymbol { TypeArguments.Length: 1 } namedType)
			return namedType.TypeArguments[0];

		return null;
	}
}
