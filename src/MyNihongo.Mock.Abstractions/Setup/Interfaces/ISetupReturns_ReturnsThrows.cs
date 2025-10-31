namespace MyNihongo.Mock;

public interface ISetupReturnsThrows<TCallback, TReturns, TReturnsCallback>
{
	ISetup<TCallback, TReturns, TReturnsCallback> Throws(in Exception exception);
	ISetup<TCallback, TReturns, TReturnsCallback> Returns(in TReturns returns);
	ISetup<TCallback, TReturns, TReturnsCallback> Returns(in TReturnsCallback returns);
}

public interface ISetupReturnsThrowsChain<TCallback, TReturns, TReturnsCallback>
{
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Throws(in Exception exception);
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Returns(in TReturns returns);
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Returns(in TReturnsCallback returns);
}

public interface ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> : ISetup<TCallback, TReturns, TReturnsCallback>
{
	ISetupCallback<TCallback, TReturns, TReturnsCallback> And();
}
