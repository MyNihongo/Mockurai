namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SetupIntInt : ISetup
{
	private SortedSet<(It<int>.Setup?, It<int>.Setup?, Exception)>? _setups;
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

		_setups ??= [];
		_setups.Add((_tempSetup.Value.Item1, _tempSetup.Value.Item2, exception));
		_tempSetup = null;
	}
}
