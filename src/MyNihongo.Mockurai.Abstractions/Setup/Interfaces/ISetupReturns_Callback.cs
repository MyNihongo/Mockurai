namespace MyNihongo.Mockurai;

public interface ISetupCallbackStart<TCallback, TReturns, TReturnsCallback>
{
	ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> Callback(in TCallback callback);
}

public interface ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> : ISetup<TCallback, TReturns, TReturnsCallback>
{
	ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback> And();
}

public interface ISetupCallbackReset<TCallback, TReturns, TReturnsCallback>
{
	ISetup<TCallback, TReturns, TReturnsCallback> Callback(in TCallback callback);
}
