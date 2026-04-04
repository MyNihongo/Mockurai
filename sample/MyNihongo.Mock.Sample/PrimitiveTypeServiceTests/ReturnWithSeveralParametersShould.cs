namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithSeveralParametersShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const double expected = 0d;
		const int parameter1 = 2025, parameter2 = 6;

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(parameter1, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturnWithSeveralParameters(It<int>.Any(), It<int>.Any(), Times.Never);
		DependencyServiceMock.VerifyReturnWithSeveralParameters(ItRef<int>.Any(), It<int>.Any(), Times.Never);
		DependencyServiceMock.VerifyReturnWithSeveralParameters(It<int>.Any(), ItRef<int>.Any(), Times.Never);
		DependencyServiceMock.VerifyReturnWithSeveralParameters(ItRef<int>.Any(), ItRef<int>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturnWithSeveralParameters(It<int>.Any(), It<int>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(any, any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const double expected = 225d;
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(parameter1, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence1()
	{
		const double expected = 0d;
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(parameter1, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence2()
	{
		const double expected = 0d;
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(parameter2, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence3()
	{
		const double expected = 0d;
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(parameter2, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters(parameter1, parameter2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence1()
	{
		const double expected = 0d;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(parameter1, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence2()
	{
		const double expected = 0d;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(parameter2, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence3()
	{
		const double expected = 0d;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParameters(parameter2, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(parameterValue1, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyReturnWithSeveralParameters(parameterValue2, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;
		var verify1 = It<int>.Where(x => x > 0);

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(verify1, parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;
		var verify1 = It<int>.Any();

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(verify1, parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);

		var actual = () =>
		{
			var verify1 = It<int>.Any();
			DependencyServiceMock.VerifyReturnWithSeveralParameters(verify1, parameterValue2, Times.Exactly(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(any, 234) to be called 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 234)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue1);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(parameterValue1, parameterValue1, Times.Once);
		DependencyServiceMock.VerifyReturnWithSeveralParameters(parameterValue2, parameterValue2, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(int, int) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 123)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, parameterValue1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify2, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify2, verify1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(parameterValue1), verify2 = It<int>.Equivalent(parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify2, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify2, verify1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify, verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, parameterValue1);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceRef1()
	{
		int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(ref parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(ref parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(ItRef<int>.Value(parameterValue1), parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(ItRef<int>.Value(parameterValue2), parameterValue1);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(ItRef<int>.Value(parameterValue2), parameterValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 234, 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceRef2()
	{
		int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, ref parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, ref parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, ref parameterValue1);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue1, ItRef<int>.Value(parameterValue2));
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, ItRef<int>.Value(parameterValue1));
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, ItRef<int>.Value(parameterValue2));
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(234, ref 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, ref 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, ref 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, ref 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceRef3()
	{
		int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(ref parameterValue1, ref parameterValue2);
		fixture.ReturnWithSeveralParameters(ref parameterValue2, ref parameterValue2);
		fixture.ReturnWithSeveralParameters(ref parameterValue2, ref parameterValue1);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(ItRef<int>.Value(parameterValue1), ItRef<int>.Value(parameterValue2));
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(ItRef<int>.Value(parameterValue2), ItRef<int>.Value(parameterValue1));
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(ItRef<int>.Value(parameterValue2), ItRef<int>.Value(parameterValue2));
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 234, ref 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 123, ref 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 234, ref 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 234, ref 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify2, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify2, verify1);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify1, verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(where(predicate), where(predicate)) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceEquivalent()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(parameterValue1), verify2 = It<int>.Equivalent(parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify2, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify2, verify1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 123)
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
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify, verify);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(verify, verify);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters(any, any) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, parameterValue2);
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, parameterValue1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceRef()
	{
		int parameterValue1 = 123, parameterValue2 = 234;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters(parameterValue2, ref parameterValue2);
		fixture.ReturnWithSeveralParameters(ref parameterValue2, ref parameterValue1);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(ItRef<int>.Value(parameterValue1), parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(parameterValue2, ItRef<int>.Value(parameterValue2));
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.ReturnWithSeveralParameters(ItRef<int>.Value(parameterValue2), ItRef<int>.Value(parameterValue1));
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 123, 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters(234, ref 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 234, ref 123)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(Math.Pow(setupValue1, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(0d, fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue1, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(Math.Pow(setupValue1, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		Assert.Equal(Math.Pow(setupValue1, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(2 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		Assert.Equal(Math.Pow(setupValue1, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Math.Pow(setupValue2, 2), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(3 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Equal(0d, fixture.ReturnWithSeveralParameters(setValue1, setValue2));

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue1 = 2, setValue2 = 3;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * (setValue1 + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const int setValue1 = 2, setValue2 = 3;
		const decimal setupValue = 736m;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(Convert.ToDouble(setupValue * setupValue), fixture.ReturnWithSeveralParameters(setValue1, setValue2));
		Assert.Equal(Convert.ToDouble(setupValue * setupValue), fixture.ReturnWithSeveralParameters(setValue1, setValue2));

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const int setValue1 = 2, setValue2 = 3;
		const decimal setupValue = 736m;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters(It<int>.Any(), It<int>.Any())
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(Convert.ToDouble(setupValue * setupValue), fixture.ReturnWithSeveralParameters(setValue1, setValue2));

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters(setValue1, setValue2); };
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyServiceMock.VerifyReturnWithSeveralParameters(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
