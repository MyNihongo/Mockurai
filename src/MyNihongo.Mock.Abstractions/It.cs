namespace MyNihongo.Mock;

public readonly ref struct It<T>
{
	public readonly ItSetup<T>? ValueSetup;
	private readonly Func<string>? _toString;

	internal It(Func<string> toString)
	{
		_toString = toString;
	}

	internal It(ItSetup<T>? valueSetup, Func<string>? toString)
	{
		ValueSetup = valueSetup;
		_toString = toString;
	}

	private It(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		_toString = toString;
		ValueSetup = new ItSetup<T>(predicate, type);
	}

	private It(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString)
	{
		_toString = toString;
		ValueSetup = new ItSetup<T>(predicate, type);
	}

	public static It<T> Value(T value)
	{
		return new It<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.ToJsonString());
	}

	public static It<T> Equivalent(T value)
	{
		return new It<T>(x => EquivalencyComparer<T>.Default.Equivalent(value, x), SetupType.Equivalent, () => value.ToJsonString());
	}

	public static It<T> Where(in Func<T, bool> predicate)
	{
		return new It<T>(predicate, SetupType.Where, static () => "where(predicate)");
	}

	public static It<T> Any()
	{
		return new It<T>();
	}

	public static implicit operator It<T>(in T value)
	{
		return Value(value);
	}

	public override string ToString()
	{
		return _toString?.Invoke() ?? "any";
	}
}
