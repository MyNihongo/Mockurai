namespace MyNihongo.Mockurai.Abstractions.Tests.Comparison.EquivalencyComparerTests;

public sealed class EquivalentTupleShould
{
	[Fact]
	public void CompareOnlyEquatableValid()
	{
		var input1 = ("text", new OnlyEquatable("key1"));
		var input2 = ("text", new OnlyEquatable("key1"));

		var actual = EquivalencyComparer<(string, OnlyEquatable)>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void CompareOnlyEquatableInvalid()
	{
		var input1 = ("text", new OnlyEquatable("key1"));
		var input2 = ("text", new OnlyEquatable("key2"));

		var actual = EquivalencyComparer<(string, OnlyEquatable)>.Default.Equivalent(input1, input2);

		var expected = new ComparisonResult.Entry[]
		{
			new("this", "(text, key1)", "(text, key2)"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}
}

file readonly struct OnlyEquatable(string key) : IEquatable<OnlyEquatable>
{
	public readonly string Key = key;

	public bool Equals(OnlyEquatable other) =>
		Key == other.Key;

	public override bool Equals(object? obj) =>
		obj is OnlyEquatable other && Equals(other);

	public override int GetHashCode() =>
		Key.GetHashCode();

	public override string ToString()
	{
		return Key;
	}
}
