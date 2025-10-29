namespace MyNihongo.Mock;

public abstract class SetupWithParameterBase<TParameter, TCallback> : ISetup<TCallback>
{
	private static readonly Comparer SortComparer = new();
	protected SetupContainer<Item>? Setups;
	private Item? _currentSetup;

	public ISetup<TCallback> SetupParameter(in It<TParameter> parameter)
	{
		_currentSetup = new Item(parameter.ValueSetup);

		Setups ??= new SetupContainer<Item>(SortComparer);
		Setups.Add(_currentSetup);

		return this;
	}

	public ISetup<TCallback> Callback(in TCallback callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(callback);
		return this;
	}

	public ISetup<TCallback> Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Add(exception);
		return this;
	}

	protected sealed class Item(in It<TParameter>.Setup? parameter)
	{
		public readonly It<TParameter>.Setup? Parameter = parameter;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;

		public void Add(in TCallback callback)
		{
			var currentSetup = new ItemSetup(callback);
			_queue.Enqueue(currentSetup);
			_currentSetup = currentSetup;
		}

		public void Add(in Exception exception)
		{
			if (_currentSetup is null)
			{
				_queue.Enqueue(new ItemSetup(exception: exception));
			}
			else
			{
				_currentSetup.Exception = exception;
				_currentSetup = null;
			}
		}

		public ItemSetup GetSetup()
		{
			return _queue.Count switch
			{
				0 => ItemSetup.Default,
				1 => _queue.Peek(),
				_ => _queue.Dequeue(),
			};
		}
	}

	protected sealed class ItemSetup(in TCallback? callback = default, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public readonly TCallback? Callback = callback;
		public Exception? Exception = exception;
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
