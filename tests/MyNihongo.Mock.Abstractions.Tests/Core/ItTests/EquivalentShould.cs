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
		var setupValue = new ClassObject
		{
			Name = "Okayama Issei",
			Age = 17,
		};

		var inputValue = new ClassObject
		{
			Name = "Okayama Issei",
			Age = 17,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual1()
	{
		var setupValue = new ClassObject
		{
			Name = "Okayama Issei",
			Age = 17,
		};

		var inputValue = new ClassObject
		{
			Name = "Okayama Issei2",
			Age = 17,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual2()
	{
		var setupValue = new ClassObject
		{
			Name = "Okayama Issei",
			Age = 17,
		};

		var inputValue = new ClassObject
		{
			Name = "Okayama Issei",
			Age = 18,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual3()
	{
		var setupValue = new ClassObject
		{
			Name = null,
			Age = 17,
		};

		var inputValue = new ClassObject
		{
			Name = "Okayama Issei",
			Age = 18,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
			.ValueSetup!.Value
			.Predicate(inputValue);

		Assert.False(actual);
	}

	[Fact]
	public void BeFalseIfIfPropertiesNotEqual4()
	{
		var setupValue = new ClassObject
		{
			Name = "Okayama Issei",
			Age = 17,
		};

		var inputValue = new ClassObject
		{
			Name = null,
			Age = 18,
		};

		var actual = It<ClassObject>.Equivalent(setupValue)
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
