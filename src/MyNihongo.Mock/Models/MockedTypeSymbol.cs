namespace MyNihongo.Mock.Models;

internal sealed class MockedTypeSymbol(ITypeSymbol typeSymbol)
{
	public readonly ITypeSymbol TypeSymbol = typeSymbol;
	public readonly ImmutableHashSet<string>? GenericTypeParameterNames = GetGenericTypeParameterNames(typeSymbol);

	private static ImmutableHashSet<string>? GetGenericTypeParameterNames(ITypeSymbol typeSymbol)
	{
		if (typeSymbol is not INamedTypeSymbol { TypeArguments.Length: > 0 } namedTypeSymbol)
			return null;

		return namedTypeSymbol.TypeArguments
			.Select(static x => x.Name)
			.ToImmutableHashSet();
	}
}
