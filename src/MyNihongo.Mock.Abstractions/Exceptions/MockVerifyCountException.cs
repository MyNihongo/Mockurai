using System.Text;

namespace MyNihongo.Mock;

public sealed class MockVerifyCountException : MockVerifyException
{
	public MockVerifyCountException(in string name, in int expectedCount, in int actualCount, in IEnumerable<string>? invocations = null)
		: base(CreateMessage(name, expectedCount, actualCount, invocations))
	{
	}

	private static string CreateMessage(in string name, in int expectedCount, in int actualCount, in IEnumerable<string>? invocations)
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
