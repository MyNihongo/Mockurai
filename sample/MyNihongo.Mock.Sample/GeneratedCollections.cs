using System.Collections;

namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SetupContainer<T> : IEnumerable<T>
	where T : IComparable<T>
{
	private readonly IComparer<T>? _comparer;
	private readonly List<T> _setups = [];

	public SetupContainer(IComparer<T>? comparer = null)
	{
		_comparer = comparer;
	}

	public void Add(in T item)
	{
		var index = _setups.BinarySearch(item, _comparer);

		if (index < 0)
			index = ~index;

		_setups.Insert(index, item);
	}

	public IEnumerator<T> GetEnumerator()
	{
		for (var i = _setups.Count - 1; i >= 0; i--)
			yield return _setups[i];
	}

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();
}

[Obsolete("Will be generated")]
public sealed class InvocationContainer<T> : IEnumerable<(long, T)>
{
	private readonly List<(long, T)> _invocations = [];

	public int Count => _invocations.Count;

	public void Add(in long index, in T item)
	{
		var indexPair = (index, item);
		var insertIndex = _invocations.BinarySearch(indexPair);

		if (insertIndex < 0)
			insertIndex = ~insertIndex;

		_invocations.Insert(insertIndex, indexPair);
	}

	public IEnumerator<(long, T)> GetEnumerator() =>
		_invocations.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();
}
