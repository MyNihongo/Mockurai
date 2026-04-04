namespace MyNihongo.Mockurai;

public interface IInvocationProvider
{
	IEnumerable<IInvocation> GetInvocations();
}
