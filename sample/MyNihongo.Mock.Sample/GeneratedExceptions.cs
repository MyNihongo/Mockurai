namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public abstract class MockVerifyException(in string message) : Exception(message);

public sealed class MockUnverifiedException : MockVerifyException
{
	public MockUnverifiedException(in string name, in long index)
		: base($"Expected {name} to be verified, but an invocation with the index {index} has not been verified")
	{
	}
}

public sealed class MockVerifyCountException : MockVerifyException
{
	public MockVerifyCountException(in string name, in int expectedCount, in int actualCount)
		: base($"Expected {name} to be called {expectedCount} times, but instead it was called {actualCount} times")
	{
	}
}

public sealed class MockVerifySequenceOutOfRangeException : MockVerifyException
{
	public MockVerifySequenceOutOfRangeException(in string name, in long index)
		: base($"Expected {name} to be invoked after {index} times, but there are no invocations")
	{
	}
}
