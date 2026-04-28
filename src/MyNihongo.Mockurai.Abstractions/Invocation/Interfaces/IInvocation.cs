namespace MyNihongo.Mockurai;

/// <summary>
/// Represents a single recorded invocation against a mocked member.
/// </summary>
public interface IInvocation
{
	/// <summary>
	/// Gets the monotonically-increasing index assigned to this invocation when it was registered.
	/// </summary>
	long Index { get; }

	/// <summary>
	/// Gets a value indicating whether this invocation has been matched by a verification call.
	/// </summary>
	bool IsVerified { get; }

	/// <summary>
	/// Returns a human-readable representation of the invocation, including its index and arguments.
	/// </summary>
	string ToString();
}

/// <summary>
/// Represents a single recorded invocation that carries a strongly-typed argument payload.
/// </summary>
/// <typeparam name="TArguments">The type of the captured arguments.</typeparam>
public interface IInvocation<out TArguments> : IInvocation
{
	/// <summary>
	/// Gets the arguments that were passed to the mocked member at invocation time.
	/// </summary>
	TArguments Arguments { get; }
}
