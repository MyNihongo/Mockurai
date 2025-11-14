namespace MyNihongo.Mock;

public interface IInvocationVerify : IInvocationProvider
{
	void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null);
}
