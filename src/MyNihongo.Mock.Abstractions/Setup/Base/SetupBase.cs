namespace MyNihongo.Mock;

public abstract class SetupBase<TCallback> : ISetup
{
	protected Exception? Exception;
	protected TCallback? CallbackDelegate;

	public void Callback(in TCallback callback)
	{
		CallbackDelegate = callback;
	}

	public void Throws(in Exception exception)
	{
		Exception = exception;
	}
}
