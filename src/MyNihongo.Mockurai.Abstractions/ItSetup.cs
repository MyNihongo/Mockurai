namespace MyNihongo.Mockurai;

/// <summary>
/// A compiled argument matcher that pairs a predicate with a <see cref="SetupType"/> tag used to order setups by specificity.
/// </summary>
/// <typeparam name="T">The argument type being matched.</typeparam>
public readonly struct ItSetup<T> : IComparable<ItSetup<T>>
{
	private readonly Func<T, bool>? _predicateBool;
	private readonly Func<T, ComparisonResult>? _predicateResult;
	private readonly Func<string>? _toString;

	/// <summary>
	/// The matcher kind used to order setups by specificity.
	/// </summary>
	public readonly SetupType Type;

	/// <summary>
	/// Initializes a matcher backed by a boolean predicate.
	/// </summary>
	/// <param name="predicate">The predicate evaluated against each argument.</param>
	/// <param name="type">The matcher kind.</param>
	/// <param name="toString">A delegate that produces the matcher's string representation.</param>
	public ItSetup(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		_predicateBool = predicate;
		_toString = toString;
		Type = type;
	}

	/// <summary>
	/// Initializes a matcher backed by a structural-comparison predicate.
	/// </summary>
	/// <param name="predicate">The predicate that returns a populated <see cref="ComparisonResult"/> when the argument fails to match.</param>
	/// <param name="type">The matcher kind.</param>
	/// <param name="toString">A delegate that produces the matcher's string representation.</param>
	public ItSetup(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString)
	{
		_predicateResult = predicate;
		_toString = toString;
		Type = type;
	}

	/// <summary>
	/// Gets the sort key derived from <see cref="Type"/>; higher values indicate more specific matchers.
	/// </summary>
	public int Sort => (int)Type;

	/// <inheritdoc/>
	public int CompareTo(ItSetup<T> other)
	{
		return Sort.CompareTo(Sort);
	}

	/// <summary>
	/// Returns whether <paramref name="value"/> satisfies this matcher.
	/// </summary>
	/// <param name="value">The argument to test.</param>
	public bool Check(in T value)
	{
		if (_predicateBool is not null)
			return _predicateBool.Invoke(value);

		if (_predicateResult is not null)
			return _predicateResult.Invoke(value);

		return Type == SetupType.Any;
	}

	/// <summary>
	/// Returns whether <paramref name="value"/> satisfies this matcher and outputs the structural-comparison result, when one is produced.
	/// </summary>
	/// <param name="value">The argument to test.</param>
	/// <param name="result">When the matcher uses a <see cref="ComparisonResult"/>-based predicate, the populated comparison result; otherwise <see langword="null"/>.</param>
	public bool Check(in T value, out ComparisonResult? result)
	{
		if (_predicateBool is not null)
		{
			result = null;
			return _predicateBool(value);
		}

		if (_predicateResult is not null)
			return result = _predicateResult.Invoke(value);

		result = null;
		return Type == SetupType.Any;
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return _toString?.Invoke() ?? "any";
	}
}
