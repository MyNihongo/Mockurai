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

	[Fact]
	public void VerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(parameter1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify1 = It<string>.Where(x => x.EndsWith('1'));

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify = It<string>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () =>
		{
			var verify = It<string>.Any();
			DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.AtLeast(3));
		};

		const string exceptionMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: "parameter1"
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}
}
