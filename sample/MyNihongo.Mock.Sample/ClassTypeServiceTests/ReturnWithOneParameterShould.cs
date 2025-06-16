namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class ReturnWithOneParameterShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
	{
		var input = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		Action actual = () => CreateFixture()
			.ReturnWithOneParameter(input);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetupSameInstance()
	{
		var setupParameter = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithOneParameter(setupParameter);

		const string expected = "name:Okayama Issei,age:12";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetupAnotherInstance()
	{
		var setupParameter = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		Action actual = () => CreateFixture()
			.ReturnWithOneParameter(input);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAnotherSetup()
	{
		var setupParameter = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input = new ClassParameter1
		{
			Number = 12345678,
			Text = "Another text",
		};

		ClassDependencyServiceMock
			.SetupReturnWithOneParameter(setupParameter)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		Action actual = () => CreateFixture()
			.ReturnWithOneParameter(input);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
	}
}
