using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MyNihongo.Mock;

public sealed class InvocationContainer<T> : IEnumerable<T>
	where T : class, IInvocation
{
	private readonly List<T> _invocations = [];

	public int Count => _invocations.Count;

	public void Add(in T item)
	{
		var insertIndex = _invocations.BinarySearch(item.Index);

		if (insertIndex < 0)
			insertIndex = ~insertIndex;

		_invocations.Insert(insertIndex, item);
	}

	public T? TryGetItemAt(in long index)
	{
		var itemIndex = TryGetIndexAt(index);

		return itemIndex.HasValue
			? _invocations[itemIndex.Value]
			: null;
	}

	public Span<T> GetItemsSpan()
	{
		return CollectionsMarshal.AsSpan(_invocations);
	}

	public Span<T> GetItemsSpanFrom(in long index)
	{
		var itemIndex = TryGetIndexAt(index);

		return itemIndex.HasValue
			? CollectionsMarshal.AsSpan(_invocations)[itemIndex.Value..]
			: Span<T>.Empty;
	}

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

	public IEnumerator<T> GetEnumerator() =>
		_invocations.GetEnumerator();

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
