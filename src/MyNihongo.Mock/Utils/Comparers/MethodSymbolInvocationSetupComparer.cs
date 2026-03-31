namespace MyNihongo.Mock.Utils;

internal sealed class MethodSymbolInvocationSetupComparer : MethodSymbolComparerBase
{
	public static readonly MethodSymbolInvocationSetupComparer Default = new();

	protected override int GetHashCode(IMethodSymbol obj, HashCode hash, SymbolEqualityComparer symbolComparer)
	{
		foreach (var parameter in obj.Parameters)
		{
			var typeHashCode = GetParameterHashCode(symbolComparer, parameter);

			var parameterHash = new HashCode();
			parameterHash.Append(typeHashCode);
			hash.Append(parameterHash);
		}

		return hash.GetHashCode();
	}
}
