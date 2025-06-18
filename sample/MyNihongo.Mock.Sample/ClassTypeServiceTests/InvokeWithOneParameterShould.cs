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
			.InvokeWithParameter(input);
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
			.SetupInvokeWithParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(setupParameter);

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
			.SetupInvokeWithParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithParameter(input);
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
			.SetupInvokeWithParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithParameter(input);
	}
}
