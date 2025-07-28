namespace MyNihongo.Mock.Abstractions.Tests.Core.ItTests;

public sealed class EquivalentShould
{
	[Fact]
	public void BeTrueIfDefault()
	{
		var setupValue = new ClassObject();
		var inputValue = new ClassObject();

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeTrueIfAllPropertiesEqual()
	{
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new ClassObject
		{
			Name = name,
			Age = age,
		};

		var inputValue = new ClassObject
		{
			Name = name,
			Age = age,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual1()
	{
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		const int age = 17;

		var setupValue = new ClassObject
		{
			Name = name1,
			Age = age,
		};

		var inputValue = new ClassObject
		{
			Name = name2,
			Age = age,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual2()
	{
		const string name = "Okayama Issei";
		const int age1 = 17, age2 = 18;

		var setupValue = new ClassObject
		{
			Name = name,
			Age = age1,
		};

		var inputValue = new ClassObject
		{
			Name = name,
			Age = age2,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual3()
	{
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new ClassObject
		{
			Name = null,
			Age = age,
		};

		var inputValue = new ClassObject
		{
			Name = name,
			Age = age,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual4()
	{
		const string name = "Okayama Issei";
		const int age = 17;

		var setupValue = new ClassObject
		{
			Name = name,
			Age = age,
		};

		var inputValue = new ClassObject
		{
			Name = null,
			Age = age,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
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

		var setupValue = new ClassNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new ClassObject
			{
				Name = name,
				Age = age,
			},
		};

		var inputValue = new ClassNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new ClassObject
			{
				Name = name,
				Age = age,
			},
		};

		var actual = It<ClassNestedObject>.Equivalent(setupValue)
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

		var setupValue = new ClassNestedObject
		{
			Price = price1,
			DateOfBirth = dateOfBirth,
			Child = new ClassObject
			{
				Name = name,
				Age = age,
			},
		};

		var inputValue = new ClassNestedObject
		{
			Price = price2,
			DateOfBirth = dateOfBirth,
			Child = new ClassObject
			{
				Name = name,
				Age = age,
			},
		};

		var actual = It<ClassNestedObject>.Equivalent(setupValue)
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

		var setupValue = new ClassNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth1,
			Child = new ClassObject
			{
				Name = name,
				Age = age,
			},
		};

		var inputValue = new ClassNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth2,
			Child = new ClassObject
			{
				Name = name,
				Age = age,
			},
		};

		var actual = It<ClassNestedObject>.Equivalent(setupValue)
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

		var setupValue = new ClassNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new ClassObject
			{
				Name = name1,
				Age = age,
			},
		};

		var inputValue = new ClassNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new ClassObject
			{
				Name = name2,
				Age = age,
			},
		};

		var actual = It<ClassNestedObject>.Equivalent(setupValue)
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

		var setupValue = new ClassNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new ClassObject
			{
				Name = name,
				Age = age1,
			},
		};

		var inputValue = new ClassNestedObject
		{
			Price = price,
			DateOfBirth = dateOfBirth,
			Child = new ClassObject
			{
				Name = name,
				Age = age2,
			},
		};

		var actual = It<ClassNestedObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeTrueForArrays()
	{
		throw new NotImplementedException();
	}

	[Fact]
	public void BeTrueForLists()
	{
		throw new NotImplementedException();
	}
}

file sealed class ClassObject
{
	public string? Name { get; init; }

	public int Age { get; init; }
}

file sealed class ClassNestedObject
{
	public decimal Price { get; init; }

	public DateTime DateOfBirth { get; init; }

	public ClassObject? Child { get; init; }
}
