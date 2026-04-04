namespace MyNihongo.Mock.Utils;

internal sealed class MethodSymbolSetupComparer : MethodSymbolComparerBase
{
	public static readonly MethodSymbolSetupComparer Default = new();

	protected override int GetHashCode(IMethodSymbol obj, HashCode hash, SymbolEqualityComparer symbolComparer)
	{
		foreach (var parameter in obj.Parameters)
		{
			var typeHashCode = GetParameterHashCode(symbolComparer, parameter);
			var refHashCode = parameter.RefKind.GetHashCode();

			var parameterHash = new HashCode();
			parameterHash.Append(typeHashCode);
			parameterHash.Append(refHashCode);
			hash.Append(parameterHash);
		}

		var returnType = obj.TryGetReturnType();
		if (returnType is not null)
		{
			var typeHashCode = symbolComparer.GetHashCode(returnType);
			hash.Append(typeHashCode);
		}

		return hash.GetHashCode();
	}
}
