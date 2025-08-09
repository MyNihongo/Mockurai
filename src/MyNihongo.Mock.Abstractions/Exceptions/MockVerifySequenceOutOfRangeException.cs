namespace MyNihongo.Mock;

public sealed class MockVerifySequenceOutOfRangeException : MockVerifyException
{
	public MockVerifySequenceOutOfRangeException(in string name, in long index, in IEnumerable<string>? invocations = null)
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
