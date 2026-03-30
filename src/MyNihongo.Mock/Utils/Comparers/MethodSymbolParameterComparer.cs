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

		var xHashCode = GetHashCode(x);
		var yHashCode = GetHashCode(y);
		return xHashCode == yHashCode;
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
				var typeHashCode = symbolComparer.GetParameterHashCode(parameter);
				var refHashCode = parameter.RefKind.GetHashCode();

				var parameterHash = 17;
				parameterHash = parameterHash * 23 + typeHashCode;
				parameterHash = parameterHash * 23 + refHashCode;

				hash = hash * 23 + parameterHash;
			}
			
			// TODO: get return parameter as well
			aaaa

			return hash;
		}
	}
}

file static class Extensions
{
	public static int GetParameterHashCode(this SymbolEqualityComparer @this, IParameterSymbol parameter)
	{
		return parameter.Type switch
		{
			ITypeParameterSymbol x => x.ConstraintTypes.GetHashCode(),
			_ => @this.GetHashCode(parameter.Type),
		};
	}
}
