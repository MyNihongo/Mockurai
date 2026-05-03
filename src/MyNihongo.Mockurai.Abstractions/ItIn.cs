namespace MyNihongo.Mockurai;

/// <summary>
/// Argument matcher factory used in mock setups and verifications for <see langword="in"/> parameters of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The argument type being matched.</typeparam>
public readonly ref struct ItIn<T>
{
	/// <summary>
	/// The underlying matcher created by this factory.
	/// </summary>
	public readonly ItSetup<T> ValueSetup;

	private ItIn(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	private ItIn(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	/// <summary>
	/// Matches arguments equal to <paramref name="value"/> using <see cref="EqualityComparer{T}.Default"/>.
	/// </summary>
	/// <param name="value">The expected value.</param>
	public static ItIn<T> Value(T value)
	{
		return new ItIn<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.SerializeToJson());
	}

	/// <summary>
	/// Matches arguments that are structurally equivalent to <paramref name="value"/>.
	/// </summary>
	/// <param name="value">The reference value to compare against.</param>
	/// <param name="comparer">An optional comparer; <see cref="EquivalencyComparer{T}.Default"/> is used when <see langword="null"/>.</param>
	/// <param name="useJsonSnapshot">
	/// Set to <see langword="false"/> if <typeparamref name="T"/> cannot be correctly
	/// serialized or deserialized to JSON (e.g. contains circular references, custom converters, or non-serializable members).
	/// </param>
	public static ItIn<T> Equivalent(T value, IEquivalencyComparer<T>? comparer = null, bool useJsonSnapshot = true)
	{
		var setupType = useJsonSnapshot ? SetupType.Equivalent : SetupType.Value;
		return new ItIn<T>(x => (comparer ?? EquivalencyComparer<T>.Default).Equivalent(value, x), setupType, () => value.SerializeToJson());
	}

	/// <summary>
	/// Matches arguments that satisfy the supplied <paramref name="predicate"/>.
	/// </summary>
	/// <param name="predicate">The predicate evaluated against each argument.</param>
	public static ItIn<T> Where(in Func<T, bool> predicate)
	{
		return new ItIn<T>(predicate, SetupType.Where, Constants.WhereToString);
	}

	/// <summary>
	/// Matches any argument value.
	/// </summary>
	public static ItIn<T> Any()
	{
		return new ItIn<T>();
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return ValueSetup.ToString();
	}
}
