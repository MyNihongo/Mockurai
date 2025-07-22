namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SetupIntInt : ISetup
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

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
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in Action<int, int> callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Callback = callback;
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Exception = exception;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		public Action<int, int>? Callback;
		public Exception? Exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}

[Obsolete("Will be generated")]
public sealed class SetupIntInt<TReturns> : ISetup<TReturns>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

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

			setup.Callback?.Invoke(parameter1, parameter2);

			if (setup.Exception is not null)
				throw setup.Exception;

			if (setup.Returns is not null)
			{
				returnValue = setup.Returns(parameter1, parameter2);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public void SetupParameters(in It<int> setup1, in It<int> setup2)
	{
		_currentSetup = new Item(setup1.ValueSetup, setup2.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in Action<int, int> callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Callback = callback;
	}

	public void Returns(TReturns? value)
	{
		Returns((_, _) => value);
	}

	public void Returns(in Func<int, int, TReturns?> value)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Returns = value;
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Exception = exception;
	}

	private sealed class Item(in It<int>.Setup? parameter1, in It<int>.Setup? parameter2)
	{
		public readonly It<int>.Setup? Parameter1 = parameter1;
		public readonly It<int>.Setup? Parameter2 = parameter2;
		public Action<int, int>? Callback;
		public Func<int, int, TReturns?>? Returns;
		public Exception? Exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x is not null)
			{
				if (x.Parameter1.HasValue)
					xSort += x.Parameter1.Value.Sort;
				if (x.Parameter2.HasValue)
					xSort += x.Parameter2.Value.Sort;
			}

			if (y is not null)
			{
				if (y.Parameter1.HasValue)
					ySort += y.Parameter1.Value.Sort;
				if (y.Parameter2.HasValue)
					ySort += y.Parameter2.Value.Sort;
			}

			return xSort.CompareTo(ySort);
		}
	}
}
