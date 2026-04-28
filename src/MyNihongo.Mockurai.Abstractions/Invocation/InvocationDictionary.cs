using System.Collections.Concurrent;

namespace MyNihongo.Mockurai;

/// <summary>
/// A thread-safe dictionary of <see cref="IInvocationVerify"/> entries keyed by <see cref="Type"/>.
/// </summary>
public class InvocationDictionary : InvocationDictionary<Type>;

/// <summary>
/// A thread-safe dictionary of <see cref="IInvocationVerify"/> entries that aggregates verification
/// across all registered values.
/// </summary>
/// <typeparam name="TKey">The key type used to look up invocation verifiers.</typeparam>
public class InvocationDictionary<TKey> : ConcurrentDictionary<TKey, IInvocationVerify>, IInvocationVerify
	where TKey : notnull
{
	/// <inheritdoc/>
	public IEnumerable<IInvocation> GetInvocations()
	{
		return Values.SelectMany(static x => x.GetInvocations());
	}

	/// <inheritdoc/>
	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		foreach (var value in Values)
			value.VerifyNoOtherCalls(invocationProviders);
	}
}
