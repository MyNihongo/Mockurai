using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MyNihongo.Mockurai;

/// <summary>
/// A sorted collection of <see cref="IInvocation"/> items that maintains order by <see cref="IInvocation.Index"/>
/// and provides efficient range-based lookups.
/// </summary>
/// <typeparam name="T">The invocation type stored in the container.</typeparam>
public sealed class InvocationContainer<T> : IEnumerable<T>
	where T : class, IInvocation
{
	private readonly List<T> _invocations = [];

	/// <summary>
	/// Gets the number of invocations currently stored in the container.
	/// </summary>
	public int Count => _invocations.Count;

	/// <summary>
	/// Inserts an invocation while preserving ascending order by <see cref="IInvocation.Index"/>.
	/// </summary>
	/// <param name="item">The invocation to add.</param>
	public void Add(in T item)
	{
		var insertIndex = _invocations.BinarySearch(item.Index);

		if (insertIndex < 0)
			insertIndex = ~insertIndex;

		_invocations.Insert(insertIndex, item);
	}

	/// <summary>
	/// Returns the first invocation whose <see cref="IInvocation.Index"/> is greater than or equal to <paramref name="index"/>,
	/// or <see langword="null"/> if no such invocation exists.
	/// </summary>
	/// <param name="index">The lower-bound index to search for.</param>
	public T? TryGetItemAt(in long index)
	{
		var itemIndex = TryGetIndexAt(index);

		return itemIndex.HasValue
			? _invocations[itemIndex.Value]
			: null;
	}

	/// <summary>
	/// Returns a span over all invocations in the container.
	/// </summary>
	public Span<T> GetItemsSpan()
	{
		return CollectionsMarshal.AsSpan(_invocations);
	}

	/// <summary>
	/// Returns a span over all invocations whose <see cref="IInvocation.Index"/> is greater than or equal to <paramref name="index"/>.
	/// </summary>
	/// <param name="index">The inclusive lower-bound index.</param>
	public Span<T> GetItemsSpanFrom(in long index)
	{
		var itemIndex = TryGetIndexAt(index);

		return itemIndex.HasValue
			? CollectionsMarshal.AsSpan(_invocations)[itemIndex.Value..]
			: Span<T>.Empty;
	}

	/// <summary>
	/// Returns a span over all invocations whose <see cref="IInvocation.Index"/> is strictly less than <paramref name="index"/>.
	/// </summary>
	/// <param name="index">The exclusive upper-bound index.</param>
	public Span<T> GetItemsSpanBefore(in long index)
	{
		var itemIndex = TryGetIndexAt(index, inclusive: true);

		return itemIndex.HasValue
			? CollectionsMarshal.AsSpan(_invocations)[..itemIndex.Value]
			: Span<T>.Empty;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int? TryGetIndexAt(in long index, in bool inclusive = false)
	{
		if (_invocations.Count == 0)
			return null;

		var itemIndex = _invocations.BinarySearch(index);

		if (itemIndex < 0)
			itemIndex = ~itemIndex;

		return inclusive || itemIndex < _invocations.Count
			? itemIndex
			: null;
	}

	/// <inheritdoc/>
	public IEnumerator<T> GetEnumerator() =>
		_invocations.GetEnumerator();

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();
}

file static class ListEx
{
	public static int BinarySearch<T>(this List<T> @this, in long index)
		where T : class, IInvocation
	{
		var span = CollectionsMarshal.AsSpan(@this);
		int start = 0, end = span.Length - 1;

		while (start <= end)
		{
			var mid = (start + end) / 2;
			var compare = span[mid].Index.CompareTo(index);

			if (compare == 0)
				return mid;
			if (compare > 0)
				end = mid - 1;
			else
				start = mid + 1;
		}

		return ~start;
	}
}
