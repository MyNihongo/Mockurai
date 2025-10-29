namespace MyNihongo.Mock;

public interface ISetup<TCallback>
{
	ISetup<TCallback> Callback(in TCallback callback);

	ISetup<TCallback> Throws(in Exception exception);
}
