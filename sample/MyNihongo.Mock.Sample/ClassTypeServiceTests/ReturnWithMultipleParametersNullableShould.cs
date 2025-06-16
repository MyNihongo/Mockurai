namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class ReturnWithMultipleParametersNullableShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ReturnNullWithoutSetup()
	{
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
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
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input2);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence1()
	{
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence2()
	{
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence3()
	{
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input2);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithDifferentInstancesInvalidSequence1()
	{
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithDifferentInstancesInvalidSequence2()
	{
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithDifferentInstancesInvalidSequence3()
	{
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		Assert.Null(actual);
	}
}
