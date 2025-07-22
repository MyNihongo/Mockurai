namespace MyNihongo.Mock;

public sealed class Setup : ISetup
{
	private Exception? _exception;
	private Action? _callback;

	public void Invoke()
	{
		_callback?.Invoke();

		if (_exception is not null)
			throw _exception;
	}

	public void Callback(in Action callback)
	{
		_callback = callback;
	}

	public void Throws(in Exception exception)
	{
		_exception = exception;
	}
}

public sealed class Setup<TReturns> : ISetup<TReturns>
{
	private Exception? _exception;
	private Func<TReturns?>? _returns;
	private Action? _callback;

	public bool Execute(out TReturns? returnValue)
	{
		_callback?.Invoke();

		if (_exception is not null)
			throw _exception;

		if (_returns is not null)
		{
			returnValue = _returns();
			return true;
		}

		returnValue = default;
		return false;
	}

	public void Callback(in Action callback)
	{
		_callback = callback;
	}

	public void Returns(TReturns? value)
	{
		Returns(() => value);
	}

	public void Returns(in Func<TReturns?> value)
	{
		_returns = value;
	}

	public void Throws(in Exception exception)
	{
		_exception = exception;
	}
}
