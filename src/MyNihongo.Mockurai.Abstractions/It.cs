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

	private It(Func<T, bool> predicate, SetupType type, Func<string> toString, object? equalityKey = null)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString, equalityKey);
	}

	private It(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString, object? equalityKey = null)
	{
		ValueSetup = new ItSetup<T>(predicate, type, toString, equalityKey);
	}

	/// <summary>
	/// Matches arguments equal to <paramref name="value"/> using <see cref="EqualityComparer{T}.Default"/>.
	/// </summary>
	/// <param name="value">The expected value.</param>
	public static It<T> Value(T value)
	{
		return new It<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => value.SerializeToJson(limitLength: true), equalityKey: value);
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
	public static It<T> Equivalent(T value, IEquivalencyComparer<T>? comparer = null, bool useJsonSnapshot = true)
	{
		var setupType = useJsonSnapshot ? SetupType.Equivalent : SetupType.Value;
		return new It<T>(x => (comparer ?? EquivalencyComparer<T>.Default).Equivalent(value, x), setupType, () => value.SerializeToJson(limitLength: true), equalityKey: value);
	}

	/// <summary>
	/// Matches arguments that satisfy the supplied <paramref name="predicate"/> expression.
	/// The expression tree is used for setup-identity comparison so that duplicate setups overwrite.
	/// </summary>
	/// <param name="predicate">The predicate expression evaluated against each argument.</param>
	public static It<T> Where(Expression<Func<T, bool>> predicate)
	{
		var expressionString = predicate.ToString();
		return new It<T>(predicate.Compile(), SetupType.Where, () => expressionString, equalityKey: expressionString);
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
