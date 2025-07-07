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
		if (_invocations.Count == 0)
			return null;

		var indexPair = (index, default(T));
		var insertIndex = _invocations.BinarySearch(indexPair!);

		if (insertIndex < 0)
			insertIndex = ~insertIndex;

		return insertIndex < _invocations.Count
			? _invocations[insertIndex]
			: null;
	}

	public IEnumerator<(long Index, T Invocation)> GetEnumerator() =>
		_invocations.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();

	public IEnumerable<string> GetItemStrings()
	{
		for (var i = 0; i < _invocations.Count; i++)
			yield return _invocations[i].GetString();
	}
}

public static class TupleEx
{
	public static string GetString<T>(this (long, T) @this)
	{
		return $"{@this.Item1}: {@this.Item2}";
	}
}
