namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class ReturnWithSeveralParametersShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
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

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input1, input2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(input1, input2);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithInvalidSequence1()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input1, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowWithInvalidSequence2()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input2, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowWithInvalidSequence3()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input2, input2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowWithDifferentInstancesInvalidSequence1()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input2, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowWithDifferentInstancesInvalidSequence2()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input1, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowWithDifferentInstancesInvalidSequence3()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input2, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input1, input2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForThrowsWithInvalidSequence1()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input1, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowForThrowsWithInvalidSequence2()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input2, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowForThrowsWithInvalidSequence3()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input2, input2);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowForThrowsWithDifferentInstancesInvalidSequence1()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input2, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowForThrowsWithDifferentInstancesInvalidSequence2()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input1, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowForThrowsWithDifferentInstancesInvalidSequence3()
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
			.SetupReturnWithSeveralParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(input2, input1);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService.ReturnWithSeveralParameters() method has not been set up", exception.Message);
	}
}
