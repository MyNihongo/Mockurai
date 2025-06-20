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

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter = 0, anotherParameter = 1;

		DependencyServiceMock
			.SetupInvokeWithParameter(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture()
			.InvokeWithParameter(anotherParameter);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int input = 123;

		DependencyServiceMock
			.SetupInvokeWithParameter(It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithParameter(input)
			.Throws(new ArgumentException(errorMessage2));

		var actual = () => CreateFixture()
			.InvokeWithParameter(input);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}
}
