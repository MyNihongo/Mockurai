namespace MyNihongo.Mock;

public abstract class SetupBase<TCallback>
	: ISetupCallbackJoin<TCallback>, ISetupCallbackReset<TCallback>,
		ISetupThrowsJoin<TCallback>, ISetupThrowsReset<TCallback>
{
	private readonly Queue<ItemSetup> _queue = [];
	private ItemSetup? _currentSetup;
	private bool _andContinue;

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
		_andContinue = true;
		return this;
	}

	ISetupCallbackReset<TCallback> ISetupThrowsJoin<TCallback>.And()
	{
		_andContinue = true;
		return this;
	}

	protected sealed class ItemSetup(in TCallback? callback = default, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public TCallback? Callback = callback;
		public Exception? Exception = exception;
	}
}
