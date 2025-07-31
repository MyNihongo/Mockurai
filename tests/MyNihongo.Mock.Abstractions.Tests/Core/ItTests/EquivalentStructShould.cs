namespace MyNihongo.Mock.Abstractions.Tests.Core.ItTests;

public sealed class EquivalentStructShould
{
	[Fact]
	public void BeTrueIfDefault()
	{
		var setupValue = new StructObject();
		var inputValue = new StructObject();

		var actual = It<StructObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeTrueIfAllPropertiesEqual()
	{
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new StructObject(name, age);
		var inputValue = new StructObject(name, age);

		var actual = It<StructObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual1()
	{
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		const int age = 17;

		var setupValue = new StructObject(name1, age);
		var inputValue = new StructObject(name2, age);

		var actual = It<StructObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual2()
	{
		const string name = "Okayama Issei";
		const int age1 = 17, age2 = 18;

		var setupValue = new StructObject(name, age1);
		var inputValue = new StructObject(name, age2);

		var actual = It<StructObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual3()
	{
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new StructObject(name: null, age);
		var inputValue = new StructObject(name, age);

		var actual = It<StructObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual4()
	{
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new StructObject(name, age);
		var inputValue = new StructObject(name: null, age);

		var actual = It<StructObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeTrueIfAllPropertiesEqualNested()
	{
		const decimal price = 123.45m;
		var dateOfBirth = new DateTime(2025, 07, 28);
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new StructNestedObject(
			price,
			dateOfBirth,
			new StructObject(name, age)
		);

		var inputValue = new StructNestedObject(
			price,
			dateOfBirth,
			new StructObject(name, age)
		);

		var actual = It<StructNestedObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeFalseIfAllPropertiesNotEqualNested1()
	{
		const decimal price1 = 123.45m, price2 = 234.56m;
		var dateOfBirth = new DateTime(2025, 07, 28);
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new StructNestedObject(
			price1,
			dateOfBirth,
			new StructObject(name, age)
		);

		var inputValue = new StructNestedObject(
			price2,
			dateOfBirth,
			new StructObject(name, age)
		);

		var actual = It<StructNestedObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfAllPropertiesNotEqualNested2()
	{
		const decimal price = 123.45m;
		DateTime dateOfBirth1 = new(2025, 07, 28), dateOfBirth2 = new(2025, 07, 29);
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new StructNestedObject(
			price,
			dateOfBirth1,
			new StructObject(name, age)
		);

		var inputValue = new StructNestedObject(
			price,
			dateOfBirth2,
			new StructObject(name, age)
		);

		var actual = It<StructNestedObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfAllPropertiesNotEqualNested3()
	{
		const decimal price = 123.45m;
		var dateOfBirth = new DateTime(2025, 07, 28);
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		const int age = 17;

		var setupValue = new StructNestedObject(
			price,
			dateOfBirth,
			new StructObject(name1, age)
		);

		var inputValue = new StructNestedObject(
			price,
			dateOfBirth,
			new StructObject(name2, age)
		);

		var actual = It<StructNestedObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfAllPropertiesNotEqualNested4()
	{
		const decimal price = 123.45m;
		var dateOfBirth = new DateTime(2025, 07, 28);
		const string name = "Okayama Issei";
		const int age1 = 17, age2 = 18;

		var setupValue = new StructNestedObject(
			price,
			dateOfBirth,
			new StructObject(name, age1)
		);

		var inputValue = new StructNestedObject(
			price,
			dateOfBirth,
			new StructObject(name, age2)
		);

		var actual = It<StructNestedObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeTrueForCollections()
	{
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new StructObject[]
		{
			new(name, age),
			new(name, age),
		};

		var inputValue = new StructObject[]
		{
			new(name, age),
			new(name, age),
		};

		var actual = It<StructObject[]>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeFalseCollectionPropertyNotEqual()
	{
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		const int age = 17;

		var setupValue = new StructObject[]
		{
			new(name1, age),
			new(name1, age),
		};

		var inputValue = new StructObject[]
		{
			new(name1, age),
			new(name2, age),
		};

		var actual = It<StructObject[]>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseCollectionNestedPropertyNotEqual()
	{
		const decimal price = 123.45m;
		var dateOfBirth = new DateTime(2025, 07, 29);
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		const int age = 17;

		var setupValue = new[]
		{
			new StructNestedObject(
				price,
				dateOfBirth,
				new StructObject(name1, age)
			),
			new StructNestedObject(
				price,
				dateOfBirth,
				new StructObject(name1, age)
			),
		};

		var inputValue = new[]
		{
			new StructNestedObject(
				price,
				dateOfBirth,
				new StructObject(name1, age)
			),
			new StructNestedObject(
				price,
				dateOfBirth,
				new StructObject(name2, age)
			),
		};

		var actual = It<StructNestedObject[]>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}
}

file readonly struct StructObject(string? name, int age)
{
	public readonly string? Name = name;
	public readonly int Age = age;
}

file readonly struct StructNestedObject(decimal price, DateTime dateOfBirth, StructObject? child)
{
	public readonly decimal Price = price;
	public readonly DateTime DateOfBirth = dateOfBirth;
	public readonly StructObject? Child = child;
}
