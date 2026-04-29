namespace MyNihongo.Mockurai;

/// <summary>
/// Argument matcher factory used in mock setups and verifications for <see langword="ref readonly"/> parameters of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The argument type being matched.</typeparam>
public readonly ref struct ItRefReadOnly<T>
{
	/// <summary>
	/// The underlying matcher created by this factory.
	/// </summary>
	public readonly ItSetup<T> ValueSetup;

	private ItRefReadOnly(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString);
	}

	/// <summary>
	/// Matches arguments equal to <paramref name="value"/> using <see cref="EqualityComparer{T}.Default"/>.
	/// </summary>
	/// <param name="value">The expected value.</param>
	public static ItRefReadOnly<T> Value(T value)
	{
		return new ItRefReadOnly<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.SerializeToJson());
	}

	/// <summary>
	/// Matches arguments that satisfy the supplied <paramref name="predicate"/>.
	/// </summary>
	/// <param name="predicate">The predicate evaluated against each argument.</param>
	public static ItRefReadOnly<T> Where(in Func<T, bool> predicate)
	{
		return new ItRefReadOnly<T>(predicate, SetupType.Where, Constants.WhereToString);
	}

	/// <summary>
	/// Matches any argument value.
	/// </summary>
	public static ItRefReadOnly<T> Any()
	{
		return new ItRefReadOnly<T>();
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return ValueSetup.ToString();
	}
}
