namespace MyNihongo.Mock;

public interface ISetupCallbackStart<TCallback>
{
	ISetupCallbackJoin<TCallback> Callback(in TCallback callback);
}

public interface ISetupCallbackJoin<TCallback> : ISetup<TCallback>
{
	ISetupThrowsReset<TCallback> And();
}

public interface ISetupCallbackReset<TCallback>
{
	ISetup<TCallback> Callback(in TCallback callback);
}
