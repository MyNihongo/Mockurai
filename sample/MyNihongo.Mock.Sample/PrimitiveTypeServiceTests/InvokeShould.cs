namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

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
			Expected IPrimitiveDependencyService.InvokeWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.InvokeWithParameter("value")
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
			Expected IPrimitiveDependencyService.InvokeWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.InvokeWithParameter("value")
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
}
