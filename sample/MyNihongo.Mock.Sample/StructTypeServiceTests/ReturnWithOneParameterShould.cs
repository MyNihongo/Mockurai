namespace MyNihongo.Mock.Sample.StructTypeServiceTests;

public sealed class ReturnWithOneParameterShould : StructTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
	{
		var input = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		Action actual = () => CreateFixture()
			.ReturnWithOneParameter(input);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IStructDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetupSameInstance()
	{
		var setupParameter = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Returns(new StructReturn(
				name: "Okayama Issei",
				age: 12,
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithOneParameter(setupParameter);

		const string expected = "name:Okayama Issei,age:12";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetupAnotherInstance()
	{
		var setupParameter = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithOneParameter(input);

		const string expected = "name:Okayama Issei,age:12";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetupAnotherSetup()
	{
		var setupParameter = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input = new StructParameter1
		{
			Number = 12345678,
			Text = "Another text",
		};

		StructDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		Action actual = () => CreateFixture()
			.ReturnWithOneParameter(input);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IStructDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowWithSetupSameInstance()
	{
		const string errorMessage = nameof(errorMessage);

		var setupParameter = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithOneParameter(setupParameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAnotherInstance()
	{
		const string errorMessage = nameof(errorMessage);

		var setupParameter = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithOneParameter(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForThrowsWithSetupAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		var setupParameter = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input = new StructParameter1
		{
			Number = 12345678,
			Text = "Another text",
		};

		StructDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithOneParameter(input);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IStructDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
	}
}
