namespace MyNihongo.Mockurai.Utils;

internal abstract class MethodSymbolComparerBase : ComparerBase<IMethodSymbol>
{
	protected override int GetHashCodeProtected(IMethodSymbol obj)
	{
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
			// For generic types we care only about the count (not actual content)
			ITypeParameterSymbol x => x.ConstraintTypes.GetHashCode(),
			_ => @this.GetHashCode(parameter.Type),
		};
	}

	protected static int GetIsNullableHashCode(IParameterSymbol parameter)
	{
		var isNullable = parameter.Type.NullableAnnotation == NullableAnnotation.Annotated;
		return isNullable.GetHashCode();
	}
}
