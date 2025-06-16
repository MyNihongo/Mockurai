namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class ReturnWithOneParameterNullableShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
	{
		var setupParameter = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(setupParameter);

		Assert.Null(actual);
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
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(setupParameter);

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
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		Assert.Null(actual);
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
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Returns(new ClassReturn
			{
				Name = "Okayama Issei",
				Age = 12,
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		Assert.Null(actual);
	}
}
