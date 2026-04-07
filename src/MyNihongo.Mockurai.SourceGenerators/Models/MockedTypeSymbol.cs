namespace MyNihongo.Mockurai.Models;

internal sealed class MockedTypeSymbol(ITypeSymbol typeSymbol)
{
	public readonly ITypeSymbol TypeSymbol = typeSymbol;
	
	/// <summary>
	/// Even though class generic types differ from method generic types, compiler does not prevent it.
	/// Therefore, we want to exclude repetitive types if they are present.
	/// </summary>
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
