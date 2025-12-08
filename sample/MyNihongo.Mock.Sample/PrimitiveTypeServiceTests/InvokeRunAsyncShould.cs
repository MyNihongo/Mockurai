namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeRunAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ThrowOnVerifyNoOtherCalls()
	{
		await CreateFixture()
			.InvokeRunAsync();

		var actual = VerifyNoOtherCalls;

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.Invoke()
			- 2: IPrimitiveDependencyService.InvokeWithParameter(123)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyTimes()
	{
		await CreateFixture()
			.InvokeRunAsync();

		DependencyServiceMock.VerifyInvoke(Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter(123, Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifySequence()
	{
		await CreateFixture()
			.InvokeRunAsync();

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyServiceMock.InvokeWithParameter(123);
		});
		VerifyNoOtherCalls();
	}
}