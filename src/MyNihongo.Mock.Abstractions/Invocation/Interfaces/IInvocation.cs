namespace MyNihongo.Mock;

public interface IInvocation
{
	long Index { get; }

	bool IsVerified { get; }

	string ToString();
}
