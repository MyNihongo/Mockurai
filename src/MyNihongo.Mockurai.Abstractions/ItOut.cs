namespace MyNihongo.Mockurai;

/// <summary>
/// Argument placeholder used in mock setups and verifications for <see langword="out"/> parameters of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The argument type being matched.</typeparam>
public readonly ref struct ItOut<T>()
{
	/// <summary>
	/// The underlying matcher; for <see langword="out"/> parameters this always matches any value.
	/// </summary>
	public readonly ItSetup<T> ValueSetup = new();

	/// <summary>
	/// Returns an <see cref="ItOut{T}"/> that matches any value.
	/// </summary>
	public static ItOut<T> Any()
	{
		return new ItOut<T>();
	}
}
