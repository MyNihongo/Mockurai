namespace MyNihongo.Mock;

public interface IInvocation
{
	long Index { get; }

	string? Snapshot { get; }
}

public sealed class InvocationSnapshot : IInvocation
{
	public required long Index { get; init; }

	public required string? Snapshot { get; init; }
}
