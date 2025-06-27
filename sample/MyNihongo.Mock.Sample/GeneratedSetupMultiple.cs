namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SetupIntInt : ISetup
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<(It<int>.Setup?, It<int>.Setup?, Exception)>? _setups;
	private (It<int>.Setup?, It<int>.Setup?)? _tempSetup;

	public void Invoke(in int parameter1, in int parameter2)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Item1.HasValue && !setup.Item1.Value.Predicate(parameter1))
				continue;
			if (setup.Item2.HasValue && !setup.Item2.Value.Predicate(parameter2))
				continue;

			throw setup.Item3;
		}
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_tempSetup = (setup1.ValueSetup, setup2.ValueSetup);
	}

	public void Throws(in Exception exception)
	{
		if (!_tempSetup.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_setups ??= new SetupContainer<(It<int>.Setup?, It<int>.Setup?, Exception)>(SortComparer);
		_setups.Add((_tempSetup.Value.Item1, _tempSetup.Value.Item2, exception));
		_tempSetup = null;
	}

	private sealed class Comparer : IComparer<(It<int>.Setup?, It<int>.Setup?, Exception)>
	{
		public int Compare((It<int>.Setup?, It<int>.Setup?, Exception) x, (It<int>.Setup?, It<int>.Setup?, Exception) y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x.Item1.HasValue)
				xSort += x.Item1.Value.Sort;
			if (x.Item2.HasValue)
				xSort += x.Item2.Value.Sort;

			if (y.Item1.HasValue)
				ySort += y.Item1.Value.Sort;
			if (y.Item2.HasValue)
				ySort += y.Item2.Value.Sort;

			return xSort.CompareTo(ySort);
		}
	}
}
