namespace MyNihongo.Mock;

public interface ISetupCallback<TCallback, TReturns, TReturnsCallback>
{
	ISetup<TCallback, TReturns, TReturnsCallback> Callback(in TCallback callback);
}

public interface ISetupCallbackChain<TCallback, TReturns, TReturnsCallback>
{
	ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> Callback(in TCallback callback);
}

public interface ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> : ISetup<TCallback, TReturns, TReturnsCallback>
{
	ISetupReturnsThrows<TCallback, TReturns, TReturnsCallback> And();
}
