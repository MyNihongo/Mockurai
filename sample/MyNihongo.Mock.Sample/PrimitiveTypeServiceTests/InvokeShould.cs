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
	public void VerifyCount()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.Invoke();

		DependencyServiceMock.VerifyInvoke(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyCount()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.Invoke();

		var actual = () => DependencyServiceMock.VerifyInvoke(Times.Once);

		const string exceptionMessage = "Expected IPrimitiveDependencyService#Invoke() to be called 1 time, but instead it was called 2 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.InvokeWithParameter("value");

		DependencyServiceMock.VerifyInvoke(Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: "value"
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}
}
