namespace MyNihongo.Mock;

public interface ISetupCallback<TCallback>
{
	ISetup<TCallback> Callback(in TCallback callback);
}

public interface ISetupCallbackChain<TCallback>
{
	ISetupCallbackJoin<TCallback> Callback(in TCallback callback);
}

public interface ISetupCallbackJoin<TCallback> : ISetup<TCallback>
{
	ISetupThrows<TCallback> And();
}
