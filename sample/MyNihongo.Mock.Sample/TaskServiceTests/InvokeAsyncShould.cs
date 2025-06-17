namespace MyNihongo.Mock.Sample.TaskServiceTests;

public sealed class InvokeAsyncShould : TaskServiceTestsBase
{
	[Fact]
	public async Task NotImplemented()
	{
		using var cts = new CancellationTokenSource();

		await CreateFixture()
			.InvokeAsync(cts.Token);

		throw new NotImplementedException();
	}
}
