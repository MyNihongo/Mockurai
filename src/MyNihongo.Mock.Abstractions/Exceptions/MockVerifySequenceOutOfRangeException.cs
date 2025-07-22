namespace MyNihongo.Mock;

public sealed class MockVerifySequenceOutOfRangeException : MockVerifyException
{
	public MockVerifySequenceOutOfRangeException(in string name, in long index)
		: base($"Expected {name} to be invoked at index {index}, but there are no invocations")
	{
	}
}
