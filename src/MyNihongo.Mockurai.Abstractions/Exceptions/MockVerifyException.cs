using System.Text;

namespace MyNihongo.Mockurai;

/// <summary>
/// Base exception thrown when a mock verification fails.
/// </summary>
/// <param name="message">The message describing the verification failure.</param>
public abstract class MockVerifyException(in string message) : Exception(message)
{
	/// <summary>
	/// Appends a formatted list of performed invocations to the supplied <paramref name="message"/>.
	/// </summary>
	/// <param name="message">The base failure message.</param>
	/// <param name="invocations">The performed invocations to append, or <see langword="null"/> to leave the message unchanged.</param>
	/// <returns>The original message, optionally followed by a list of performed invocations.</returns>
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
