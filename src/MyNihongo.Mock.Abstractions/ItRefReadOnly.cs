namespace MyNihongo.Mock;

public readonly ref struct ItRefReadOnly<T>
{
	public readonly ItSetup<T> ValueSetup;

	private ItRefReadOnly(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	public static ItRefReadOnly<T> Value(T value)
	{
		return new ItRefReadOnly<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.ToJsonString());
	}

	public static ItRefReadOnly<T> Where(in Func<T, bool> predicate)
	{
		return new ItRefReadOnly<T>(predicate, SetupType.Where, Constants.WhereToString);
	}

	public static ItRefReadOnly<T> Any()
	{
		return new ItRefReadOnly<T>();
	}

	public override string ToString()
	{
		return ValueSetup.ToString();
	}
}
