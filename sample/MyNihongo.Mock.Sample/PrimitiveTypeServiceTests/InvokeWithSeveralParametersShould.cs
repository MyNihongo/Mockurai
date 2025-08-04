namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithSeveralParametersShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const int parameter1 = 2025, parameter2 = 6;

		CreateFixture()
			.InvokeWithSeveralParameters(parameter1, parameter2);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters(parameter1, parameter2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithInvalidSequence1()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParameters(parameter1, parameter1);
	}

	[Fact]
	public void ExecuteWithInvalidSequence2()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParameters(parameter2, parameter1);
	}

	[Fact]
	public void ExecuteWithInvalidSequence3()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParameters(parameter2, parameter2);
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter = 0, anotherParameter = 1;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter, parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters(parameter, parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture()
			.InvokeWithSeveralParameters(anotherParameter, anotherParameter);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters()
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(input1)
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(input1, input2)
			.Throws(new IndexOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters(input1, input2);

		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters()
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(input1)
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters(input1, anotherParameter);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters()
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(input1)
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters(anotherParameter, input2);

		var exception = Assert.Throws<OutOfMemoryException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters()
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(input1)
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters(anotherParameter, anotherParameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters(parameterValue1, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyInvokeWithSeveralParameters(parameterValue2, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;
		var verify1 = It<int>.Where(x => x > 0);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;
		var verify1 = It<int>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters(parameterValue2, parameterValue2);

		var actual = () =>
		{
			var verify1 = It<int>.Any();
			DependencyServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(3));
		};

		const string exceptionMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithSeveralParameters(any, 234) to be called 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: 123, 234
			- 2: 234, 234
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters(parameterValue2, parameterValue1);
		fixture.InvokeWithSeveralParameters(parameterValue1, parameterValue1);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters(parameterValue1, parameterValue1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithSeveralParameters(parameterValue2, parameterValue2, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithSeveralParameters(Int32, Int32) to be verified, but the following invocations have not been verified:
			- 1: 123, 234
			- 3: 234, 123
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}
}
