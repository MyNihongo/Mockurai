namespace MyNihongo.Mockurai;

/// <summary>
/// Combines invocation tracking with the ability to assert that no unverified invocations remain.
/// </summary>
public interface IInvocationVerify : IInvocationProvider
{
	/// <summary>
	/// Asserts that every recorded invocation has been matched by a previous verification call.
	/// </summary>
	/// <param name="invocationProviders">An optional accessor that supplies additional invocation providers used to enrich the failure message.</param>
	/// <exception cref="MockUnverifiedException">Thrown when one or more invocations have not been verified.</exception>
	void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null);
}
