using System.Collections;
using System.Runtime.InteropServices;

namespace MyNihongo.Mockurai;

/// <summary>
/// A sorted collection of setup items kept in order by the supplied <see cref="IComparer{T}"/>,
/// where new items that are identity-equal to an existing item replace it, and otherwise are appended after existing ones.
/// Enumeration yields items in reverse insertion order.
/// </summary>
/// <typeparam name="T">The setup type stored in the container.</typeparam>
public sealed class SetupContainer<T> : IEnumerable<T>
{
	private readonly IComparer<T> _comparer;
	private readonly IEqualityComparer<T>? _equalityComparer;
	private readonly List<T> _setups = [];

	/// <summary>
	/// Initializes a new instance of the <see cref="SetupContainer{T}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to determine the ordering of setups.</param>
	/// <param name="equalityComparer">An optional equality comparer used to detect duplicate setups that should be replaced.</param>
	public SetupContainer(IComparer<T> comparer, IEqualityComparer<T>? equalityComparer = null)
	{
		_comparer = comparer;
		_equalityComparer = equalityComparer;
	}

	/// <summary>
	/// Inserts a setup into the container, preserving the order defined by the configured comparer.
	/// If an identity-equal item already exists (determined by the equality comparer), it is replaced.
	/// Otherwise, the new item is placed after existing items that compare equal.
	/// </summary>
	/// <param name="item">The setup to add.</param>
	public void Add(in T item)
	{
		var index = _setups.BinarySearch(item, _comparer);

		if (index < 0)
		{
			index = ~index;
			_setups.Insert(index, item);
			return;
		}

		var span = CollectionsMarshal.AsSpan(_setups);
		if (_equalityComparer is not null)
		{
			var start = index;
			while (start > 0 && _comparer.Compare(item, span[start - 1]) == 0)
				start--;

			for (var i = start; i < span.Length && _comparer.Compare(item, span[i]) == 0; i++)
			{
				if (_equalityComparer.Equals(item, span[i]))
				{
					_setups[i] = item;
					return;
				}
			}
		}

		while (index < span.Length && _comparer.Compare(item, span[index]) == 0)
			index++;

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
