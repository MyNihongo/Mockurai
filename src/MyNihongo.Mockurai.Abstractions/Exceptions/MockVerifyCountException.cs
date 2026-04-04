namespace MyNihongo.Mockurai;

public sealed class MockVerifyCountException : MockVerifyException
{
	public MockVerifyCountException(in string name, in Times expectedCount, in int actualCount, in IEnumerable<string>? invocations)
		: base(AppendInvocations($"Expected {name} to be called {expectedCount.ToString()}, but instead it was called {Times.Exactly(actualCount).ToString()}.", invocations))
	{
	}
}
