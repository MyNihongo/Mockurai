namespace MyNihongo.Mock;

public interface ISetup<TCallback, TReturns, TReturnsCallback>
	: ISetupCallbackStart<TCallback, TReturns, TReturnsCallback>, 
		ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>;
