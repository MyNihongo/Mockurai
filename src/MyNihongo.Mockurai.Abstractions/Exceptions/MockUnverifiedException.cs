using System.Text;

namespace MyNihongo.Mockurai;

/// <summary>
/// Thrown when a mock has invocations that were not asserted by any verification call.
/// </summary>
public sealed class MockUnverifiedException : MockVerifyException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MockUnverifiedException"/> class.
	/// </summary>
	/// <param name="name">The display name of the mocked member.</param>
	/// <param name="invocations">The invocations that were not verified.</param>
	public MockUnverifiedException(in string name, in IEnumerable<string> invocations)
		: base(CreateMessage(name, invocations))
	{
	}

	private static string CreateMessage(in string name, in IEnumerable<string> invocations)
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
