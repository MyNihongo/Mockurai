namespace MyNihongo.Mock;

public readonly ref struct ItRefReadOnly<T>
{
	private readonly It<T> _it;

	private ItRefReadOnly(It<T> it)
	{
		_it = it;
	}

	public static ItRefReadOnly<T> Value(in T value)
	{
		return new ItRefReadOnly<T>(It<T>.Value(value));
	}

	public static ItRef<T> Any()
	{
		return new ItRef<T>();
	}

	public static implicit operator It<T>(ItRefReadOnly<T> itRef)
	{
		return itRef._it;
	}

	public override string ToString()
	{
		return $"ref {_it.ToString()}";
	}
}
