namespace MyNihongo.Mockurai.Abstractions.Tests.Invocation.InvocationWithOneParameterTests;

public sealed class VerifyBadJsonShould : InvocationWithOneParameterTestsBase
{
	[Fact]
	public void ThrowVerifySameSnapshot()
	{
		const string key = "key";
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<PartitionKey>();
		fixture.Register(index, new PartitionKey(key));

		var actual = () =>
		{
			var it = It<PartitionKey>.Equivalent(new PartitionKey(key));
			fixture.Verify(it.ValueSetup, Times.Once());
		};

		const string expected =
			"""
			Expected MyClass.MyMethod({}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod({})
			  expected: {}
			  actual: {}
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		var exceptionMessage = exception.Message;
		Assert.Equal(expected, exceptionMessage);
	}

	[Fact]
	public void NotThrowVerifyWithoutSnapshot()
	{
		const string key = "key";
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<PartitionKey>();
		fixture.Register(index, new PartitionKey(key));

		var it = It<PartitionKey>.Equivalent(new PartitionKey(key), useJsonSnapshot: false);
		fixture.Verify(it.ValueSetup, Times.Once());
	}
}

file readonly struct PartitionKey : IEquatable<PartitionKey>
{
	public PartitionKey(string key)
	{
		Key = key;
	}

	internal string Key { get; }

	public bool Equals(PartitionKey other) =>
		Key == other.Key;

	public override bool Equals(object? obj) =>
		obj is PartitionKey other && Equals(other);

	public override int GetHashCode() =>
		Key.GetHashCode();
}
