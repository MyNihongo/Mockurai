namespace MyNihongo.Mock.Utils;

internal abstract class MethodSymbolComparerBase : IEqualityComparer<IMethodSymbol>
{
	public bool Equals(IMethodSymbol? x, IMethodSymbol? y)
	{
		if (x is null)
			return y is null;
		if (y is null)
			return false;
		if (x.Parameters.Length != y.Parameters.Length)
			return false;

		var xHashCode = GetHashCode(x);
		var yHashCode = GetHashCode(y);
		return xHashCode == yHashCode;
	}

	public int GetHashCode(IMethodSymbol? obj)
	{
		if (obj is null)
			return 0;

		var hash = new HashCode();
		hash.Append(obj.Parameters.Length);

		var symbolComparer = SymbolEqualityComparer.Default;
		return GetHashCode(obj, hash, symbolComparer);
	}

	protected abstract int GetHashCode(IMethodSymbol obj, HashCode hash, SymbolEqualityComparer symbolComparer);

	protected static int GetParameterHashCode(SymbolEqualityComparer @this, IParameterSymbol parameter)
	{
		return parameter.Type switch
		{
			ITypeParameterSymbol x => GetParameterHashCode(@this, x.ConstraintTypes),
			_ => @this.GetHashCode(parameter.Type),
		};
	}

	private static int GetParameterHashCode(SymbolEqualityComparer @this, ImmutableArray<ITypeSymbol> constraintTypes)
	{
		var hash = new HashCode();

		foreach (var constraint in constraintTypes)
		{
			var typeHashCode = @this.GetHashCode(constraint);
			hash.Append(typeHashCode);
		}

		return hash.GetHashCode();
	}

	protected ref struct HashCode()
	{
		private int _hash = 17;

		public void Append(HashCode hash) =>
			Append(hash._hash);

		public void Append(int value) =>
			_hash = unchecked(_hash * 23 + value);

		public override int GetHashCode() =>
			_hash;
	}
}
