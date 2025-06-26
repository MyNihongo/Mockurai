// // ReSharper disable AccessToDisposedClosure
//
// namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;
//
// public sealed class InvokeAsyncShould : ValueTaskServiceTestsBase
// {
// 	[Fact]
// 	public async Task ExecuteWithoutSetup()
// 	{
// 		using var cts = new CancellationTokenSource();
//
// 		await CreateFixture()
// 			.InvokeAsync(cts.Token);
// 	}
//
// 	[Fact]
// 	public async Task ThrowWithSetup()
// 	{
// 		const string errorMessage = nameof(errorMessage);
// 		using var cts = new CancellationTokenSource();
//
// 		ValueTaskDependencyServiceMock
// 			.SetupInvokeAsync(cts.Token)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		var actual = async () => await CreateFixture()
// 			.InvokeAsync(cts.Token);
//
// 		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public async Task ExecuteWithAnotherSetup()
// 	{
// 		const string errorMessage = nameof(errorMessage);
// 		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
// 		using var ctsInput = new CancellationTokenSource();
//
// 		ValueTaskDependencyServiceMock
// 			.SetupInvokeAsync(ctsSetup.Token)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		await CreateFixture()
// 			.InvokeAsync(ctsInput.Token);
// 	}
// }
