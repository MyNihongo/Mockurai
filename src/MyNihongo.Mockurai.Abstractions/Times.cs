namespace MyNihongo.Mockurai;

public readonly ref struct Times
{
	public readonly Func<int, bool> Predicate;
	private readonly string _stringValue;

	private Times(int count)
	{
		Predicate = x => x == count;
		_stringValue = count != 1 ? $"{count} times" : "1 time";
	}

	private Times(Func<int, bool> predicate, string stringValue)
	{
		Predicate = predicate;
		_stringValue = stringValue;
	}

	public static Times Exactly(in int count)
	{
		if (count < 0)
			throw new ArgumentException($"Count must not be negative, count=`{count}`", nameof(count));

		return new Times(count);
	}

	public static Times Once()
	{
		return new Times(1);
	}

	public static Times Never()
	{
		return new Times(0);
	}

	public static Times AtLeast(int count)
	{
		if (count < 0)
			throw new ArgumentException($"Count must not be negative, count=`{count}`", nameof(count));

		var stringValue = count != 1
			? $"at least {count} times"
			: "at least 1 time";

		return new Times(x => x >= count, stringValue);
	}

	public static Times AtMost(int count)
	{
		if (count < 0)
			throw new ArgumentException($"Count must not be negative, count=`{count}`", nameof(count));

		var stringValue = count != 1
			? $"at most {count} times"
			: "at most 1 time";

		return new Times(x => x <= count, stringValue);
	}

	public override string ToString()
	{
		return _stringValue;
	}
}
