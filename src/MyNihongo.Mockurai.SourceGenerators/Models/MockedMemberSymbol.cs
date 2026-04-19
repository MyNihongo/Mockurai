namespace MyNihongo.Mockurai.Models;

internal sealed class MockedMemberSymbol(string name, int index, ISymbol symbol)
{
	public readonly string MemberName = name;
	public readonly int Index = index;
	public readonly ISymbol Symbol = symbol;
}
