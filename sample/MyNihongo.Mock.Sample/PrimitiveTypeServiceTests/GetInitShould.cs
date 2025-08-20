namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class GetInitShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		var actual = CreateFixture()
			.GetInit;

		Assert.Empty(actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyGetGetInit(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyGetGetInit(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService#GetInit#get to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string setupValue = nameof(setupValue);

		DependencyServiceMock
			.SetupGetGetInit()
			.Returns(setupValue);

		var actual = CreateFixture()
			.GetInit;

		Assert.Equal(setupValue, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupGetGetInit()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => { _ = CreateFixture().GetInit; };

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		_ = fixture.GetInit;
		_ = fixture.GetInit;

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		_ = fixture.GetInit;
		_ = fixture.GetInit;

		var actual = () => DependencyServiceMock.VerifyGetGetInit(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#GetInit#get to be called 1 time, but instead it was called 2 times.
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
		_ = fixture.GetInit;
		fixture.ReturnWithParameter("value");

		DependencyServiceMock.VerifyGetGetInit(Times.Once);

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
		_ = fixture.GetInit;
		_ = fixture.GetInit;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.GetGetInit();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		_ = fixture.GetInit;
		_ = fixture.GetInit;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.GetGetInit();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#GetInit#get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1
			- 2
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
