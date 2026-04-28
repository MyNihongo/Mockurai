namespace MyNihongo.Mockurai;

/// <summary>
/// Internal helpers for converting collections of invocations into string sequences while collapsing empty inputs to <see langword="null"/>.
/// </summary>
internal static class EnumerableEx
{
	/// <summary>
	/// Projects each invocation to its string representation, returning <see langword="null"/> when the source contains no elements.
	/// </summary>
	/// <typeparam name="T">The invocation element type.</typeparam>
	/// <param name="this">The source sequence.</param>
	public static IEnumerable<string>? NullIfEmpty<T>(this IEnumerable<T> @this)
		where T : class, IInvocation
	{
		int count;
		if (@this is InvocationContainer<T> invocationContainer)
		{
			count = invocationContainer.Count;
		}
		else if (!@this.TryGetNonEnumeratedCount(out count))
		{
			var array = @this.ToArray();
			@this = array;
			count = array.Length;
		}

		return count > 0
			? @this.Select(static x => x.ToString())
			: null;
	}

	/// <summary>
	/// Returns the source sequence projected to itself, or <see langword="null"/> when the source contains no elements.
	/// </summary>
	/// <param name="this">The source sequence of strings.</param>
	public static IEnumerable<string>? NullIfEmpty(this IEnumerable<string> @this)
	{
		if (!@this.TryGetNonEnumeratedCount(out var count))
		{
			var array = @this.ToArray();
			@this = array;
			count = array.Length;
		}

		return count > 0
			? @this.Select(static x => x.ToString())
			: null;
	}
}
