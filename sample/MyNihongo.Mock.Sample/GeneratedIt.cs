namespace MyNihongo.Mock.Sample;

public readonly ref struct It<T> : IEquatable<It<T>>
{
	private readonly bool _hasValue;
	private readonly T? _value;

	private It(T value)
	{
		_value = value;
		_hasValue = true;
	}

	public static It<T> Value(in T value)
	{
		return new It<T>(value);
	}

	public static It<T> Any()
	{
		return new It<T>();
	}

	public static implicit operator It<T>(in T value)
	{
		return Value(value);
	}

	public bool Equals(It<T> other)
	{
		var otherHashCode = other.GetHashCode();
		return GetHashCode() == otherHashCode;
	}

	public override int GetHashCode()
	{
		return _hasValue
			? _value!.GetHashCode()
			: 0;
	}
}
