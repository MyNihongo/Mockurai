namespace MyNihongo.Mockurai;

/// <summary>
/// Exposes the invocations recorded against a mock for inspection and reporting.
/// </summary>
public interface IInvocationProvider
{
	/// <summary>
	/// Returns the invocations recorded against this provider.
	/// </summary>
	IEnumerable<IInvocation> GetInvocations();
}

/// <summary>
/// Exposes invocations recorded against a mock together with their strongly-typed arguments.
/// </summary>
/// <typeparam name="TArguments">The type of the captured arguments.</typeparam>
public interface IInvocationProvider<out TArguments>
{
	/// <summary>
	/// Returns the invocations recorded against this provider, including their arguments.
	/// </summary>
	IEnumerable<IInvocation<TArguments>> GetInvocationsWithArguments();
}
