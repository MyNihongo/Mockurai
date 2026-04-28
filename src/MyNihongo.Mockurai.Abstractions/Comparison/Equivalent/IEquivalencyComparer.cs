namespace MyNihongo.Mockurai;

/// <summary>
/// Compares two values of type <typeparamref name="T"/> for structural equivalence.
/// </summary>
/// <typeparam name="T">The type of the values being compared.</typeparam>
public interface IEquivalencyComparer<in T>
{
	/// <summary>
	/// Compares two values for structural equivalence.
	/// </summary>
	/// <param name="x">The expected value.</param>
	/// <param name="y">The actual value.</param>
	/// <returns>A <see cref="ComparisonResult"/> describing any detected differences.</returns>
	ComparisonResult Equivalent(T? x, T? y);
}
