namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithSeveralParametersGenericShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const float parameter1 = 2025f;
		const int parameter2 = 6;

		CreateFixture()
			.InvokeWithSeveralParameters(parameter1, parameter2);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyInvokeWithSeveralParameters(It<float>.Any(), default, Times.Never);
		DependencyServiceMock.VerifyInvokeWithSeveralParameters(It<string>.Any(), default, Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(any, any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const float parameter1 = 2025f;
		const int parameter2 = 6;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(parameter1, parameter2)
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
			.SetupInvokeWithSeveralParameters<float>(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParameters<float>(parameter1, parameter1);
	}

	[Fact]
	public void ExecuteWithInvalidSequence2()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParameters<float>(parameter2, parameter1);
	}

	[Fact]
	public void ExecuteWithInvalidSequence3()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParameters<float>(parameter2, parameter2);
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter = 0, anotherParameter = 1;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(parameter, parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters<float>(parameter, parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture()
			.InvokeWithSeveralParameters<float>(anotherParameter, anotherParameter);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(input1, It<int>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(input1, input2)
			.Throws(new IndexOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters<float>(input1, input2);

		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(input1, It<int>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters<float>(input1, anotherParameter);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(input1, It<int>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters<float>(anotherParameter, input2);

		var exception = Assert.Throws<OutOfMemoryException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(input1, It<int>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParameters<float>(anotherParameter, anotherParameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(parameterValue1, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(parameterValue2, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;
		var verify1 = It<float>.Where(x => x > 0);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;
		var verify1 = It<float>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);

		var actual = () =>
		{
			var verify1 = It<float>.Any();
			DependencyServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(any, 234) to be called 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 234)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue1);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(parameterValue1, parameterValue1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(parameterValue2, parameterValue2, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(Single, Int32) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(123, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 123)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<int> verifyInt1 = It<int>.Where(x => x < 200), verifyInt2 = It<int>.Where(x => x > 200);
			It<float> verifyFloat1 = It<float>.Where(x => x < 200), verifyFloat2 = It<float>.Where(x => x > 200);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat1, verifyInt2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat2, verifyInt2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat2, verifyInt1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<int> verifyInt1 = It<int>.Equivalent(parameterValue1), verifyInt2 = It<int>.Equivalent(parameterValue2);
			It<float> verifyFloat1 = It<float>.Equivalent(parameterValue1), verifyFloat2 = It<float>.Equivalent(parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat1, verifyInt2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat2, verifyInt2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat2, verifyInt1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			var verifyInt = It<int>.Any();
			var verifyFloat = It<float>.Any();
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat, verifyInt);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat, verifyInt);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat, verifyInt);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verifyInt1 = It<int>.Where(x => x < 200), verifyInt2 = It<int>.Where(x => x > 200);
			It<float> verifyFloat1 = It<float>.Where(x => x < 200), verifyFloat2 = It<float>.Where(x => x > 200);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat2, verifyInt2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat2, verifyInt1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat1, verifyInt2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(where(predicate), where(predicate)) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceEquivalent()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verifyInt1 = It<int>.Equivalent(parameterValue1), verifyInt2 = It<int>.Equivalent(parameterValue2);
			It<float> verifyFloat1 = It<float>.Equivalent(parameterValue1), verifyFloat2 = It<float>.Equivalent(parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat2, verifyInt2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat1, verifyInt2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat2, verifyInt1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 123)
			  - parameter1:
			    expected: 123
			    actual: 234
			  - parameter2:
			    expected: 234
			    actual: 123
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			var verifyInt = It<int>.Any();
			var verifyFloat = It<float>.Any();
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat, verifyInt);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat, verifyInt);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat, verifyInt);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(verifyFloat, verifyInt);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(any, any) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue2);
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(parameterValue2, parameterValue1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParameters<Single>(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0f;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);

		var actual2 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0f;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0f;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0f;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParameters<float>(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void SetupDifferentTypes()
	{
		const string errorMessage = nameof(errorMessage);
		const string stringValue = nameof(stringValue);
		const float floatValue = 123f;
		const int intValue = 456;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<float>()
			.Throws(new NullReferenceException(errorMessage));

		DependencyServiceMock
			.SetupInvokeWithSeveralParameters<string>()
			.Callback((x, _) => callback += x.Length);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<string>(stringValue, intValue);

		var actual = () => fixture.InvokeWithSeveralParameters(floatValue, intValue);
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
		const int intParam = 456;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<string>(stringParam, intParam);
		fixture.InvokeWithSeveralParameters(decimalParam, intParam);
		fixture.InvokeWithSeveralParameters(floatParam, intParam);
		fixture.InvokeWithSeveralParameters<string>(stringParam, intParam);

		DependencyServiceMock.VerifyInvokeWithSeveralParameters<string>(stringParam, intParam, Times.Exactly(2));
		DependencyServiceMock.VerifyInvokeWithSeveralParameters<decimal>(decimalParam, intParam, Times.Once);
		DependencyServiceMock.VerifyInvokeWithSeveralParameters<float>(floatParam, intParam, Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyDifferentTypesInSequence()
	{
		const float floatParam = 123f;
		const string stringParam = nameof(stringParam);
		const decimal decimalParam = 123m;
		const int intParam = 456;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters<string>(stringParam, intParam);
		fixture.InvokeWithSeveralParameters(decimalParam, intParam);
		fixture.InvokeWithSeveralParameters(floatParam, intParam);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<string>(stringParam, intParam);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<decimal>(decimalParam, intParam);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters<float>(floatParam, intParam);
		});
		VerifyNoOtherCalls();
	}
}
