namespace MyNihongo.Mockurai;

/// <summary>
/// Argument matcher factory used in mock setups and verifications for by-value parameters of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The argument type being matched.</typeparam>
public readonly ref struct It<T>
{
	/// <summary>
	/// The underlying matcher created by this factory.
	/// </summary>
	public readonly ItSetup<T> ValueSetup;

	private It(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	private It(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	/// <summary>
	/// Matches arguments equal to <paramref name="value"/> using <see cref="EqualityComparer{T}.Default"/>.
	/// </summary>
	/// <param name="value">The expected value.</param>
	public static It<T> Value(T value)
	{
		return new It<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.ToJsonString());
	}

	/// <summary>
	/// Matches arguments that are structurally equivalent to <paramref name="value"/>.
	/// </summary>
	/// <param name="value">The reference value to compare against.</param>
	/// <param name="comparer">An optional comparer; <see cref="EquivalencyComparer{T}.Default"/> is used when <see langword="null"/>.</param>
	public static It<T> Equivalent(T value, IEquivalencyComparer<T>? comparer = null)
	{
		return new It<T>(x => (comparer ?? EquivalencyComparer<T>.Default).Equivalent(value, x), SetupType.Equivalent, () => value.ToJsonString());
	}

	/// <summary>
	/// Matches arguments that satisfy the supplied <paramref name="predicate"/>.
	/// </summary>
	/// <param name="predicate">The predicate evaluated against each argument.</param>
	public static It<T> Where(in Func<T, bool> predicate)
	{
		return new It<T>(predicate, SetupType.Where, Constants.WhereToString);
	}

	/// <summary>
	/// Matches any argument value.
	/// </summary>
	public static It<T> Any()
	{
		return new It<T>();
	}

	/// <summary>
	/// Implicitly converts a value into an equality matcher via <see cref="Value"/>.
	/// </summary>
	/// <param name="value">The expected value.</param>
	public static implicit operator It<T>(T value)
	{
		return Value(value);
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return ValueSetup.ToString();
	}
}
