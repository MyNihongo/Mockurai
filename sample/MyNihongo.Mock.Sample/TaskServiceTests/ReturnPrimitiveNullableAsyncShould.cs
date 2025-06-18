namespace MyNihongo.Mock.Sample.TaskServiceTests;

public sealed class ReturnPrimitiveNullableAsyncShould : TaskServiceTestsBase
{
	[Fact]
	public async Task ReturnNullWithoutSetup()
	{
		using var cts = new CancellationTokenSource();

		var actual = await CreateFixture()
			.ReturnPrimitiveNullableAsync(cts.Token);

		Assert.Null(actual);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		const int setupParameter = 1234;
		const int expected = 1236;

		using var cts = new CancellationTokenSource();

		TaskDependencyServiceMock
			.SetupReturnPrimitiveNullableAsync(cts.Token)
			.Returns(setupParameter);

		var actual = await CreateFixture()
			.ReturnPrimitiveNullableAsync(cts.Token);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ReturnNullWithSetupAnotherInstance()
	{
		const int setupParameter = 1234;

		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
		using var ctsInput = new CancellationTokenSource();

		TaskDependencyServiceMock
			.SetupReturnPrimitiveAsync(ctsSetup.Token)
			.Returns(setupParameter);

		var actual = await CreateFixture()
			.ReturnPrimitiveNullableAsync(ctsInput.Token);

		Assert.Null(actual);
	}
}
