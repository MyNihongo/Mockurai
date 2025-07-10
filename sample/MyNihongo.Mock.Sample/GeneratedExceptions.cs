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

	public MockUnverifiedException(in string name, in IReadOnlyList<string> invocations)
		: base(CreateMessage(name, invocations))
	{
	}

	private static string CreateMessage(in string name, in IReadOnlyList<long> indices)
	{
		var stringBuilder = InitStringBuilder(name);

		foreach (var index in indices)
		{
			stringBuilder
				.AppendLine()
				.Append($"- index {index}");
		}

		return stringBuilder.ToString();
	}

	private static string CreateMessage(in string name, in IReadOnlyList<string> invocations)
	{
		var stringBuilder = InitStringBuilder(name);

		foreach (var invocation in invocations)
		{
			stringBuilder
				.AppendLine()
				.Append($"- {invocation}");
		}

		return stringBuilder.ToString();
	}

	private static StringBuilder InitStringBuilder(in string name)
	{
		return new StringBuilder()
			.Append($"Expected {name} to be verified, but the following invocations have not been verified:");
	}
}

public sealed class MockVerifyCountException : MockVerifyException
{
	public MockVerifyCountException(in string name, in int expectedCount, in int actualCount, in IEnumerable<string>? invocations = null)
		: base($"Expected {name} to be called {expectedCount} times, but instead it was called {actualCount} times")
	{
	}

	private static string CreateMessage(in string name, in int expectedCount, in int actualCount, in IEnumerable<string>? invocations = null)
	{
		var message = $"Expected {name} to be called {expectedCount} times, but instead it was called {actualCount} times";
		if (invocations is null)
			return message;

		var stringBuilder = new StringBuilder(message)
			.AppendLine()
			.AppendLine("Performed invocations:");

		foreach (var invocation in invocations)
		{
			stringBuilder
				.AppendLine()
				.AppendLine($"- {invocation}");
		}

		return stringBuilder.ToString();
	}
}

public sealed class MockVerifySequenceOutOfRangeException : MockVerifyException
{
	public MockVerifySequenceOutOfRangeException(in string name, in long index)
		: base($"Expected {name} to be invoked at index {index}, but there are no invocations")
	{
	}
}
