namespace MyNihongo.Mock;

public abstract class SetupWithParameterBase<TParameter, TCallback>
	: ISetupCallbackJoin<TCallback>, ISetupCallbackReset<TCallback>,
		ISetupThrowsJoin<TCallback>, ISetupThrowsReset<TCallback>
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

	ISetupCallbackJoin<TCallback> ISetupCallbackStart<TCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<TCallback> ISetupCallbackReset<TCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetupThrowsJoin<TCallback> ISetupThrowsStart<TCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<TCallback> ISetupThrowsReset<TCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupThrowsReset<TCallback> ISetupCallbackJoin<TCallback>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	ISetupCallbackReset<TCallback> ISetupThrowsJoin<TCallback>.And()
	{
		if (_currentSetup is not null)
			_currentSetup.AndContinue = true;

		return this;
	}

	protected sealed class Item(in ItSetup<TParameter>? parameter)
	{
		public readonly ItSetup<TParameter>? Parameter = parameter;
		private readonly Queue<ItemSetup> _queue = [];
		private ItemSetup? _currentSetup;
		public bool AndContinue;

		public void Add(in TCallback callback)
		{
			if (AndContinue && _currentSetup is not null)
			{
				_currentSetup.Callback = callback;
				AndContinue = false;
				_currentSetup = null;
			}
			else
			{
				_currentSetup = new ItemSetup(callback);
				_queue.Enqueue(_currentSetup);
			}
		}

		public void Add(in Exception exception)
		{
			if (AndContinue && _currentSetup is not null)
			{
				_currentSetup.Exception = exception;
				AndContinue = false;
				_currentSetup = null;
			}
			else
			{
				_currentSetup = new ItemSetup(exception: exception);
				_queue.Enqueue(_currentSetup);
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
