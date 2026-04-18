namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests.DerivedTests;

public sealed class VerifyInSequenceShould : PrimitiveTypeServiceTestsDerivedBase
{
	[Fact]
	public void VerifyInvocationInOrder()
	{
		DependencyServiceDerivedMock.Object.Invoke();
		DependencyServiceMock.Object.Invoke();

		VerifyInSequence(x =>
		{
			x.DependencyServiceDerivedMock.Invoke();
			x.DependencyServiceMock.Invoke();
		});
	}

	[Fact]
	public void ThrowIfNotInOrder()
	{
		DependencyServiceDerivedMock.Object.Invoke();
		DependencyServiceMock.Object.Invoke();

		var actual = () => VerifyInSequence(x =>
		{
			x.DependencyServiceMock.Invoke();
			x.DependencyServiceDerivedMock.Invoke();
		});

		const string errorMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Invoke()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
