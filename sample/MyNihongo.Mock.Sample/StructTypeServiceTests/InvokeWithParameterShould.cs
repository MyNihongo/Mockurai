namespace MyNihongo.Mock.Sample.StructTypeServiceTests;

public sealed class InvokeWithParameterShould : StructTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		var input = new StructParameter1
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

		var input = new StructParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		StructDependencyServiceMock
			.SetupInvokeWithParameter(input)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(input);

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
			.SetupInvokeWithParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithSetupAnotherSetup()
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
			.SetupInvokeWithParameter(setupParameter)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithParameter(input);
	}
}
