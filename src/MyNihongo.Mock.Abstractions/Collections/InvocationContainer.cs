using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MyNihongo.Mock;

public sealed class InvocationContainer<T> : IEnumerable<(long Index, T Invocation)>
{
	private readonly List<(long Index, T Invocation)> _invocations = [];

	public int Count => _invocations.Count;

	public void Add(in long index, in T item)
	{
		var indexPair = (index, item);
		var insertIndex = _invocations.BinarySearch(indexPair);

		if (insertIndex < 0)
			insertIndex = ~insertIndex;

		_invocations.Insert(insertIndex, indexPair);
	}

	public (long Index, T Invocation)? TryGetItemAt(in long index)
	{
		var itemIndex = TryGetIndexAt(index);

		return itemIndex.HasValue
			? _invocations[itemIndex.Value]
			: null;
	}

	public Span<(long Index, T Invocation)> GetItemsSpan()
	{
		return CollectionsMarshal.AsSpan(_invocations);
	}

	public Span<(long Index, T Invocation)> GetItemsSpanFrom(in long index)
	{
		var itemIndex = TryGetIndexAt(index);

		return itemIndex.HasValue
			? CollectionsMarshal.AsSpan(_invocations)[itemIndex.Value..]
			: Span<(long Index, T Invocation)>.Empty;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int? TryGetIndexAt(in long index)
	{
		if (_invocations.Count == 0)
			return null;

		var indexPair = (index, default(T));
		var itemIndex = _invocations.BinarySearch(indexPair!);

		if (itemIndex < 0)
			itemIndex = ~itemIndex;

		return itemIndex < _invocations.Count
			? itemIndex
			: null;
	}

	public IEnumerator<(long Index, T Invocation)> GetEnumerator() =>
		_invocations.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();
}
