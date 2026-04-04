using System.Collections;
using System.Runtime.InteropServices;

namespace MyNihongo.Mockurai;

public sealed class SetupContainer<T> : IEnumerable<T>
{
	private readonly IComparer<T> _comparer;
	private readonly List<T> _setups = [];

	public SetupContainer(IComparer<T> comparer)
	{
		_comparer = comparer;
	}

	public void Add(in T item)
	{
		var index = _setups.BinarySearch(item, _comparer);

		if (index < 0)
		{
			index = ~index;
		}
		else
		{
			var span = CollectionsMarshal.AsSpan(_setups);
			while (index < span.Length && _comparer.Compare(item, span[index]) == 0)
				index++;
		}

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
