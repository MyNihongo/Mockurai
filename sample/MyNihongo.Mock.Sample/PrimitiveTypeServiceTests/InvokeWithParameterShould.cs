namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithParameterShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const string paramCustomerId = "qOp5SNt2LVqzQ9qS";

		CreateFixture()
			.InvokeWithParameter(paramCustomerId);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithParameter(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID";

		DependencyServiceMock
			.SetupInvokeWithParameter(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithParameter(paramCustomerId);
	}
}
