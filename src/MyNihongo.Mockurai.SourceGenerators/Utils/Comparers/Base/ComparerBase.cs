namespace MyNihongo.Mockurai.Utils;

internal abstract class ComparerBase<T> : IEqualityComparer<T>
{
	public bool Equals(T? x, T? y)
	{
		if (x is null)
			return y is null;
		if (y is null)
			return false;

		var xHashCode = GetHashCode(x);
		var yHashCode = GetHashCode(y);
		return xHashCode == yHashCode;
	}

	public int GetHashCode(T? obj)
	{
		return obj is not null
			? GetHashCodeProtected(obj)
			: 0;
	}

	protected abstract int GetHashCodeProtected(T obj);

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
