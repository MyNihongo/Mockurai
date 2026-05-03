namespace MyNihongo.Mockurai.Abstractions.Tests.Invocation.InvocationWithOneParameterTests;

public sealed class VerifyBadJsonShould : InvocationWithOneParameterTestsBase
{
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void NotThrowVerifyWithoutSnapshot(bool useJsonSnapshot)
	{
		const string key = "key";
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<PartitionKey>();
		fixture.Register(index, new PartitionKey(key));

		var it = It<PartitionKey>.Equivalent(new PartitionKey(key), useJsonSnapshot: useJsonSnapshot);
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
