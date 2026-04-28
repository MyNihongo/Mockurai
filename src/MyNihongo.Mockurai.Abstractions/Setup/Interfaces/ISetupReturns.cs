namespace MyNihongo.Mockurai;

/// <summary>
/// The starting point of a returning mock setup, allowing a callback, a return value, or an exception to be configured.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type invoked when the mocked member is called.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public interface ISetup<TCallback, TReturns, TReturnsCallback>
	: ISetupCallbackStart<TCallback, TReturns, TReturnsCallback>,
		ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>;
