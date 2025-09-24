namespace MyNihongo.Mock;

public abstract class SetupBase<TDelegate> : ISetup
{
	protected Exception? Exception;
	protected TDelegate? CallbackDelegate;

	public void Callback(in TDelegate callback)
	{
		CallbackDelegate = callback;
	}

	public void Throws(in Exception exception)
	{
		Exception = exception;
	}
}
