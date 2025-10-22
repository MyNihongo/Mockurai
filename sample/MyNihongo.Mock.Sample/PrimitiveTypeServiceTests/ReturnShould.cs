namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const decimal expected = 0m;

		var actual = CreateFixture()
			.Return();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithoutSetupOut()
	{
		const bool expected = false;

		var actual = CreateFixture()
			.Return(out _);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturn(Times.Never);
		DependencyServiceMock.VerifyReturn(ItOut<string>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturn(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.Return() to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotCalledOut()
	{
		var actual = () => DependencyServiceMock.VerifyReturn(ItOut<string>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.Return(out any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const int setupCount = 5;
		const decimal expected = 5_000m;

		DependencyServiceMock
			.SetupReturn()
			.Returns(setupCount);

		var actual = CreateFixture()
			.Return();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetupOut()
	{
		const bool setupValue = true;

		DependencyServiceMock
			.SetupReturn(ItOut<string>.Any())
			.Returns(setupValue);

		var actual = CreateFixture()
			.Return(out _);

		Assert.Equal(setupValue, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturn()
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.Return();

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupOut()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturn(ItOut<string>.Any())
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.Return(out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.Return();
		fixture.Return();

		DependencyServiceMock.VerifyReturn(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesOut()
	{
		var fixture = CreateFixture();
		fixture.Return(out _);
		fixture.Return(out _);

		DependencyServiceMock.VerifyReturn(ItOut<string>.Any(), Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.Return();
		fixture.Return();

		var actual = () => DependencyServiceMock.VerifyReturn(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return() to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return()
			- 2: IPrimitiveDependencyService.Return()
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyTimesOut()
	{
		var fixture = CreateFixture();
		fixture.Return(out _);
		fixture.Return(out _);

		var actual = () => DependencyServiceMock.VerifyReturn(ItOut<string>.Any(), Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return(out any) to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return(out null)
			- 2: IPrimitiveDependencyService.Return(out null)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		fixture.Return();
		fixture.ReturnWithParameter("value");

		DependencyServiceMock.VerifyReturn(Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.ReturnWithParameter("value")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsOut()
	{
		var fixture = CreateFixture();
		fixture.Return(out _);
		fixture.ReturnWithParameter("value");

		DependencyServiceMock.VerifyReturn(ItOut<string>.Any(), Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.ReturnWithParameter("value")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		var fixture = CreateFixture();
		fixture.Return();
		fixture.Return();

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.Return();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceOut()
	{
		var fixture = CreateFixture();
		fixture.Return(out _);
		fixture.Return(out _);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return(ItOut<string>.Any());
			ctx.DependencyServiceMock.Return(ItOut<string>.Any());
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		fixture.Return();
		fixture.Return();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.Return();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return()
			- 2: IPrimitiveDependencyService.Return()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceOut()
	{
		var fixture = CreateFixture();
		fixture.Return(out _);
		fixture.Return(out _);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return(ItOut<string>.Any());
			ctx.DependencyServiceMock.Return(ItOut<string>.Any());
			ctx.DependencyServiceMock.Return(ItOut<string>.Any());
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return(out any) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return(out null)
			- 2: IPrimitiveDependencyService.Return(out null)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		fixture.Return();
		fixture.Return();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.Return();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return()
			- 2: IPrimitiveDependencyService.Return()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceOut()
	{
		var fixture = CreateFixture();
		fixture.Return(out _);
		fixture.Return(out _);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return(ItOut<string>.Any());
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.Return(ItOut<string>.Any());
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return(out null)
			- 2: IPrimitiveDependencyService.Return(out null)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
