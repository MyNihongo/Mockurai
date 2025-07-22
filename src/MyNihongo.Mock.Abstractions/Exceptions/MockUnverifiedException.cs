using System.Text;

namespace MyNihongo.Mock;

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
