namespace MyNihongo.Mock;

public abstract class SetupBaseReturns<TCallback, TReturns, TReturnsCallback>
	: ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback>, ISetupCallback<TCallback, TReturns, TReturnsCallback>,
		ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback>, ISetupReturnsThrows<TCallback, TReturns, TReturnsCallback>
{
	private readonly Queue<ItemSetup> _queue = [];
	private ItemSetup? _currentItem;
	private bool _andContinue;

	public abstract void Returns(TReturns? returns);

	public void Callback(in TCallback callback)
	{
		if (_andContinue && _currentItem is not null)
		{
			_currentItem.Callback = callback;
			_andContinue = false;
			_currentItem = null;
		}
		else
		{
			_currentItem = new ItemSetup(callback);
			_queue.Enqueue(_currentItem);
		}
	}

	public void Returns(in TReturnsCallback returns)
	{
		if (_andContinue && _currentItem is not null)
		{
			_currentItem.Returns = returns;
			_andContinue = false;
			_currentItem = null;
		}
		else
		{
			_currentItem = new ItemSetup(returns: returns);
			_queue.Enqueue(_currentItem);
		}
	}

	public void Throws(in Exception exception)
	{
		if (_andContinue && _currentItem is not null)
		{
			_currentItem.Exception = exception;
			_andContinue = false;
			_currentItem = null;
		}
		else
		{
			_currentItem = new ItemSetup(exception: exception);
			_queue.Enqueue(_currentItem);
		}
	}

	protected ItemSetup GetSetup()
	{
		return _queue.Count switch
		{
			0 => ItemSetup.Default,
			1 => _queue.Peek(),
			_ => _queue.Dequeue(),
		};
	}

	ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> ISetupCallbackChain<TCallback, TReturns, TReturnsCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupCallback<TCallback, TReturns, TReturnsCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsChain<TCallback, TReturns, TReturnsCallback>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsChain<TCallback, TReturns, TReturnsCallback>.Returns(in TReturnsCallback returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrows<TCallback, TReturns, TReturnsCallback>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrows<TCallback, TReturns, TReturnsCallback>.Returns(in TReturnsCallback returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsChain<TCallback, TReturns, TReturnsCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrows<TCallback, TReturns, TReturnsCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrows<TCallback, TReturns, TReturnsCallback> ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback>.And()
	{
		_andContinue = true;
		return this;
	}

	ISetupCallback<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback>.And()
	{
		_andContinue = true;
		return this;
	}

	protected sealed class ItemSetup(in TCallback? callback = default, in TReturnsCallback? returns = default, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public TCallback? Callback = callback;
		public TReturnsCallback? Returns = returns;
		public Exception? Exception = exception;
	}
}
