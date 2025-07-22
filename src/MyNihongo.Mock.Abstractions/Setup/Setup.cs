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
