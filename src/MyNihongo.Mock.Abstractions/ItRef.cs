namespace MyNihongo.Mock;

public readonly ref struct ItRef<T>
{
	private readonly It<T> _it;

	private ItRef(It<T> it)
	{
		_it = it;
	}

	public static ItRef<T> Value(in T value)
	{
		return new ItRef<T>(It<T>.Value(value));
	}

	public static ItRef<T> Any()
	{
		return new ItRef<T>();
	}

	public static implicit operator It<T>(ItRef<T> itRef)
	{
		return itRef._it;
	}

	public override string ToString()
	{
		return $"ref {_it.ToString()}";
	}
}
