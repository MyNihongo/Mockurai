namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class GetOnlyShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const decimal expected = 0m;

		var actual = CreateFixture()
			.GetOnly;

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyGetGetOnly(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyGetGetOnly(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.GetOnly.get to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const int setupValue = 5;

		DependencyServiceMock
			.SetupGetGetOnly()
			.Returns(setupValue);

		var actual = CreateFixture()
			.GetOnly;

		Assert.Equal(setupValue, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupGetGetOnly()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => { _ = CreateFixture().GetOnly; };

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnly;
		_ = fixture.GetOnly;

		DependencyServiceMock.VerifyGetGetOnly(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnly;
		_ = fixture.GetOnly;

		var actual = () => DependencyServiceMock.VerifyGetGetOnly(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetOnly.get to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetOnly.get
			- 2: IPrimitiveDependencyService.GetOnly.get
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnly;
		fixture.ReturnWithParameter("value");

		DependencyServiceMock.VerifyGetGetOnly(Times.Once);

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
		_ = fixture.GetOnly;
		_ = fixture.GetOnly;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetGetOnly();
			ctx.DependencyServiceMock.GetGetOnly();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnly;
		_ = fixture.GetOnly;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetGetOnly();
			ctx.DependencyServiceMock.GetGetOnly();
			ctx.DependencyServiceMock.GetGetOnly();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetOnly.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetOnly.get
			- 2: IPrimitiveDependencyService.GetOnly.get
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnly;
		_ = fixture.GetOnly;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetGetOnly();
			ctx.DependencyServiceMock.GetGetOnly();
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetOnly.get
			- 2: IPrimitiveDependencyService.GetOnly.get
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
