// ReSharper disable AccessToDisposedClosure

namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;

public sealed class ReturnPrimitiveAsyncShould : ValueTaskServiceTestsBase
{
	[Fact]
	public async Task ThrowWithoutSetup()
	{
		using var cts = new CancellationTokenSource();

		Func<Task> actual = async () => await CreateFixture()
			.ReturnPrimitiveAsync(cts.Token);

		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
		Assert.Equal("IValueTaskDependencyService#ReturnPrimitiveAsync() method has not been set up", exception.Message);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		const int setupParameter = 1234;
		const decimal expected = 1236m;

		using var cts = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnPrimitiveAsync(cts.Token)
			.Returns(setupParameter);

		var actual = await CreateFixture()
			.ReturnPrimitiveAsync(cts.Token);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ThrowWithSetupAnotherInstance()
	{
		const int setupParameter = 1234; 

		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
		using var ctsInput = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnPrimitiveAsync(ctsSetup.Token)
			.Returns(setupParameter);

		Func<Task> actual = async () => await CreateFixture()
			.ReturnPrimitiveAsync(ctsInput.Token);

		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
		Assert.Equal("IValueTaskDependencyService#ReturnPrimitiveAsync() method has not been set up", exception.Message);
	}
}
