namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithMultipleParametersShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const int parameter1 = 2025, parameter2 = 6;

		CreateFixture()
			.InvokeWithMultipleParameters(parameter1, parameter2);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupInvokeWithMultipleParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithMultipleParameters(parameter1, parameter2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithInvalidSequence1()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithMultipleParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(parameter1, parameter1);
	}

	[Fact]
	public void ExecuteWithInvalidSequence2()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithMultipleParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(parameter2, parameter1);
	}

	[Fact]
	public void ExecuteWithInvalidSequence3()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithMultipleParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(parameter2, parameter2);
	}
}
