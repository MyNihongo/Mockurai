namespace MyNihongo.Mockurai.Utils;

internal sealed class MethodSymbolInvocationComparer : MethodSymbolComparerBase
{
	public static readonly MethodSymbolInvocationComparer Default = new();

	protected override int GetHashCode(IMethodSymbol obj, HashCode hash, SymbolEqualityComparer symbolComparer)
	{
		foreach (var parameter in obj.Parameters)
		{
			var typeHashCode = GetParameterHashCode(symbolComparer, parameter);
			var nullableHashCode = GetIsNullableHashCode(parameter);

			var parameterHash = new HashCode();
			parameterHash.Append(typeHashCode);
			parameterHash.Append(nullableHashCode);
			hash.Append(parameterHash);
		}

		return hash.GetHashCode();
	}
}
