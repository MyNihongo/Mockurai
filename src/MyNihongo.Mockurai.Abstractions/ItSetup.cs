namespace MyNihongo.Mockurai;

/// <summary>
/// A compiled argument matcher that pairs a predicate with a <see cref="SetupType"/> tag used to order setups by specificity.
/// </summary>
/// <typeparam name="T">The argument type being matched.</typeparam>
public readonly struct ItSetup<T> : IComparable<ItSetup<T>>, IEquatable<ItSetup<T>>
{
	private readonly Func<T, bool>? _predicateBool;
	private readonly Func<T, ComparisonResult>? _predicateResult;
	private readonly Func<string>? _toString;
	private readonly object? _equalityKey;

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
	/// <param name="equalityKey">An optional key used for setup-identity comparison.</param>
	public ItSetup(Func<T, bool> predicate, SetupType type, Func<string> toString, object? equalityKey = null)
	{
		_predicateBool = predicate;
		_toString = toString;
		Type = type;
		_equalityKey = equalityKey;
	}

	/// <summary>
	/// Initializes a matcher backed by a structural-comparison predicate.
	/// </summary>
	/// <param name="predicate">The predicate that returns a populated <see cref="ComparisonResult"/> when the argument fails to match.</param>
	/// <param name="type">The matcher kind.</param>
	/// <param name="toString">A delegate that produces the matcher's string representation.</param>
	/// <param name="equalityKey">An optional key used for setup-identity comparison.</param>
	public ItSetup(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString, object? equalityKey = null)
	{
		_predicateResult = predicate;
		_toString = toString;
		Type = type;
		_equalityKey = equalityKey;
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
	public bool Equals(ItSetup<T> other)
	{
		if (Type != other.Type)
			return false;

		if (Type == SetupType.Any)
			return true;

		if (_equalityKey is null || other._equalityKey is null)
			return false;

		return _equalityKey.Equals(other._equalityKey);
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj) =>
		obj is ItSetup<T> other && Equals(other);

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return HashCode.Combine(Type, _equalityKey);
	}

    /// <summary>
    /// Determines whether two specified <see cref="ItSetup{T}"/> instances are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <c>true</c> if the <paramref name="left"/> and <paramref name="right"/> parameters are equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(ItSetup<T> left, ItSetup<T> right) => left.Equals(right);

    /// <summary>
    /// Determines whether two specified <see cref="ItSetup{T}"/> instances are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <c>true</c> if the <paramref name="left"/> and <paramref name="right"/> parameters are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(ItSetup<T> left, ItSetup<T> right) => !left.Equals(right);

	/// <inheritdoc/>
	public override string ToString()
	{
		return _toString?.Invoke() ?? "any";
	}
}
