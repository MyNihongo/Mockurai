namespace MyNihongo.Mockurai;

/// <summary>
/// Entry point of a returning setup chain that allows a return value or an exception to be configured.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type for the parent setup.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public interface ISetupReturnsThrowsStart<TCallback, TReturns, TReturnsCallback>
{
	/// <summary>
	/// Configures the mocked member to return a constant value.
	/// </summary>
	/// <param name="returns">The value to return.</param>
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Returns(in TReturns returns);

	/// <summary>
	/// Configures the mocked member to return a value computed by the supplied delegate.
	/// </summary>
	/// <param name="returns">The delegate that produces the return value.</param>
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Returns(in TReturnsCallback returns);

	/// <summary>
	/// Configures the mocked member to throw <paramref name="exception"/> when invoked.
	/// </summary>
	/// <param name="exception">The exception to throw.</param>
	ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> Throws(in Exception exception);
}

/// <summary>
/// Continuation of a return/throws setup that allows pairing it with a callback via <see cref="And"/>.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type for the parent setup.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public interface ISetupReturnsThrowsJoin<TCallback, TReturns, TReturnsCallback> : ISetup<TCallback, TReturns, TReturnsCallback>
{
	/// <summary>
	/// Indicates that the next configured action should be combined with the current return value or exception into a single setup step.
	/// </summary>
	ISetupCallbackReset<TCallback, TReturns, TReturnsCallback> And();
}

/// <summary>
/// Configures a return value or exception as the secondary action of a previous step joined via <c>And</c>.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type for the parent setup.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public interface ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback>
{
	/// <summary>
	/// Configures the mocked member to return a constant value.
	/// </summary>
	/// <param name="returns">The value to return.</param>
	ISetup<TCallback, TReturns, TReturnsCallback> Returns(in TReturns returns);

	/// <summary>
	/// Configures the mocked member to return a value computed by the supplied delegate.
	/// </summary>
	/// <param name="returns">The delegate that produces the return value.</param>
	ISetup<TCallback, TReturns, TReturnsCallback> Returns(in TReturnsCallback returns);

	/// <summary>
	/// Configures the mocked member to throw <paramref name="exception"/> when invoked.
	/// </summary>
	/// <param name="exception">The exception to throw.</param>
	ISetup<TCallback, TReturns, TReturnsCallback> Throws(in Exception exception);
}
