namespace MyNihongo.Mock;

public abstract class SetupBaseReturns<TCallback, TReturns, TReturnsCallback>
{
	private readonly Queue<ItemSetup> _queue = [];
	private ItemSetup? _currentSetup;

	public abstract void Returns(TReturns? returns);

	public void Callback(in TCallback callback)
	{
		var currentSetup = new ItemSetup(callback);
		_queue.Enqueue(currentSetup);
		_currentSetup = currentSetup;
	}

	public void Returns(in TReturnsCallback returns)
	{
		if (_currentSetup is null)
		{
			_queue.Enqueue(new ItemSetup(returns: returns));
		}
		else
		{
			_currentSetup.Returns = returns;
			_currentSetup = null;
		}
	}

	public void Throws(in Exception exception)
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

	protected ItemSetup GetSetup()
	{
		return _queue.Count switch
		{
			0 => ItemSetup.Default,
			1 => _queue.Peek(),
			_ => _queue.Dequeue(),
		};
	}

	protected sealed class ItemSetup(in TCallback? callback = default, in TReturnsCallback? returns = default, in Exception? exception = null)
	{
		public static readonly ItemSetup Default = new();

		public readonly TCallback? Callback = callback;
		public TReturnsCallback? Returns = returns;
		public Exception? Exception = exception;
	}
}
