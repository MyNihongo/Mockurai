namespace MyNihongo.Mock.Abstractions.Tests.Collections.InvocationContainerTests;

public abstract class InvocationContainerTestsBase
{
	protected static InvocationContainer<Invocation> CreateFixture()
	{
		return [];
	}

	protected sealed class Invocation : IInvocation
	{
		public long Index { get; init; }

		public bool IsVerified { get; set; }

		public string? Label { get; init; }

		public override string ToString()
		{
			return !string.IsNullOrEmpty(Label)
				? $"{Index}: {Label}"
				: Index.ToString();
		}
	}
}
