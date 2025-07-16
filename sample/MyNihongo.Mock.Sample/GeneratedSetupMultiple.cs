namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SetupIntInt : ISetup
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<(It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Exception? Exception)>? _setups;
	private (It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Exception? Exception)? _currentSetup;

	public void Invoke(in int parameter1, in int parameter2)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Predicate(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Predicate(parameter2))
				continue;

			setup.Callback?.Invoke(parameter1, parameter2);

			if (setup.Exception is not null)
				throw setup.Exception;
		}
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = (setup1.ValueSetup, setup2.ValueSetup, null, null);
	}

	public void Callback(in Action<int, int> callback)
	{
		if (!_currentSetup.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_setups ??= new SetupContainer<(It<int>.Setup?, It<int>.Setup?, Action<int, int>?, Exception?)>(SortComparer);
		_currentSetup = _setups.Add((_currentSetup.Value.Parameter1, _currentSetup.Value.Parameter2, callback, _currentSetup.Value.Exception));
	}

	public void Throws(in Exception exception)
	{
		if (!_currentSetup.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_setups ??= new SetupContainer<(It<int>.Setup?, It<int>.Setup?, Action<int, int>?, Exception?)>(SortComparer);
		_currentSetup = _setups.Add((_currentSetup.Value.Parameter1, _currentSetup.Value.Parameter2, _currentSetup.Value.Callback, exception));
	}

	private sealed class Comparer : IComparer<(It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Exception? Exception)>
	{
		public int Compare((It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Exception? Exception) x, (It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Exception? Exception) y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x.Parameter1.HasValue)
				xSort += x.Parameter1.Value.Sort;
			if (x.Parameter2.HasValue)
				xSort += x.Parameter2.Value.Sort;

			if (y.Parameter1.HasValue)
				ySort += y.Parameter1.Value.Sort;
			if (y.Parameter2.HasValue)
				ySort += y.Parameter2.Value.Sort;

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupIntInt<TReturns> : ISetup<TReturns>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<(It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Func<int, int, TReturns?>? Returns, Exception? Exception)>? _setups;
	private (It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Func<int, int, TReturns?>? Returns, Exception? Exception)? _currentSetup;

	public bool Execute(in int parameter1, in int parameter2, out TReturns? returnValue)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Predicate(parameter1))
				continue;
			if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Predicate(parameter2))
				continue;

			if (setup.Exception is not null)
				throw setup.Exception;

			returnValue = setup.Returns is not null ? setup.Returns(parameter1, parameter2) : default;
			return true;
		}

		Default:
		returnValue = default;
		return false;
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = (setup1.ValueSetup, setup2.ValueSetup, null, null, null);
	}

	public void Callback(in Action<int, int> callback)
	{
		if (!_currentSetup.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_setups ??= new SetupContainer<(It<int>.Setup?, It<int>.Setup?, Action<int, int>? Callback, Func<int, int, TReturns?>?, Exception?)>(SortComparer);
		_setups.Add((_currentSetup.Value.Parameter1, _currentSetup.Value.Parameter2, callback, _currentSetup.Value.Returns, _currentSetup.Value.Exception));
	}

	public void Returns(TReturns? value)
	{
		Returns((_, _) => value);
	}

	public void Returns(in Func<int, int, TReturns?> value)
	{
		if (!_currentSetup.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_setups ??= new SetupContainer<(It<int>.Setup?, It<int>.Setup?, Action<int, int>? Callback, Func<int, int, TReturns?>?, Exception?)>(SortComparer);
		_setups.Add((_currentSetup.Value.Parameter1, _currentSetup.Value.Parameter2, _currentSetup.Value.Callback, value, _currentSetup.Value.Exception));
	}

	public void Throws(in Exception exception)
	{
		if (!_currentSetup.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_setups ??= new SetupContainer<(It<int>.Setup?, It<int>.Setup?, Action<int, int>? Callback, Func<int, int, TReturns?>? Returns, Exception?)>(SortComparer);
		_setups.Add((_currentSetup.Value.Item1, _currentSetup.Value.Item2, _currentSetup.Value.Callback, _currentSetup.Value.Returns, exception));
	}

	private sealed class Comparer : IComparer<(It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Func<int, int, TReturns?>? Returns, Exception? Exception)>
	{
		public int Compare((It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Func<int, int, TReturns?>? Returns, Exception? Exception) x, (It<int>.Setup? Parameter1, It<int>.Setup? Parameter2, Action<int, int>? Callback, Func<int, int, TReturns?>? Returns, Exception? Exception) y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x.Parameter1.HasValue)
				xSort += x.Parameter1.Value.Sort;
			if (x.Parameter2.HasValue)
				xSort += x.Parameter2.Value.Sort;

			if (y.Parameter1.HasValue)
				ySort += y.Parameter1.Value.Sort;
			if (y.Parameter2.HasValue)
				ySort += y.Parameter2.Value.Sort;

			return xSort.CompareTo(ySort);
		}
	}
}
