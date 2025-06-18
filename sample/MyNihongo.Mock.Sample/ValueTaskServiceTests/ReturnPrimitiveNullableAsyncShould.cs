// ReSharper disable AccessToDisposedClosure

namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;

public sealed class ReturnPrimitiveNullableAsyncShould : ValueTaskServiceTestsBase
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

		ValueTaskDependencyServiceMock
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

		ValueTaskDependencyServiceMock
			.SetupReturnPrimitiveAsync(ctsSetup.Token)
			.Returns(setupParameter);

		var actual = await CreateFixture()
			.ReturnPrimitiveNullableAsync(ctsInput.Token);

		Assert.Null(actual);
	}

	[Fact]
	public async Task ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		using var cts = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnPrimitiveNullableAsync(cts.Token)
			.Throws(new InvalidOperationException(errorMessage));

		Func<Task> actual = async () => await CreateFixture()
			.ReturnPrimitiveNullableAsync(cts.Token);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnNullForThrowsWithSetupAnotherInstance()
	{
		const string errorMessage = nameof(errorMessage);
		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
		using var ctsInput = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnPrimitiveAsync(ctsSetup.Token)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = await CreateFixture()
			.ReturnPrimitiveNullableAsync(ctsInput.Token);

		Assert.Null(actual);
	}
}
