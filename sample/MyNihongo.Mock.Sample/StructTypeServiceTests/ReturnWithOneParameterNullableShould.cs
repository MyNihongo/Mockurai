namespace MyNihongo.Mock.Sample.StructTypeServiceTests;

public sealed class ReturnWithOneParameterNullableShould : StructTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
	{
		var input = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithSetupSameInstance()
	{
		var input = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithOneParameterNullable(input)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

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
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		const string expected = "name:Okayama Issei,age:12";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullWithSetupAnotherSetup()
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
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		Assert.Null(actual);
	}

	[Fact]
	public void ThrowWithSetupSameInstance()
	{
		const string errorMessage = nameof(errorMessage);

		var input = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupReturnWithOneParameterNullable(input)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithOneParameterNullable(input);

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
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithOneParameterNullable(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnNullForThrowsWithSetupAnotherSetup()
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
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		Assert.Null(actual);
	}
}
