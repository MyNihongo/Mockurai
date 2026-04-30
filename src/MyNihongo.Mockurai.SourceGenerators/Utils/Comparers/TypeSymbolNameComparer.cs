namespace MyNihongo.Mockurai.Utils;

internal sealed class TypeSymbolNameComparer : ComparerBase<ITypeSymbol>
{
	public static readonly TypeSymbolNameComparer Default = new();

	protected override int GetHashCodeProtected(ITypeSymbol obj)
	{
		return obj
			.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
			.GetHashCode();
	}
}
