namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;

public sealed class InvokeAsyncShould : ValueTaskServiceTestsBase
{
	[Fact]
	public async Task ExecuteWithoutSetup()
	{
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.InvokeAsync(cts.Token);
	}
}
