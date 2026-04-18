namespace MyNihongo.Mockurai;

public interface IInvocation
{
	long Index { get; }

	bool IsVerified { get; }

	string ToString();
}

public interface IInvocation<out TArguments> : IInvocation
{
	TArguments Arguments { get; }
}
