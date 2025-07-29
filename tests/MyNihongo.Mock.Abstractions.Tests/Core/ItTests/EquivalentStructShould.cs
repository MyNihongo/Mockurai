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

		var setupValue = new StructObject
		{
			Name = name,
			Age = age,
		};

		var inputValue = new StructObject
		{
			Name = name,
			Age = age,
		};

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

		var setupValue = new StructObject
		{
			Name = name1,
			Age = age,
		};

		var inputValue = new StructObject
		{
			Name = name2,
			Age = age,
		};

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

		var setupValue = new StructObject
		{
			Name = name,
			Age = age1,
		};

		var inputValue = new StructObject
		{
			Name = name,
			Age = age2,
		};

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

		var setupValue = new StructObject
		{
			Name = null,
			Age = age,
		};

		var inputValue = new StructObject
		{
			Name = name,
			Age = age,
		};

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

		var setupValue = new StructObject
		{
			Name = name,
			Age = age,
		};

		var inputValue = new StructObject
		{
			Name = null,
			Age = age,
		};

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

		var setupValue = new StructNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new StructObject
			{
				Name = name,
				Age = age,
			},
		};

		var inputValue = new StructNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new StructObject
			{
				Name = name,
				Age = age,
			},
		};

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

		var setupValue = new StructNestedObject
		{
			Price = price1,
			DateOfBirth = dateOfBirth,
			Child = new StructObject
			{
				Name = name,
				Age = age,
			},
		};

		var inputValue = new StructNestedObject
		{
			Price = price2,
			DateOfBirth = dateOfBirth,
			Child = new StructObject
			{
				Name = name,
				Age = age,
			},
		};

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

		var setupValue = new StructNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth1,
			Child = new StructObject
			{
				Name = name,
				Age = age,
			},
		};

		var inputValue = new StructNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth2,
			Child = new StructObject
			{
				Name = name,
				Age = age,
			},
		};

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

		var setupValue = new StructNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new StructObject
			{
				Name = name1,
				Age = age,
			},
		};

		var inputValue = new StructNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new StructObject
			{
				Name = name2,
				Age = age,
			},
		};

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

		var setupValue = new StructNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new StructObject
			{
				Name = name,
				Age = age1,
			},
		};

		var inputValue = new StructNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new StructObject
			{
				Name = name,
				Age = age2,
			},
		};

		var actual = It<StructNestedObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}
}

file readonly struct StructObject
{
	public string? Name { get; init; }

	public int Age { get; init; }
}

file readonly struct StructNestedObject
{
	public decimal Price { get; init; }

	public DateTime DateOfBirth { get; init; }

	public StructObject? Child { get; init; }
}
