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
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.Invoke();

		DependencyServiceMock.VerifyInvoke(Times.Exactly(2));
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
			Expected IPrimitiveDependencyService#Invoke() to be called 1 time, but instead it was called 2 times.
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
		fixture.Invoke();
		fixture.InvokeWithParameter("value");

		DependencyServiceMock.VerifyInvoke(Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: "value"
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
			Expected IPrimitiveDependencyService#Invoke() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1
			- 2
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
