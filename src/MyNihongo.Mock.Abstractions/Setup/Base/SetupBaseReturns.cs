namespace MyNihongo.Mock;

public abstract class SetupBaseReturns<TCallback, TReturns, TReturnsCallback>
	: ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback>, ISetupCallbackReset<TCallback, TReturns, TReturnsCallback>,
		ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback>, ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>
{
	private readonly Queue<ItemSetup> _queue = [];
	private ItemSetup? _currentSetup;
	private bool _andContinue;

	public abstract void Returns(TReturns returns);

	public void Callback(in TCallback callback)
	{
		if (_andContinue && _currentSetup is not null)
		{
			_currentSetup.Callback = callback;
			_andContinue = false;
			_currentSetup = null;
		}
		else
		{
			_currentSetup = new ItemSetup(callback);
			_queue.Enqueue(_currentSetup);
		}
	}

	public void Returns(in TReturnsCallback returns)
	{
		if (_andContinue && _currentSetup is not null)
		{
			_currentSetup.Returns = returns;
			_andContinue = false;
			_currentSetup = null;
		}
		else
		{
			_currentSetup = new ItemSetup(returns: returns);
			_queue.Enqueue(_currentSetup);
		}
	}

	public void Throws(in Exception exception)
	{
		if (_andContinue && _currentSetup is not null)
		{
			_currentSetup.Exception = exception;
			_andContinue = false;
			_currentSetup = null;
		}
		else
		{
			_currentSetup = new ItemSetup(exception: exception);
			_queue.Enqueue(_currentSetup);
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

	ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> ISetupCallbackStart<TCallback, TReturns, TReturnsCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupCallbackReset<TCallback, TReturns, TReturnsCallback>.Callback(in TCallback callback)
	{
		Callback(callback);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>.Returns(in TReturnsCallback returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>.Returns(in TReturns returns)
	{
		Returns(returns);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>.Returns(in TReturnsCallback returns)
	{
		Returns(returns);
		return this;
	}

	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetup<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>.Throws(in Exception exception)
	{
		Throws(exception);
		return this;
	}

	ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback> ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback>.And()
	{
		_andContinue = true;
		return this;
	}

	ISetupCallbackReset<TCallback, TReturns, TReturnsCallback> ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback>.And()
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
