namespace MyNihongo.Mock;

public interface ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>
{
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Returns(in TReturns returns);
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Returns(in TReturnsCallback returns);
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Throws(in Exception exception);
}

public interface ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> : ISetup<TCallback, TReturns, TReturnsCallback>
{
	ISetupCallbackReset<TCallback, TReturns, TReturnsCallback> And();
}

public interface ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>
{
	ISetup<TCallback, TReturns, TReturnsCallback> Returns(in TReturns returns);
	ISetup<TCallback, TReturns, TReturnsCallback> Returns(in TReturnsCallback returns);
	ISetup<TCallback, TReturns, TReturnsCallback> Throws(in Exception exception);
}
