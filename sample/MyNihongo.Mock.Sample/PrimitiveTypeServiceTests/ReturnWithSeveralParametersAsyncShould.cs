namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithSeveralParametersAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ReturnValueWithoutSetup()
	{
		const decimal expected = 0m;
		const int parameter1 = 2025, parameter2 = 6;

		var actual = await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter1, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(default, default, Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(any, any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter1, parameter2);

		Assert.Equal(resultSetup, actual);
	}

	[Fact]
	public async Task ReturnValueWithInvalidSequence1()
	{
		const decimal expected = 0m, resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter1, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ReturnValueWithInvalidSequence2()
	{
		const decimal expected = 0m, resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter2, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ReturnValueWithInvalidSequence3()
	{
		const decimal expected = 0m, resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter2, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = async () => await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter1, parameter2);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnValueForThrowsWithInvalidSequence1()
	{
		const decimal expected = 0m;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter1, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ReturnValueForThrowsWithInvalidSequence2()
	{
		const decimal expected = 0m;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter2, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ReturnValueForThrowsWithInvalidSequence3()
	{
		const decimal expected = 0m;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = await CreateFixture()
			.ReturnWithSeveralParametersAsync(parameter2, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task VerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(parameterValue1, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(parameterValue2, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyTimesWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(It<int>.Where(x => x > 0), parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyTimesAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(It<int>.Any(), parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowVerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);

		var actual = () =>
		{
			var verify1 = It<int>.Any();
			DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(verify1, parameterValue2, Times.Exactly(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(any, 234) to be called 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 234)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowVerifyNoOtherCalls()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue1);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(parameterValue1, parameterValue1, Times.Once);
		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(parameterValue2, parameterValue2, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(int, int) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(123, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyValidSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify2, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify2, verify1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceEquivalent()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(parameterValue1), verify2 = It<int>.Equivalent(parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify2, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify2, verify1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify, verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowInvalidSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify2, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify2, verify1);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify1, verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(where(predicate), where(predicate)) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceEquivalent()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(parameterValue1), verify2 = It<int>.Equivalent(parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify2, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify2, verify1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 123)
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
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(verify, verify);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(any, any) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidMethodInSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		await fixture.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
		await fixture.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue2);
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.ReturnWithSeveralParametersAsync(parameterValue2, parameterValue1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnDifferentValues()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback1()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(0m, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue1, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback2()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback3()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(2 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback4()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue2, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(3 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Equal(0m, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));

		var actual2 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception2 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception4 = await Assert.ThrowsAsync<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const int setValue1 = 2, setValue2 = 3;
		const decimal setupValue = 736m;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));
		Assert.Equal(setupValue, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const int setValue1 = 2, setValue2 = 3;
		const decimal setupValue = 736m;

		DependencyServiceMock
			.SetupReturnWithSeveralParametersAsync(It<int>.Any(), It<int>.Any())
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2));

		var actual2 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithSeveralParametersAsync(setValue1, setValue2); };
		var exception3 = await Assert.ThrowsAsync<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyServiceMock.VerifyReturnWithSeveralParametersAsync(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
