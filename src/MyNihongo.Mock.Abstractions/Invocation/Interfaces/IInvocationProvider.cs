namespace MyNihongo.Mock;

public interface IInvocationProvider
{
	IEnumerable<IInvocation> GetInvocations();
}
