namespace MyNihongo.Mock;

public readonly ref struct Times
{
	public readonly Func<int, bool> Predicate;
	private readonly string _stringValue;

	private Times(int count)
	{
		Predicate = x => x == count;
		_stringValue = count != 1 ? $"{count} times" : "1 time";
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

	public override string ToString()
	{
		return _stringValue;
	}
}
