namespace MyNihongo.Mockurai;

/// <summary>
/// Entry point of a setup chain that allows an exception to be thrown when the mocked member is invoked.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type for the parent setup.</typeparam>
public interface ISetupThrowsStart<TCallback>
{
	/// <summary>
	/// Configures the mocked member to throw <paramref name="exception"/> when invoked.
	/// </summary>
	/// <param name="exception">The exception to throw.</param>
	ISetupThrowsJoin<TCallback> Throws(in Exception exception);
}

/// <summary>
/// Continuation of a throwing setup that allows pairing the exception with a callback via <see cref="And"/>.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type for the parent setup.</typeparam>
public interface ISetupThrowsJoin<TCallback> : ISetup<TCallback>
{
	/// <summary>
	/// Indicates that the next configured action should be combined with the current exception into a single setup step.
	/// </summary>
	ISetupCallbackReset<TCallback> And();
}

/// <summary>
/// Configures an exception as the secondary action of a previous step joined via <c>And</c>.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type for the parent setup.</typeparam>
public interface ISetupThrowsReset<TCallback>
{
	/// <summary>
	/// Configures the mocked member to throw <paramref name="exception"/> when invoked.
	/// </summary>
	/// <param name="exception">The exception to throw.</param>
	ISetup<TCallback> Throws(in Exception exception);
}
