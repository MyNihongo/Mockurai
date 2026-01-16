namespace MyNihongo.Mock;

public readonly ref struct It<T>
{
	public readonly ItSetup<T> ValueSetup;

	private It(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	private It(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
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
		return new It<T>(predicate, SetupType.Where, Constants.WhereToString);
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
		return ValueSetup.ToString();
	}
}
