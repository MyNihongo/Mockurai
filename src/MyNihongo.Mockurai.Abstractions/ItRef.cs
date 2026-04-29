namespace MyNihongo.Mockurai;

/// <summary>
/// Argument matcher factory used in mock setups and verifications for <see langword="ref"/> parameters of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The argument type being matched.</typeparam>
public readonly ref struct ItRef<T>
{
	/// <summary>
	/// The underlying matcher created by this factory.
	/// </summary>
	public readonly ItSetup<T> ValueSetup;

	private ItRef(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	/// <summary>
	/// Matches arguments equal to <paramref name="value"/> using <see cref="EqualityComparer{T}.Default"/>.
	/// </summary>
	/// <param name="value">The expected value.</param>
	public static ItRef<T> Value(T value)
	{
		return new ItRef<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.SerializeToJson());
	}

	/// <summary>
	/// Matches arguments that satisfy the supplied <paramref name="predicate"/>.
	/// </summary>
	/// <param name="predicate">The predicate evaluated against each argument.</param>
	public static ItRef<T> Where(in Func<T, bool> predicate)
	{
		return new ItRef<T>(predicate, SetupType.Where, Constants.WhereToString);
	}

	/// <summary>
	/// Matches any argument value.
	/// </summary>
	public static ItRef<T> Any()
	{
		return new ItRef<T>();
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return ValueSetup.ToString();
	}
}
