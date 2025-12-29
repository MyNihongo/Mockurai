namespace MyNihongo.Mock;

public readonly ref struct ItIn<T>
{
	private readonly ItSetup<T>? _valueSetup;
	private readonly Func<string>? _toString;

	internal ItIn(Func<string> toString)
	{
		_toString = toString;
	}

	private ItIn(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		_toString = toString;
		_valueSetup = new ItSetup<T>(predicate, type);
	}

	private ItIn(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString)
	{
		_toString = toString;
		_valueSetup = new ItSetup<T>(predicate, type);
	}

	public static ItIn<T> Value(T value)
	{
		return new ItIn<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.ToJsonString());
	}

	public static ItIn<T> Equivalent(T value)
	{
		return new ItIn<T>(x => EquivalencyComparer<T>.Default.Equivalent(value, x), SetupType.Equivalent, () => value.ToJsonString());
	}

	public static ItIn<T> Where(in Func<T, bool> predicate)
	{
		return new ItIn<T>(predicate, SetupType.Where, static () => "where(predicate)");
	}

	public static ItIn<T> Any()
	{
		return new ItIn<T>();
	}

	public static implicit operator ItIn<T>(in T value)
	{
		return Value(value);
	}

	public static implicit operator It<T>(in ItIn<T> itIn)
	{
		return new It<T>(itIn._valueSetup, itIn._toString);
	}

	public override string ToString()
	{
		return _toString?.Invoke() ?? "any";
	}
}
