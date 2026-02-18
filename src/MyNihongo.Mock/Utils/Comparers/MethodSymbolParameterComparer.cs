namespace MyNihongo.Mock.Utils;

internal sealed class MethodSymbolParameterComparer : IEqualityComparer<IMethodSymbol>
{
	public static readonly MethodSymbolParameterComparer Default = new();

	public bool Equals(IMethodSymbol? x, IMethodSymbol? y)
	{
		if (x is null)
			return y is null;
		if (y is null)
			return false;
		if (x.Parameters.Length != y.Parameters.Length)
			return false;

		for (var i = 0; i < x.Parameters.Length; i++)
		{
			var xHashCode = GetHashCode(x);
			var yHashCode = GetHashCode(y);

			if (xHashCode != yHashCode)
				return false;
		}

		return true;
	}

	public int GetHashCode(IMethodSymbol? obj)
	{
		if (obj is null)
			return 0;

		unchecked
		{
			var hash = 17;
			hash = hash * 23 + obj.Parameters.Length;

			var symbolComparer = SymbolEqualityComparer.Default;
			foreach (var parameter in obj.Parameters)
			{
				var typeHashCode = symbolComparer.GetHashCode(parameter.Type);
				var refHashCode = parameter.RefKind.GetHashCode();

				var parameterHash = 17;
				parameterHash = parameterHash * 23 + typeHashCode;
				parameterHash = parameterHash * 23 + refHashCode;

				hash = hash * 23 + parameterHash;
			}

			return hash;
		}
	}
}
