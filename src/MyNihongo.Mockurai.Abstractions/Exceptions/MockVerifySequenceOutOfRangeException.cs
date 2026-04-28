namespace MyNihongo.Mockurai;

/// <summary>
/// Thrown when a sequence-based verification expects an invocation at a specific index but no such invocation exists.
/// </summary>
public sealed class MockVerifySequenceOutOfRangeException : MockVerifyException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MockVerifySequenceOutOfRangeException"/> class.
	/// </summary>
	/// <param name="name">The display name of the mocked member.</param>
	/// <param name="index">The expected sequence index that did not have a matching invocation.</param>
	/// <param name="invocations">The performed invocations to include in the error message, or <see langword="null"/> if none were recorded.</param>
	public MockVerifySequenceOutOfRangeException(in string name, in long index, in IEnumerable<string>? invocations)
		: base(AppendInvocations($"Expected {name} to be invoked at index {index}, but {ButText(invocations)}.", invocations))
	{
	}

	private static string ButText(in IEnumerable<string>? invocations)
	{
		return invocations is null
			? "there are no invocations"
			: "it has not been called";
	}
}
