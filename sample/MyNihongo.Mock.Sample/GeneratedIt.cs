namespace MyNihongo.Mock.Sample;

public readonly ref struct It<T>
{
	public readonly Func<T, bool>? Predicate;

	private It(Func<T, bool> predicate)
	{
		Predicate = predicate;
	}

	public static It<T> Value(T value)
	{
		return new It<T>(x => EqualityComparer<T>.Default.Equals(x, value));
	}

	public static It<T> Where(in Func<T, bool> predicate)
	{
		return new It<T>(predicate);
	}

	public static It<T> Any()
	{
		return new It<T>();
	}

	public static implicit operator It<T>(in T value)
	{
		return Value(value);
	}
}
