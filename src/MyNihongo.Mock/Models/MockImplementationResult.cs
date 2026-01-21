namespace MyNihongo.Mock.Models;

internal readonly ref struct MockImplementationResult(string name, string source, IReadOnlyList<IMethodSymbol>? methodSymbols)
{
	public readonly string Name = name, Source = source;
	public readonly IReadOnlyList<IMethodSymbol>? MethodSymbols = methodSymbols;
}
