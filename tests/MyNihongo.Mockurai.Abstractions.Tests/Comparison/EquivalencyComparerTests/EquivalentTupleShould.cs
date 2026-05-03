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

	[Fact]
	public void CompareOnlyComparableValid()
	{
		var input1 = ("text", new OnlyComparable("key1"));
		var input2 = ("text", new OnlyComparable("key1"));

		var actual = EquivalencyComparer<(string, OnlyComparable)>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void CompareOnlyComparableInvalid()
	{
		var input1 = ("text", new OnlyComparable("key1"));
		var input2 = ("text", new OnlyComparable("key2"));

		var actual = EquivalencyComparer<(string, OnlyComparable)>.Default.Equivalent(input1, input2);

		var expected = new ComparisonResult.Entry[]
		{
			new("this", "(text, key1)", "(text, key2)"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void CompareOnlyComparableGenericValid()
	{
		var input1 = ("text", new OnlyComparableGeneric("key1"));
		var input2 = ("text", new OnlyComparableGeneric("key1"));

		var actual = EquivalencyComparer<(string, OnlyComparableGeneric)>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void CompareOnlyComparableGenericInvalid()
	{
		var input1 = ("text", new OnlyComparableGeneric("key1"));
		var input2 = ("text", new OnlyComparableGeneric("key2"));

		var actual = EquivalencyComparer<(string, OnlyComparableGeneric)>.Default.Equivalent(input1, input2);

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

	public override string ToString() =>
		Key;
}

file readonly struct OnlyComparable(string key) : IComparable
{
	public readonly string Key = key;

	public int CompareTo(object? obj)
	{
		return obj is OnlyComparable other
			? Key.CompareTo(other.Key, StringComparison.InvariantCulture)
			: 1;
	}

	public override string ToString() =>
		Key;
}

file readonly struct OnlyComparableGeneric(string key) : IComparable<OnlyComparableGeneric>
{
	public readonly string Key = key;

	public int CompareTo(OnlyComparableGeneric other)
	{
		return Key.CompareTo(other.Key, StringComparison.InvariantCulture);
	}

	public override string ToString() =>
		Key;
}
