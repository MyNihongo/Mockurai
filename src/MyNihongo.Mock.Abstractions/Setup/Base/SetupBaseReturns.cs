namespace MyNihongo.Mock;

public abstract class SetupBaseReturns<TReturns, TCallback, TReturnsCallback> : ISetup<TReturns>
{
	protected Exception? Exception;
	protected TReturnsCallback? ReturnsDelegate;
	protected TCallback? CallbackDelegate;

	public abstract void Returns(TReturns? value);

	public void Callback(in TCallback callback)
	{
		CallbackDelegate = callback;
	}

	public void Returns(in TReturnsCallback value)
	{
		ReturnsDelegate = value;
	}

	public void Throws(in Exception exception)
	{
		Exception = exception;
	}
}
