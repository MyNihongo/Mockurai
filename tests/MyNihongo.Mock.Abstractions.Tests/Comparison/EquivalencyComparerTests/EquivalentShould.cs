namespace MyNihongo.Mock.Abstractions.Tests.Comparison.EquivalencyComparerTests;

public sealed class EquivalentShould
{
	[Fact]
	public void BeEmptyIfIntEquals()
	{
		const int input1 = 1, input2 = 1;

		var actual = EquivalencyComparer<int>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void HaveEntryIfIntNotEqual()
	{
		const int input1 = 1, input2 = 2;

		var actual = EquivalencyComparer<int>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("this", "1", "2"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void BeEmptyIfStringEquals()
	{
		const string input1 = "input", input2 = "input";

		var actual = EquivalencyComparer<string>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void HaveEntryIfStringNotEqual()
	{
		const string input1 = nameof(input1), input2 = nameof(input2);

		var actual = EquivalencyComparer<string>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("this", "input1", "input2"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

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

	[Fact]
	public void HaveEntriesIfTwoInvalid()
	{
		const int age1 = 17, age2 = 18;
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		var dateOfBirth = new DateOnly(2024, 6, 29);
		var dateTimeUpdated = new DateTime(2025, 7, 30, 18, 23, 32);

		var input1 = new ClassObject
		{
			Age = age1,
			DateTimeUpdated = dateTimeUpdated,
			Name = name1,
			DateOfBirth = dateOfBirth,
		};

		var input2 = new ClassObject
		{
			Age = age2,
			DateTimeUpdated = dateTimeUpdated,
			Name = name2,
			DateOfBirth = dateOfBirth,
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Age", "17", "18"),
			new("Name", "Okayama Issei", "Okayama Issei2"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntriesIfThreeInvalid()
	{
		const int age1 = 17, age2 = 18;
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		DateOnly dateOfBirth1 = new(2024, 6, 29), dateOfBirth2 = new(2024, 6, 30);
		var dateTimeUpdated = new DateTime(2025, 7, 30, 18, 23, 32);

		var input1 = new ClassObject
		{
			Age = age1,
			DateTimeUpdated = dateTimeUpdated,
			Name = name1,
			DateOfBirth = dateOfBirth1,
		};

		var input2 = new ClassObject
		{
			Age = age2,
			DateTimeUpdated = dateTimeUpdated,
			Name = name2,
			DateOfBirth = dateOfBirth2,
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Age", "17", "18"),
			new("Name", "Okayama Issei", "Okayama Issei2"),
			new("DateOfBirth", "2024/06/29", "2024/06/30"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntriesIfFourInvalid()
	{
		const int age1 = 17, age2 = 18;
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		DateOnly dateOfBirth1 = new(2024, 6, 29), dateOfBirth2 = new(2024, 6, 30);
		DateTime dateTimeUpdated1 = new(2025, 7, 30, 18, 23, 32), dateTimeUpdated2 = new(2025, 7, 30, 18, 23, 33);

		var input1 = new ClassObject
		{
			Age = age1,
			DateTimeUpdated = dateTimeUpdated1,
			Name = name1,
			DateOfBirth = dateOfBirth1,
		};

		var input2 = new ClassObject
		{
			Age = age2,
			DateTimeUpdated = dateTimeUpdated2,
			Name = name2,
			DateOfBirth = dateOfBirth2,
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Age", "17", "18"),
			new("Name", "Okayama Issei", "Okayama Issei2"),
			new("DateOfBirth", "2024/06/29", "2024/06/30"),
			new("DateTimeUpdated", "2025/07/30 18:23:32", "2025/07/30 18:23:33"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfClassNotEquals1()
	{
		var input1 = new ClassObject
		{
			Parent = null,
		};

		var input2 = new ClassObject
		{
			Parent = new ClassObject.Item
			{
				Ticks = 123L,
			},
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Parent", "null", "Item"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfClassNotEquals2()
	{
		var input1 = new ClassObject
		{
			Parent = new ClassObject.Item
			{
				Ticks = 123L,
			},
		};

		var input2 = new ClassObject
		{
			Parent = null,
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Parent", "Item", "null"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void BeEmptyIfNestedEquals()
	{
		const long ticks = 1234567890L;
		const bool isValid = true;

		var input1 = new ClassObject
		{
			Parent = new ClassObject.Item
			{
				Ticks = ticks,
				IsValid = isValid,
			},
		};

		var input2 = new ClassObject
		{
			Parent = new ClassObject.Item
			{
				Ticks = ticks,
				IsValid = isValid,
			},
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void HaveOneEntryIfNestedNotEquals()
	{
		const long ticks1 = 1234567890L, ticks2 = 2345678901L;
		const bool isValid = true;

		var input1 = new ClassObject
		{
			Parent = new ClassObject.Item
			{
				Ticks = ticks1,
				IsValid = isValid,
			},
		};

		var input2 = new ClassObject
		{
			Parent = new ClassObject.Item
			{
				Ticks = ticks2,
				IsValid = isValid,
			},
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Parent.Ticks", "1234567890", "2345678901"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntriesIfNestedNotEquals2()
	{
		const long ticks1 = 1234567890L, ticks2 = 2345678901L;
		const bool isValid1 = true, isValid2 = false;

		var input1 = new ClassObject
		{
			Parent = new ClassObject.Item
			{
				Ticks = ticks1,
				IsValid = isValid1,
			},
		};

		var input2 = new ClassObject
		{
			Parent = new ClassObject.Item
			{
				Ticks = ticks2,
				IsValid = isValid2,
			},
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Parent.Ticks", "1234567890", "2345678901"),
			new("Parent.IsValid", "True", "False"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfListNotEquals1()
	{
		var input1 = new ClassObject
		{
			Children = [],
		};

		var input2 = new ClassObject
		{
			Children = null,
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Children", "[ClassObject]", "null"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfListNotEquals2()
	{
		var input1 = new ClassObject
		{
			Children = null,
		};

		var input2 = new ClassObject
		{
			Children = [],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Children", "null", "[ClassObject]"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void BeEmptyIfListEmpty()
	{
		var input1 = new ClassObject
		{
			Children = [],
		};

		var input2 = new ClassObject
		{
			Children = [],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void BeEmptyIfListItemEqual()
	{
		const long ticks = 1234567890L;
		const bool isValid = true;

		var input1 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks,
					IsValid = isValid,
				},
			],
		};

		var input2 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks,
					IsValid = isValid,
				},
			],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void HaveEntryIfListItemNotEqual1()
	{
		const long ticks1 = 1234567890L, ticks2 = 2345678901L;
		const bool isValid = true;

		var input1 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks1,
					IsValid = isValid,
				},
			],
		};

		var input2 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks2,
					IsValid = isValid,
				},
			],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Children[0].Ticks", "1234567890", "2345678901"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfListItemNotEqual2()
	{
		const long ticks1 = 1234567890L, ticks2 = 2345678901L;
		const bool isValid1 = true, isValid2 = false;

		var input1 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks1,
					IsValid = isValid1,
				},
			],
		};

		var input2 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks2,
					IsValid = isValid2,
				},
			],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Children[0].Ticks", "1234567890", "2345678901"),
			new("Children[0].IsValid", "True", "False"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfListItemNotEqual3()
	{
		const long ticks1 = 1234567890L, ticks2 = 2345678901L;
		const bool isValid1 = true, isValid2 = false;

		var input1 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks1,
					IsValid = isValid1,
				},
				new ClassObject.Item
				{
					Ticks = ticks1,
					IsValid = isValid1,
				},
			],
		};

		var input2 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks2,
					IsValid = isValid2,
				},
			],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Children[0].Ticks", "1234567890", "2345678901"),
			new("Children[0].IsValid", "True", "False"),
			new("Children", "collection with at least 2 elements", "collection with 1 elements"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfListItemNotEqual4()
	{
		const long ticks1 = 1234567890L, ticks2 = 2345678901L;
		const bool isValid1 = true, isValid2 = false;

		var input1 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks1,
					IsValid = isValid1,
				},
				new ClassObject.Item
				{
					Ticks = ticks1,
					IsValid = isValid1,
				},
			],
		};

		var input2 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks1,
					IsValid = isValid2,
				},
				new ClassObject.Item
				{
					Ticks = ticks2,
					IsValid = isValid1,
				},
			],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Children[1].Ticks", "1234567890", "2345678901"),
			new("Children[0].IsValid", "True", "False"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfListItemNestedNotEqual()
	{
		const int age1 = 17, age2 = 18;
		const long ticks = 1234567890L;
		const bool isValid = true;

		var input1 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks,
					IsValid = isValid,
					Object = new ClassObject
					{
						Age = age1,
					},
				},
			],
		};

		var input2 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks,
					IsValid = isValid,
					Object = new ClassObject
					{
						Age = age2,
					},
				},
			],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Children[0].Object.Age", "17", "18"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void HaveEntryIfListItemNestedListNotEqual()
	{
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";
		const long ticks = 1234567890L;
		const bool isValid = true;

		var input1 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks,
					IsValid = isValid,
					Objects =
					[
						new ClassObject
						{
							Name = name1,
						},
						new ClassObject
						{
							Name = name1,
						},
					],
				},
			],
		};

		var input2 = new ClassObject
		{
			Children =
			[
				new ClassObject.Item
				{
					Ticks = ticks,
					IsValid = isValid,
					Objects =
					[
						new ClassObject
						{
							Name = name1,
						},
						new ClassObject
						{
							Name = name2,
						},
					],
				},
			],
		};

		var actual = EquivalencyComparer<ClassObject>.Default.Equivalent(input1, input2);
		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("Children[0].Objects[1].Name", "Okayama Issei", "Okayama Issei2"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void BeEmptyIfArrayStringEquals()
	{
		const string value1 = nameof(value1), value2 = nameof(value2);

		var input1 = new[]
		{
			value1,
			value2,
		};

		var input2 = new[]
		{
			value1,
			value2,
		};

		var actual = EquivalencyComparer<string[]>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void HaveEntryIfArrayStringNotEquals()
	{
		const string value1 = nameof(value1), value2 = nameof(value2);

		var input1 = new[]
		{
			value1,
			value1,
		};

		var input2 = new[]
		{
			value1,
			value2,
		};

		var actual = EquivalencyComparer<string[]>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("this[1]", "value1", "value2"),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	[Fact]
	public void BeEmptyIfArrayEquals()
	{
		const int age = 17;
		const string name = "Okayama Issei";

		var input1 = new ClassObject[]
		{
			new()
			{
				Age = age,
				Name = name,
			},
			new()
			{
				Age = age,
				Name = name,
			},
		};

		var input2 = new ClassObject[]
		{
			new()
			{
				Age = age,
				Name = name,
			},
			new()
			{
				Age = age,
				Name = name,
			},
		};

		var actual = EquivalencyComparer<ClassObject[]>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void HaveEntryIfArrayNotEquals()
	{
		const int age1 = 17, age2 = 18;
		const string name1 = "Okayama Issei", name2 = "Okayama Issei2";

		var input1 = new ClassObject[]
		{
			new()
			{
				Age = age1,
				Name = name1,
			},
			new()
			{
				Age = age2,
				Name = name2,
			},
		};

		var input2 = new ClassObject[]
		{
			new()
			{
				Age = age2,
				Name = name1,
			},
			new()
			{
				Age = age2,
				Name = name1,
			},
		};

		var actual = EquivalencyComparer<ClassObject[]>.Default.Equivalent(input1, input2);

		var expected = new EquivalencyComparerResult.Entry[]
		{
			new("this[0].Age", "17", "18"),
			new("this[1].Name", "Okayama Issei2", "Okayama Issei"),
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

	public ListOfClass? Children { get; init; }

	public override string ToString()
	{
		return nameof(ClassObject);
	}

	public sealed class Item
	{
		public long Ticks;
		public ClassObject[]? Objects;

		public bool IsValid { get; init; }

		public ClassObject? Object { get; init; }

		public override string ToString()
		{
			return nameof(Item);
		}
	}
}

file sealed class ListOfClass : List<ClassObject.Item>
{
	public override string ToString()
	{
		return $"[{nameof(ClassObject)}]";
	}
}
