namespace MyNihongo.Mock;

public abstract class SetupWithParameterBase<TParameter, TCallback> : ISetup
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

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
	}

	protected sealed class Item(in It<TParameter>.Setup? parameter)
	{
		private readonly Queue<(TCallback? Callback, Exception? Exception)> _queue = [];
		public readonly It<TParameter>.Setup? Parameter = parameter;

		public void Add(in TCallback callback)
		{
			_queue.Enqueue((callback, null));
		}

		public void Add(in Exception exception)
		{
			_queue.Enqueue((default, exception));
		}

		public (TCallback? Callback, Exception? Exception) GetSetup()
		{
			return _queue.Count switch
			{
				0 => (default, null),
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
