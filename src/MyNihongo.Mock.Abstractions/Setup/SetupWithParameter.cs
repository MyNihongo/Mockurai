namespace MyNihongo.Mock;

public sealed class SetupWithParameter<TParameter> : ISetup
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public void Invoke(in TParameter parameter)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Predicate(parameter))
				continue;

			setup.Callback?.Invoke(parameter);

			if (setup.Exception is not null)
				throw setup.Exception;
		}
	}

	public void SetupParameter(in It<TParameter> parameter)
	{
		_currentSetup = new Item(parameter.ValueSetup);

		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in Action<TParameter> callback)
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

	private sealed class Item(in It<TParameter>.Setup? parameter)
	{
		public readonly It<TParameter>.Setup? Parameter = parameter;
		public Action<TParameter>? Callback;
		public Exception? Exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x?.Parameter.HasValue == true)
				xSort += x.Parameter.Value.Sort;

			if (y?.Parameter.HasValue == true)
				ySort += y.Parameter.Value.Sort;

			return xSort.CompareTo(ySort);
		}
	}
}
