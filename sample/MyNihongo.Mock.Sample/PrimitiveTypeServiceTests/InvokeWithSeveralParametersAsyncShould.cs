namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithSeveralParametersAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ExecuteWithoutSetup()
	{
		const int parameter1 = 2025, parameter2 = 6;

		await CreateFixture()
			.InvokeWithSeveralParametersAsync(parameter1, parameter2);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(It<int>.Any(), default, Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(any, any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersAsync(parameter1, parameter2);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ExecuteWithInvalidSequence1()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		await CreateFixture()
			.InvokeWithSeveralParametersAsync(parameter1, parameter1);
	}

	[Fact]
	public async Task ExecuteWithInvalidSequence2()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		await CreateFixture()
			.InvokeWithSeveralParametersAsync(parameter2, parameter1);
	}

	[Fact]
	public async Task ExecuteWithInvalidSequence3()
	{
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		await CreateFixture()
			.InvokeWithSeveralParametersAsync(parameter2, parameter2);
	}

	[Fact]
	public async Task NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter = 0, anotherParameter = 1;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(parameter, parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersAsync(parameter, parameter);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		await CreateFixture()
			.InvokeWithSeveralParametersAsync(anotherParameter, anotherParameter);
	}

	[Fact]
	public async Task TreatExactMatchesWithMorePriority1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(input1, It<int>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(input1, input2)
			.Throws(new IndexOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersAsync(input1, input2);

		var exception = await Assert.ThrowsAsync<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
	}

	[Fact]
	public async Task TreatExactMatchesWithMorePriority2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(input1, It<int>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersAsync(input1, anotherParameter);

		var exception = await Assert.ThrowsAsync<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public async Task TreatExactMatchesWithMorePriority3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(input1, It<int>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersAsync(anotherParameter, input2);

		var exception = await Assert.ThrowsAsync<OutOfMemoryException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public async Task TreatExactMatchesWithMorePriority4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const int input1 = 123, input2 = 234, anotherParameter = 987654;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(input1, It<int>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), parameter2: input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersAsync(anotherParameter, anotherParameter);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public async Task VerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(parameterValue1, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(parameterValue2, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyTimesWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(It<int>.Where(x => x > 0), parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyTimesAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(It<int>.Any(), parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowVerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);

		var actual = () =>
		{
			var verify1 = It<int>.Any();
			DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(verify1, parameterValue2, Times.Exactly(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(any, 234) to be called 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 234)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowVerifyNoOtherCalls()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue1);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(parameterValue1, parameterValue1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(parameterValue2, parameterValue2, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(Int32, Int32) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(123, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyValidSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify1, verify2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify2, verify2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify2, verify1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceEquivalent()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(parameterValue1), verify2 = It<int>.Equivalent(parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify1, verify2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify2, verify2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify2, verify1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify, verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowInvalidSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify2, verify2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify2, verify1);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify1, verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(where(predicate), where(predicate)) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceEquivalent()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(parameterValue1), verify2 = It<int>.Equivalent(parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify2, verify2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify1, verify2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify2, verify1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 123)
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
	public async Task ThrowInvalidSequenceAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(verify, verify);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(any, any) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidMethodInSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue2);
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.InvokeWithSeveralParametersAsync(parameterValue2, parameterValue1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		await fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);

		var actual2 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception2 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception4 = await Assert.ThrowsAsync<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersAsync(setValue1, setValue2);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyInvokeWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
