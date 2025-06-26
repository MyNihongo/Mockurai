namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SetupIntInt : ISetup
{
	private SortedSet<(Func<int, bool>?, Func<int, bool>?, Exception)>? _setups;
	private (Func<int, bool>?, Func<int, bool>?)? _tempSetup;

	public void Invoke(in int parameter1, in int parameter2)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Item1 is not null && !setup.Item1(parameter1))
				continue;
			if (setup.Item2 is not null && !setup.Item2(parameter2))
				continue;

			throw setup.Item3;
		}
	}

	public void SetupParameters(in Func<int, bool>? setup1, in Func<int, bool>? setup2)
	{
		_tempSetup = (setup1, setup2);
	}

	public void Throws(in Exception exception)
	{
		if (!_tempSetup.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_setups ??= new SortedSet<(Func<int, bool>?, Func<int, bool>?, Exception)>();
		_setups.Add((_tempSetup.Value.Item1, _tempSetup.Value.Item2, exception));
		_tempSetup = null;
	}
}
