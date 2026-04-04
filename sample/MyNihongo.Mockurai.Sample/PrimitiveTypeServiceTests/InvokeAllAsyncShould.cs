namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeAllAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ThrowOnVerifyNoOtherCalls()
	{
		await CreateFixture()
			.InvokeWhenAllAsync();

		var actual = VerifyNoOtherCalls;

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeAsync() to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.InvokeAsync()
			- 2: IPrimitiveDependencyService.InvokeAsync()
			- 3: IPrimitiveDependencyService.InvokeAsync()
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyTimes()
	{
		await CreateFixture()
			.InvokeWhenAllAsync();

		DependencyServiceMock.VerifyInvokeAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}