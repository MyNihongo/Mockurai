namespace MyNihongo.Mockurai;

/// <summary>
/// Entry point of a setup chain that allows a callback to be configured.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type.</typeparam>
public interface ISetupCallbackStart<TCallback>
{
	/// <summary>
	/// Configures the callback that runs when the mocked member is invoked.
	/// </summary>
	/// <param name="callback">The callback to invoke.</param>
	ISetupCallbackJoin<TCallback> Callback(in TCallback callback);
}

/// <summary>
/// Continuation of a callback setup that allows pairing the callback with a follow-up <c>Throws</c> via <see cref="And"/>.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type.</typeparam>
public interface ISetupCallbackJoin<TCallback> : ISetup<TCallback>
{
	/// <summary>
	/// Indicates that the next configured action should be combined with the current callback into a single setup step.
	/// </summary>
	ISetupThrowsReset<TCallback> And();
}

/// <summary>
/// Configures a callback as the secondary action of a previous step joined via <c>And</c>.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type.</typeparam>
public interface ISetupCallbackReset<TCallback>
{
	/// <summary>
	/// Configures the callback that runs when the mocked member is invoked.
	/// </summary>
	/// <param name="callback">The callback to invoke.</param>
	ISetup<TCallback> Callback(in TCallback callback);
}
