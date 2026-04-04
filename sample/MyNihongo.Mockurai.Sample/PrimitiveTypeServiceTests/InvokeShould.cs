namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		CreateFixture()
			.Invoke();
	}

	[Fact]
	public void ExecuteWithoutSetupOut()
	{
		CreateFixture()
			.Invoke(out _);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyInvoke(Times.Never);
		DependencyServiceMock.VerifyInvoke(ItOut<int>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyInvoke(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.Invoke() to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotCalledOut()
	{
		var actual = () => DependencyServiceMock.VerifyInvoke(ItOut<int>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.Invoke(out any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvoke()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.Invoke();

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupOut()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvoke(ItOut<int>.Any())
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.Invoke(out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.Invoke();

		DependencyServiceMock.VerifyInvoke(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesOut()
	{
		var fixture = CreateFixture();
		fixture.Invoke(out _);
		fixture.Invoke(out _);

		DependencyServiceMock.VerifyInvoke(ItOut<int>.Any(), Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.Invoke();

		var actual = () => DependencyServiceMock.VerifyInvoke(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Invoke()
			- 2: IPrimitiveDependencyService.Invoke()
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyTimesOut()
	{
		var fixture = CreateFixture();
		fixture.Invoke(out _);
		fixture.Invoke(out _);

		var actual = () => DependencyServiceMock.VerifyInvoke(ItOut<int>.Any(), Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke(out any) to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Invoke(out 0)
			- 2: IPrimitiveDependencyService.Invoke(out 0)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.InvokeWithParameter("value");

		DependencyServiceMock.VerifyInvoke(Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in "value")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsOut()
	{
		var fixture = CreateFixture();
		fixture.Invoke(out _);
		fixture.InvokeWithParameter("value");

		DependencyServiceMock.VerifyInvoke(ItOut<int>.Any(), Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in "value")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.Invoke();

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyServiceMock.Invoke();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceOut()
	{
		var fixture = CreateFixture();
		fixture.Invoke(out _);
		fixture.Invoke(out _);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke(ItOut<int>.Any());
			ctx.DependencyServiceMock.Invoke(ItOut<int>.Any());
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.Invoke();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyServiceMock.Invoke();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Invoke()
			- 2: IPrimitiveDependencyService.Invoke()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceOut()
	{
		var fixture = CreateFixture();
		fixture.Invoke(out _);
		fixture.Invoke(out _);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke(ItOut<int>.Any());
			ctx.DependencyServiceMock.Invoke(ItOut<int>.Any());
			ctx.DependencyServiceMock.Invoke(ItOut<int>.Any());
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke(out any) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Invoke(out 0)
			- 2: IPrimitiveDependencyService.Invoke(out 0)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.Invoke();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.Invoke();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return() to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Invoke()
			- 2: IPrimitiveDependencyService.Invoke()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceOut()
	{
		var fixture = CreateFixture();
		fixture.Invoke(out _);
		fixture.Invoke(out _);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke(ItOut<int>.Any());
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.Invoke(ItOut<int>.Any());
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return() to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Invoke(out 0)
			- 2: IPrimitiveDependencyService.Invoke(out 0)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyServiceMock
			.SetupInvoke()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.Invoke();
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke();
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyInvoke(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupInvoke()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.Invoke();

		var actual2 = () => fixture.Invoke();
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.Invoke();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.Invoke();
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyInvoke(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupInvoke()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.Invoke();
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke();
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyInvoke(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupInvoke()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => fixture.Invoke();
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke();
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		DependencyServiceMock.VerifyInvoke(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupInvoke()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => fixture.Invoke();
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke();
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		DependencyServiceMock.VerifyInvoke(Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
