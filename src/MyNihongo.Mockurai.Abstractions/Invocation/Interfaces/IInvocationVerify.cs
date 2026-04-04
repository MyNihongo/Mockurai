namespace MyNihongo.Mockurai;

public interface IInvocationVerify : IInvocationProvider
{
	void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null);
}
