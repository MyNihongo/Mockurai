namespace MyNihongo.Mock;

public interface IMockSequence<out T>
{
	VerifyIndex VerifyIndex { get; }

	IMock<T> Mock { get; }
}

public sealed class MockSequence<T> : IMockSequence<T>
{
	public required VerifyIndex VerifyIndex { get; init; }

	public required IMock<T> Mock { get; init; }
}
