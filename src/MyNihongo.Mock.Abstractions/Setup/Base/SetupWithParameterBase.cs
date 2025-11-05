namespace MyNihongo.Mock;

public abstract class SetupWithParameterBase<TParameter, TCallback>
	: ISetupCallbackJoin<TCallback>, ISetupCallback<TCallback>,
		ISetupThrowsJoin<TCallback>, ISetupThrows<TCallback>
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

	ISetupCallbackJoin<TCallback> ISetupCallbackChain<TCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<TCallback> ISetupCallback<TCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetupThrowsJoin<TCallback> ISetupThrowsChain<TCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<TCallback> ISetupThrows<TCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupThrows<TCallback> ISetupCallbackJoin<TCallback>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallback<TCallback> ISetupThrowsJoin<TCallback>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	protected sealed class Item(in It<TParameter>.Setup? parameter)
	{
		public readonly It<TParameter>.Setup? Parameter = parameter;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentItem;
		public bool AndContinue;

		public void Add(in TCallback callback)
		{
			if (AndContinue && _currentItem is not null)
			{
				_currentItem.Callback = callback;
				AndContinue = false;
				_currentItem = null;
			}
			else
			{
				_currentItem = new ItemSetup(callback);
				_queue.Enqueue(_currentItem);
			}
		}

		public void Add(in Exception exception)
		{
			if (AndContinue && _currentItem is not null)
			{
				_currentItem.Exception = exception;
				AndContinue = false;
				_currentItem = null;
			}
			else
			{
				_currentItem = new ItemSetup(exception: exception);
				_queue.Enqueue(_currentItem);
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

		public TCallback? Callback = callback;
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
