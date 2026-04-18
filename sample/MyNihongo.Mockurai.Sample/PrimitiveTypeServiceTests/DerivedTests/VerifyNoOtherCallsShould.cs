namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests.DerivedTests;

public sealed class VerifyNoOtherCallsShould : PrimitiveTypeServiceTestsDerivedBase
{
	[Fact]
	public void VerifyInvocations()
	{
		DependencyServiceDerivedMock.Object.Invoke();
		DependencyServiceMock.Object.Invoke();

		DependencyServiceMock.VerifyInvoke(Times.Once);
		DependencyServiceDerivedMock.VerifyInvoke(Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerified()
	{
		DependencyServiceDerivedMock.Object.Invoke();
		DependencyServiceMock.Object.Invoke();

		var actual = VerifyNoOtherCalls;

		const string errorMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.Invoke()
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
