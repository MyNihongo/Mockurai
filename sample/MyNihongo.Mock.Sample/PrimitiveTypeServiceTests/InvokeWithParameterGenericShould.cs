namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithParameterGenericShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const float parameter = 123f;

		CreateFixture()
			.InvokeWithParameter(parameter);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyInvokeWithParameter(It<float>.Any(), Times.Never);
		DependencyServiceMock.VerifyInvokeWithParameter(It<short>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyInvokeWithParameter(It<float>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.InvokeWithParameter<Single>(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const float parameter = 123f;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithParameter<float>(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAny()
	{
		const float parameter = 123f;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithParameter(It<float>.Any())
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

		const float paramCustomerId = 123f,
			setupCustomerId = 321f;

		DependencyServiceMock
			.SetupInvokeWithParameter<float>(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithParameter(paramCustomerId);
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const float parameter = 0, anotherParameter = 1;

		DependencyServiceMock
			.SetupInvokeWithParameter<float>(parameter)
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
		const float input = 123f;

		DependencyServiceMock
			.SetupInvokeWithParameter(It<float>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithParameter<float>(input)
			.Throws(new ArgumentException(errorMessage2));

		var actual = () => CreateFixture()
			.InvokeWithParameter(input);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter<float>(parameter1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter<float>(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const float parameter1 = 123f, parameter2 = 321f;
		var verify1 = It<float>.Where(x => x < 200f);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter<float>(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const float parameter1 = 123f, parameter2 = 321f;
		var verify = It<float>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () =>
		{
			var verify = It<float>.Any();
			DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter<Single>(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter<Single>(321)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter<float>(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter<Single>(Single) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.InvokeWithParameter<Single>(321)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter<float>(parameter1);
			ctx.DependencyServiceMock.InvokeWithParameter<float>(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Equivalent(parameter1), verify2 = It<float>.Equivalent(parameter2);
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<float>.Any();
			ctx.DependencyServiceMock.InvokeWithParameter(verify);
			ctx.DependencyServiceMock.InvokeWithParameter(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Where(x => x > 100f), verify2 = It<float>.Where(x => x > 300f);
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter<float>(parameter2);
			ctx.DependencyServiceMock.InvokeWithParameter<float>(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter<Single>(123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter<Single>(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Where(x => x > 300f), verify2 = It<float>.Where(x => x > 100f);
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter<Single>(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter<Single>(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter<float>(parameter1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.InvokeWithParameter<float>(parameter2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter<Single>(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceEquivalent()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithSeveralParameters(123, 321);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter<float>(parameter1);
			ctx.DependencyServiceMock.InvokeWithParameter(It<float>.Equivalent(234f));
			ctx.DependencyServiceMock.InvokeWithParameter<float>(parameter2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter<Single>(234) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321)
			- 3: IPrimitiveDependencyService.InvokeWithParameter<Single>(321)
			  expected: 234
			  actual: 321
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 2f;

		DependencyServiceMock
			.SetupInvokeWithParameter<float>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithParameter(setValue);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithParameter(setValue);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameter(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyInvokeWithParameter<float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 2f;
		var callback = 0f;

		DependencyServiceMock
			.SetupInvokeWithParameter<float>()
			.Callback(x => callback += x)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.InvokeWithParameter(setValue);

		var actual2 = () => fixture.InvokeWithParameter(setValue);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameter(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.InvokeWithParameter(setValue);
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifyInvokeWithParameter<float>(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 2f;
		var callback = 0f;

		DependencyServiceMock
			.SetupInvokeWithParameter<float>()
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithParameter(setValue);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithParameter(setValue);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameter(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifyInvokeWithParameter<float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 2;
		var callback = 0f;

		DependencyServiceMock
			.SetupInvokeWithParameter<float>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithParameter(setValue);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithParameter(setValue);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameter(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue, callback);

		DependencyServiceMock.VerifyInvokeWithParameter<float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 2f;
		var callback = 0f;

		DependencyServiceMock
			.SetupInvokeWithParameter<float>()
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithParameter(setValue);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithParameter(setValue);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameter(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue, callback);

		DependencyServiceMock.VerifyInvokeWithParameter<float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void SetupDifferentTypes()
	{
		const string errorMessage = nameof(errorMessage);
		const string stringValue = nameof(stringValue);
		const float floatValue = 123f;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithParameter<float>()
			.Throws(new NullReferenceException(errorMessage));

		DependencyServiceMock
			.SetupInvokeWithParameter<string>()
			.Callback(x => callback += x.Length);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter<string>(stringValue);

		var actual = () => fixture.InvokeWithParameter(floatValue);
		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		Assert.Equal(stringValue.Length, callback);
	}

	[Fact]
	public void VerifyDifferentTypes()
	{
		const float floatParam = 123f;
		const string stringParam = nameof(stringParam);
		const decimal decimalParam = 123m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter<string>(stringParam);
		fixture.InvokeWithParameter(decimalParam);
		fixture.InvokeWithParameter(floatParam);
		fixture.InvokeWithParameter<string>(stringParam);

		DependencyServiceMock.VerifyInvokeWithParameter<string>(stringParam, Times.Exactly(2));
		DependencyServiceMock.VerifyInvokeWithParameter<decimal>(decimalParam, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter<float>(floatParam, Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyDifferentTypesInSequence()
	{
		const float floatParam = 123f;
		const string stringParam = nameof(stringParam);
		const decimal decimalParam = 123m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter<string>(stringParam);
		fixture.InvokeWithParameter(decimalParam);
		fixture.InvokeWithParameter(floatParam);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter<string>(stringParam);
			ctx.DependencyServiceMock.InvokeWithParameter<decimal>(decimalParam);
			ctx.DependencyServiceMock.InvokeWithParameter<float>(floatParam);
		});
		VerifyNoOtherCalls();
	}
}
