namespace MyNihongo.Mockurai;

internal static class EnumerableEx
{
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
