namespace MyNihongo.Mockurai.Utils;

internal sealed class MethodSymbolSetupComparer : MethodSymbolComparerBase
{
	public static readonly MethodSymbolSetupComparer Default = new();

	protected override int GetHashCode(IMethodSymbol obj, HashCode hash, SymbolEqualityComparer symbolComparer)
	{
		foreach (var parameter in obj.Parameters)
		{
			var typeHashCode = GetParameterHashCode(symbolComparer, parameter);
			var refHashCode = parameter.RefKind.GetHashCode();
			var nullableHashCode = GetIsNullableHashCode(parameter);

			var parameterHash = new HashCode();
			parameterHash.Append(typeHashCode);
			parameterHash.Append(refHashCode);
			parameterHash.Append(nullableHashCode);
			hash.Append(parameterHash);
		}

		var returnType = obj.TryGetReturnType();
		if (returnType is not null)
		{
			// When we generate the setup we only care about whether the return type exists or not
			// Setup has the generic return type
			var typeHashCode = true.GetHashCode();
			hash.Append(typeHashCode);
		}

		return hash.GetHashCode();
	}
}
