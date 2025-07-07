using System.Text;

namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public abstract class MockVerifyException(in string message) : Exception(message);

public sealed class MockUnverifiedException : MockVerifyException
{
	public MockUnverifiedException(in string name, in IReadOnlyList<long> indices)
		: base(CreateMessage(name, indices))
	{
	}

	private static string CreateMessage(in string name, in IReadOnlyList<long> indices)
	{
		var stringBuilder = new StringBuilder()
			.Append($"Expected {name} to be verified, but the following invocations have not been verified");

		foreach (var index in indices)
		{
			stringBuilder
				.AppendLine()
				.Append($"- index {index}");
		}

		return stringBuilder.ToString();
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
		: base($"Expected {name} to be invoked at index {index}, but there are no invocations")
	{
	}
}
