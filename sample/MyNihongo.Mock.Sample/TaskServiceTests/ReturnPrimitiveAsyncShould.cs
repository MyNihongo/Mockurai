// ReSharper disable AccessToDisposedClosure

namespace MyNihongo.Mock.Sample.TaskServiceTests;

public sealed class ReturnPrimitiveAsyncShould : TaskServiceTestsBase
{
	[Fact]
	public async Task ThrowWithoutSetup()
	{
		using var cts = new CancellationTokenSource();

		Func<Task> actual = () => CreateFixture()
			.ReturnPrimitiveAsync(cts.Token);

		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
		Assert.Equal("ITaskDependencyService#ReturnPrimitiveAsync() method has not been set up", exception.Message);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		const int setupParameter = 1234;
		const decimal expected = 1236m;

		using var cts = new CancellationTokenSource();

		TaskDependencyServiceMock
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

		TaskDependencyServiceMock
			.SetupReturnPrimitiveAsync(ctsSetup.Token)
			.Returns(setupParameter);

		Func<Task> actual = () => CreateFixture()
			.ReturnPrimitiveAsync(ctsInput.Token);

		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
		Assert.Equal("ITaskDependencyService#ReturnPrimitiveAsync() method has not been set up", exception.Message);
	}

	[Fact]
	public async Task ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		using var cts = new CancellationTokenSource();

		TaskDependencyServiceMock
			.SetupReturnPrimitiveAsync(cts.Token)
			.Throws(new InvalidOperationException(errorMessage));

		Func<Task> actual = () => CreateFixture()
			.ReturnPrimitiveAsync(cts.Token);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowForThrowsWithSetupAnotherInstance()
	{
		const string errorMessage = nameof(errorMessage);
		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
		using var ctsInput = new CancellationTokenSource();

		TaskDependencyServiceMock
			.SetupReturnPrimitiveAsync(ctsSetup.Token)
			.Throws(new InvalidOperationException(errorMessage));

		Func<Task> actual = () => CreateFixture()
			.ReturnPrimitiveAsync(ctsInput.Token);

		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
		Assert.Equal("ITaskDependencyService#ReturnPrimitiveAsync() method has not been set up", exception.Message);
	}
}
