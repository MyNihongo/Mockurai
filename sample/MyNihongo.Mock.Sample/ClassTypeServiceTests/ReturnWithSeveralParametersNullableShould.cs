namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class ReturnWithSeveralParametersNullableShould : ClassTypeServiceTestsBase
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
			.ReturnWithSeveralParametersNullable(input1, input2);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input1, input2);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input1, input1);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input2, input1);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input2, input2);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input2, input1);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input1, input1);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input2, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ThrowCustomException()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParametersNullable(input1, input2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnNullThrowsWithInvalidSequence1()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input1, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullThrowsWithInvalidSequence2()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input2, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullThrowsWithInvalidSequence3()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input2, input2);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullThrowsWithDifferentInstancesInvalidSequence1()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input2, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullThrowsWithDifferentInstancesInvalidSequence2()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input1, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullThrowsWithDifferentInstancesInvalidSequence3()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupReturnWithSeveralParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersNullable(input2, input1);

		Assert.Null(actual);
	}
}
