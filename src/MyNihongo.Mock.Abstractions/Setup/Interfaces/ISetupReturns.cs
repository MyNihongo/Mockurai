namespace MyNihongo.Mock;

public interface ISetup<TCallback, TReturns, TReturnsCallback>
	: ISetupCallbackChain<TCallback, TReturns, TReturnsCallback>, 
		ISetupReturnsThrowsChain<TCallback, TReturns, TReturnsCallback>;
