using System.Collections;
using System.Runtime.InteropServices;

namespace MyNihongo.Mockurai;

/// <summary>
/// A sorted collection of setup items kept in order by the supplied <see cref="IComparer{T}"/>,
/// where new items with equal keys are appended after existing ones and enumeration yields items in reverse insertion order.
/// </summary>
/// <typeparam name="T">The setup type stored in the container.</typeparam>
public sealed class SetupContainer<T> : IEnumerable<T>
{
	private readonly IComparer<T> _comparer;
	private readonly List<T> _setups = [];

	/// <summary>
	/// Initializes a new instance of the <see cref="SetupContainer{T}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to determine the ordering of setups.</param>
	public SetupContainer(IComparer<T> comparer)
	{
		_comparer = comparer;
	}

	/// <summary>
	/// Inserts a setup into the container, preserving the order defined by the configured comparer.
	/// Items that compare equal to existing items are placed after them.
	/// </summary>
	/// <param name="item">The setup to add.</param>
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

	/// <inheritdoc/>
	public IEnumerator<T> GetEnumerator()
	{
		for (var i = _setups.Count - 1; i >= 0; i--)
			yield return _setups[i];
	}

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();
}
