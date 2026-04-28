namespace MyNihongo.Mockurai;

/// <summary>
/// Thrown when the number of times a mocked member was invoked does not match the expected <see cref="Times"/> constraint.
/// </summary>
public sealed class MockVerifyCountException : MockVerifyException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MockVerifyCountException"/> class.
	/// </summary>
	/// <param name="name">The display name of the mocked member.</param>
	/// <param name="expectedCount">The expected invocation count constraint.</param>
	/// <param name="actualCount">The actual number of invocations observed.</param>
	/// <param name="invocations">The performed invocations to include in the error message, or <see langword="null"/> if none should be listed.</param>
	public MockVerifyCountException(in string name, in Times expectedCount, in int actualCount, in IEnumerable<string>? invocations)
		: base(AppendInvocations($"Expected {name} to be called {expectedCount.ToString()}, but instead it was called {Times.Exactly(actualCount).ToString()}.", invocations))
	{
	}
}
