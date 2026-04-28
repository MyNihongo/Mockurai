namespace MyNihongo.Mockurai;

/// <summary>
/// Entry point of a returning setup chain that allows a callback to be configured.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public interface ISetupCallbackStart<TCallback, TReturns, TReturnsCallback>
{
	/// <summary>
	/// Configures the callback that runs when the mocked member is invoked.
	/// </summary>
	/// <param name="callback">The callback to invoke.</param>
	ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> Callback(in TCallback callback);
}

/// <summary>
/// Continuation of a callback setup for a returning member that allows pairing it with a return value or exception via <see cref="And"/>.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public interface ISetupCallbackJoin<TCallback, TReturns, TReturnsCallback> : ISetup<TCallback, TReturns, TReturnsCallback>
{
	/// <summary>
	/// Indicates that the next configured action should be combined with the current callback into a single setup step.
	/// </summary>
	ISetupReturnsThrowsReset<TCallback, TReturns, TReturnsCallback> And();
}

/// <summary>
/// Configures a callback as the secondary action of a previous step joined via <c>And</c>.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
/// <typeparam name="TReturnsCallback">The delegate type used to compute a return value dynamically.</typeparam>
public interface ISetupCallbackReset<TCallback, TReturns, TReturnsCallback>
{
	/// <summary>
	/// Configures the callback that runs when the mocked member is invoked.
	/// </summary>
	/// <param name="callback">The callback to invoke.</param>
	ISetup<TCallback, TReturns, TReturnsCallback> Callback(in TCallback callback);
}
