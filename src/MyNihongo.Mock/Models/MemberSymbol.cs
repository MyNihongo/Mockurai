namespace MyNihongo.Mock.Models;

internal sealed class MemberSymbol(string name, ISymbol symbol)
{
	public readonly string MemberName = name;
	public readonly ISymbol Symbol = symbol;
}
