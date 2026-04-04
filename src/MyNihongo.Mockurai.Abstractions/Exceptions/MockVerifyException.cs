using System.Text;

namespace MyNihongo.Mockurai;

public abstract class MockVerifyException(in string message) : Exception(message)
{
	protected static string AppendInvocations(in string message, in IEnumerable<string>? invocations)
	{
		if (invocations is null)
			return message;

		var stringBuilder = new StringBuilder(message)
			.AppendLine()
			.Append("Performed invocations:");

		foreach (var invocation in invocations)
		{
			stringBuilder
				.AppendLine()
				.Append($"- {invocation}");
		}

		return stringBuilder.ToString();
	}
}
