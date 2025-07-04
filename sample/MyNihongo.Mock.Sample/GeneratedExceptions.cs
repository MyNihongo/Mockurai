namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class MockVerifyException : Exception
{
	public MockVerifyException(in string name, in int expectedCount, in int actualCount)
		: base($"Expected {name} to be called {expectedCount} times, but instead it was called {actualCount} times")
	{
	}

	public MockVerifyException(in string name, in long index)
		: base($"Expected {name} to be verified, but an invocation with the index {index} has not been verified")
	{
	}
}
