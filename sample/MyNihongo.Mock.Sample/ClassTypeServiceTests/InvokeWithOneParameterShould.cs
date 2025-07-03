namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class InvokeWithOneParameterShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		var input = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		CreateFixture()
			.InvokeWithOneParameter(input);
	}

	[Fact]
	public void ThrowWithSetupSameInstance()
	{
		const string errorMessage = nameof(errorMessage);

		var setupParameter = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithOneParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithOneParameter(setupParameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithSetupAnotherInstance()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupInvokeWithOneParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithOneParameter(input);
	}

	[Fact]
	public void ExecuteWithSetupAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

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
			.SetupInvokeWithOneParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithOneParameter(input);
	}

	[Fact]
	public void ThrowIfSetupAnyImplicitly()
	{
		const string errorMessage = nameof(errorMessage);

		var input = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithOneParameter()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithOneParameter(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfSetupAnyExplicitly()
	{
		const string errorMessage = nameof(errorMessage);

		var input = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithOneParameter(It<ClassParameter1>.Any())
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithOneParameter(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
