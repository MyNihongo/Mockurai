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
