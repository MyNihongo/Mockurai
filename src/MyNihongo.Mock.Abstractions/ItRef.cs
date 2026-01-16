namespace MyNihongo.Mock;

public readonly ref struct ItRef<T>
{
	public readonly ItSetup<T> ValueSetup;

	private ItRef(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	public static ItRef<T> Value(T value)
	{
		return new ItRef<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.ToJsonString());
	}

	public static ItRef<T> Where(in Func<T, bool> predicate)
	{
		return new ItRef<T>(predicate, SetupType.Where, Constants.WhereToString);
	}

	public static ItRef<T> Any()
	{
		return new ItRef<T>();
	}

	public override string ToString()
	{
		return ValueSetup.ToString();
	}
}
