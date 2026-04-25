namespace MyNihongo.Mockurai;

public readonly ref struct ItIn<T>
{
	public readonly ItSetup<T> ValueSetup;

	private ItIn(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	private ItIn(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	public static ItIn<T> Value(T value)
	{
		return new ItIn<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.ToJsonString());
	}

	public static ItIn<T> Equivalent(T value, IEquivalencyComparer<T>? comparer = null)
	{
		return new ItIn<T>(x => (comparer ?? EquivalencyComparer<T>.Default).Equivalent(value, x), SetupType.Equivalent, () => value.ToJsonString());
	}

	public static ItIn<T> Where(in Func<T, bool> predicate)
	{
		return new ItIn<T>(predicate, SetupType.Where, Constants.WhereToString);
	}

	public static ItIn<T> Any()
	{
		return new ItIn<T>();
	}

	public override string ToString()
	{
		return ValueSetup.ToString();
	}
}
