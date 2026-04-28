namespace MyNihongo.Mockurai;

/// <summary>
/// Constraint expressing how many times a mocked member is expected to be invoked.
/// </summary>
public readonly ref struct Times
{
	/// <summary>
	/// The predicate that evaluates whether an observed invocation count satisfies this constraint.
	/// </summary>
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

	/// <summary>
	/// Requires the mocked member to be invoked exactly <paramref name="count"/> times.
	/// </summary>
	/// <param name="count">The exact expected invocation count.</param>
	/// <exception cref="ArgumentException">Thrown when <paramref name="count"/> is negative.</exception>
	public static Times Exactly(in int count)
	{
		if (count < 0)
			throw new ArgumentException($"Count must not be negative, count=`{count}`", nameof(count));

		return new Times(count);
	}

	/// <summary>
	/// Requires the mocked member to be invoked exactly once.
	/// </summary>
	public static Times Once()
	{
		return new Times(1);
	}

	/// <summary>
	/// Requires the mocked member to never be invoked.
	/// </summary>
	public static Times Never()
	{
		return new Times(0);
	}

	/// <summary>
	/// Requires the mocked member to be invoked at least <paramref name="count"/> times.
	/// </summary>
	/// <param name="count">The minimum expected invocation count.</param>
	/// <exception cref="ArgumentException">Thrown when <paramref name="count"/> is negative.</exception>
	public static Times AtLeast(int count)
	{
		if (count < 0)
			throw new ArgumentException($"Count must not be negative, count=`{count}`", nameof(count));

		var stringValue = count != 1
			? $"at least {count} times"
			: "at least 1 time";

		return new Times(x => x >= count, stringValue);
	}

	/// <summary>
	/// Requires the mocked member to be invoked at most <paramref name="count"/> times.
	/// </summary>
	/// <param name="count">The maximum expected invocation count.</param>
	/// <exception cref="ArgumentException">Thrown when <paramref name="count"/> is negative.</exception>
	public static Times AtMost(int count)
	{
		if (count < 0)
			throw new ArgumentException($"Count must not be negative, count=`{count}`", nameof(count));

		var stringValue = count != 1
			? $"at most {count} times"
			: "at most 1 time";

		return new Times(x => x <= count, stringValue);
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return _stringValue;
	}
}
