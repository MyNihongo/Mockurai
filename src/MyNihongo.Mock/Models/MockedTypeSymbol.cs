namespace MyNihongo.Mock.Models;

internal sealed class MockedTypeSymbol(ITypeSymbol typeSymbol)
{
	public readonly ITypeSymbol TypeSymbol = typeSymbol;
	public readonly ISet<string>? GenericTypeParameterNames = GetGenericTypeParameterNames(typeSymbol);

	private static ISet<string>? GetGenericTypeParameterNames(ITypeSymbol typeSymbol)
	{
		return null;
	}
}
