namespace MyNihongo.Mock.Abstractions.Tests.Core.ItTests;

public sealed class EquivalentRecordShould
{
	[Fact]
	public void BeTrueIfDefault()
	{
		var setupValue = new RecordObject();
		var inputValue = new RecordObject();

		var actual = It<RecordObject>.Equivalent(setupValue)
			.ValueSetup.Check(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeTrueIfAllPropertiesEqual()
	{
		const string address = "Tokyo-to Nakano-ku Honcho";
		const bool isMarried = true;

		var setupValue = new RecordObject
		{
			IsMarried = isMarried,
			Address = address,
		};

		var inputValue = new RecordObject
		{
			IsMarried = isMarried,
			Address = address,
		};

		var actual = It<RecordObject>.Equivalent(setupValue)
			.ValueSetup.Check(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeFalseIfFieldNotEqual()
	{
		const string address = "Tokyo-to Nakano-ku Honcho";
		const bool isMarried1 = true, isMarried2 = false;

		var setupValue = new RecordObject
		{
			IsMarried = isMarried1,
			Address = address,
		};

		var inputValue = new RecordObject
		{
			IsMarried = isMarried2,
			Address = address,
		};

		var actual = It<RecordObject>.Equivalent(setupValue)
			.ValueSetup.Check(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfPropertyNotEqual()
	{
		const string address1 = "Tokyo-to Nakano-ku Honcho", address2 = "Tokyo-to Suginami-ku Ogikubo";
		const bool isMarried = true;

		var setupValue = new RecordObject
		{
			IsMarried = isMarried,
			Address = address1,
		};

		var inputValue = new RecordObject
		{
			IsMarried = isMarried,
			Address = address2,
		};

		var actual = It<RecordObject>.Equivalent(setupValue)
			.ValueSetup.Check(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeTrueForCollections()
	{
		const string address = "Tokyo-to Nakano-ku Honcho";
		const bool isMarried = true;

		var setupValue = new RecordObject[]
		{
			new()
			{
				IsMarried = isMarried,
				Address = address,
			},
			new()
			{
				IsMarried = isMarried,
				Address = address,
			},
			new()
			{
				IsMarried = isMarried,
				Address = address,
			},
		};

		var inputValue = new RecordObject[]
		{
			new()
			{
				IsMarried = isMarried,
				Address = address,
			},
			new()
			{
				IsMarried = isMarried,
				Address = address,
			},
			new()
			{
				IsMarried = isMarried,
				Address = address,
			},
		};

		var actual = It<RecordObject[]>.Equivalent(setupValue)
			.ValueSetup.Check(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeTrueForCollectionsPropertyNotEqual()
	{
		const string address1 = "Tokyo-to Nakano-ku Honcho", address2 = "Tokyo-to Suginami-ku Ogikubo";
		const bool isMarried = true;

		var setupValue = new RecordObject[]
		{
			new()
			{
				IsMarried = isMarried,
				Address = address1,
			},
			new()
			{
				IsMarried = isMarried,
				Address = address1,
			},
			new()
			{
				IsMarried = isMarried,
				Address = address1,
			},
		};

		var inputValue = new RecordObject[]
		{
			new()
			{
				IsMarried = isMarried,
				Address = address1,
			},
			new()
			{
				IsMarried = isMarried,
				Address = address1,
			},
			new()
			{
				IsMarried = isMarried,
				Address = address2,
			},
		};

		var actual = It<RecordObject[]>.Equivalent(setupValue)
			.ValueSetup.Check(inputValue);

		Assert.False(actual);
	}
}

file sealed record RecordObject
{
	public bool IsMarried;

	public string? Address { get; init; }
}
