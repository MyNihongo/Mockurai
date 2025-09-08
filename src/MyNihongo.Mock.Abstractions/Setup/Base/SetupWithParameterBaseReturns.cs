namespace MyNihongo.Mock;

public abstract class SetupWithParameterBase<TParameter, TReturns, TDelegate> : ISetup<TReturns>
{
	private static readonly Comparer SortComparer = new();
	protected SetupContainer<Item>? Setups;
	private Item? _currentSetup;

	public void SetupParameter(in It<TParameter> parameter)
	{
		_currentSetup = new Item(parameter.ValueSetup);

		Setups ??= new SetupContainer<Item>(SortComparer);
		Setups.Add(_currentSetup);
	}

	public void Callback(in TDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Callback = callback;
	}

	public void Returns(TReturns? value)
	{
		Returns(_ => value);
	}

	public void Returns(in Func<TParameter, TReturns?> value)
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

	protected sealed class Item(in It<TParameter>.Setup? parameter)
	{
		public readonly It<TParameter>.Setup? Parameter = parameter;
		public TDelegate? Callback;
		public Func<TParameter, TReturns?>? Returns;
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
