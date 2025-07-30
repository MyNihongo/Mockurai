namespace MyNihongo.Mock.Abstractions.Tests.Comparison.EquivalencyComparerTests;

public sealed class EquivalentShould
{
	[Fact]
	public void BeEmptyIfAllEqual()
	{
		const int age = 17;
		const string name = "Okayama Issei";
		var dateOfBirth = new DateOnly(2024, 6, 29);
		var dateTimeUpdated = new DateTime(2025, 7, 30, 18, 23, 32);

		var input1 = new ClassObject
		{
			Age = age,
			DateTimeUpdated = dateTimeUpdated,
			Name = name,
			DateOfBirth = dateOfBirth,
		};

		var input2 = new ClassObject
		{
			Age = age,
			DateTimeUpdated = dateTimeUpdated,
			Name = name,
			DateOfBirth = dateOfBirth,
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void HaveOneEntryIfOneInvalid()
	{
		const int age1 = 17, age2 = 18;
		const string name = "Okayama Issei";
		var dateOfBirth = new DateOnly(2024, 6, 29);
		var dateTimeUpdated = new DateTime(2025, 7, 30, 18, 23, 32);

		var input1 = new ClassObject
		{
			Age = age1,
			DateTimeUpdated = dateTimeUpdated,
			Name = name,
			DateOfBirth = dateOfBirth,
		};

		var input2 = new ClassObject
		{
			Age = age2,
			DateTimeUpdated = dateTimeUpdated,
			Name = name,
			DateOfBirth = dateOfBirth,
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Age", "17", "18"),
		};

		Assert.Equivalent(expected, actual.Entries, true);
	}
}

file sealed class ClassObject
{
	public int Age;
	public DateTime DateTimeUpdated;
	public Item? Parent;

	public string? Name { get; init; }

	public DateOnly DateOfBirth { get; init; }

	public List<Item>? Children { get; init; }

	public sealed class Item
	{
		public long Ticks;
		public ClassObject[]? Objects;

		public bool IsValid { get; init; }

		public ClassObject? Object { get; init; }
	}
}
