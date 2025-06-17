namespace MyNihongo.Mock.Sample.StructTypeServiceTests;

public sealed class ReturnWithMultipleParametersNullableShould : StructTypeServiceTestsBase
{
	[Fact]
	public void ReturnNullWithoutSetup()
	{
		var input1 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new StructParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input2);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		var input1 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new StructParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input2);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence1()
	{
		var input1 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new StructParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence2()
	{
		var input1 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new StructParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence3()
	{
		var input1 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new StructParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input2);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithDifferentInstancesInvalidSequence1()
	{
		var input1 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithDifferentInstancesInvalidSequence2()
	{
		var input1 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input1);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithDifferentInstancesInvalidSequence3()
	{
		var input1 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}
}
