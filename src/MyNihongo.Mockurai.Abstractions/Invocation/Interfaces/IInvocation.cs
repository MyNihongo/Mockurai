namespace MyNihongo.Mockurai;

public interface IInvocation
{
	long Index { get; }

	bool IsVerified { get; }

	string ToString();
}
