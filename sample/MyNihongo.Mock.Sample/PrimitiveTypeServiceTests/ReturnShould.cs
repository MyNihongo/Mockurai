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
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturn(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturn(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService#Return() to be called 1 time, but instead it was called 0 times.";
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
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.Return();
		fixture.Return();

		DependencyServiceMock.VerifyReturn(Times.Exactly(2));
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
			Expected IPrimitiveDependencyService#Return() to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1
			- 2
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
			Expected IPrimitiveDependencyService#ReturnWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: "value"
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
			Expected IPrimitiveDependencyService#Return() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1
			- 2
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

		const string expectedMessage = "Expected IPrimitiveDependencyService#InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
