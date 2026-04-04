using System.Collections.Concurrent;

namespace MyNihongo.Mockurai;

public class InvocationDictionary : InvocationDictionary<Type>;

public class InvocationDictionary<TKey> : ConcurrentDictionary<TKey, IInvocationVerify>, IInvocationVerify
	where TKey : notnull
{
	public IEnumerable<IInvocation> GetInvocations()
	{
		return Values.SelectMany(static x => x.GetInvocations());
	}

	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		foreach (var value in Values)
			value.VerifyNoOtherCalls(invocationProviders);
	}
}
