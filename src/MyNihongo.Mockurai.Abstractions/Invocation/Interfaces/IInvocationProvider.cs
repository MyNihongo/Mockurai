namespace MyNihongo.Mockurai;

public interface IInvocationProvider
{
	IEnumerable<IInvocation> GetInvocations();
}

public interface IInvocationProvider<out TArguments>
{
	IEnumerable<IInvocation<TArguments>> GetInvocationsWithArguments();
}
