namespace MyNihongo.Mock;

public abstract class SetupWithParameterBase<TParameter, TReturns, TCallback, TReturnsCallback> : ISetup<TReturns>
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

	public void Callback(in TCallback callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
	}

	public void Returns(in TReturnsCallback value)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(value);
	}

	public abstract void Returns(TReturns? value);

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	protected sealed class Item(in It<TParameter>.Setup? parameter)
	{
		private readonly Queue<(TCallback? Callback, TReturnsCallback? Returns, Exception? Exception)> _queue = [];
		public readonly It<TParameter>.Setup? Parameter = parameter;

		public void Add(in TCallback callback)
		{
			_queue.Enqueue((callback, default, null));
		}

		public void Add(in TReturnsCallback returns)
		{
			_queue.Enqueue((default, returns, null));
		}

		public void Add(in Exception exception)
		{
			_queue.Enqueue((default, default, exception));
		}

		public (TCallback? Callback, TReturnsCallback? Returns, Exception? Exception) GetSetup()
		{
			return _queue.Count switch
			{
				0 => (default, default, null),
				1 => _queue.Peek(),
				_ => _queue.Dequeue(),
			};
		}
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
