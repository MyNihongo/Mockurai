namespace MyNihongo.Mock;

public interface ISetup<TCallback, in TReturns, TReturnsCallback>
{
	ISetup<TCallback, TReturns, TReturnsCallback> Callback(in TCallback callback);

	ISetup<TCallback, TReturns, TReturnsCallback> Throws(in Exception exception);

	ISetup<TCallback, TReturns, TReturnsCallback> Returns(TReturns? returns);

	ISetup<TCallback, TReturns, TReturnsCallback> Returns(in TReturnsCallback returns);
}
