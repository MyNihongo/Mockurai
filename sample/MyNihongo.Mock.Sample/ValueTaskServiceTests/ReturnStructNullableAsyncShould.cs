// ReSharper disable AccessToDisposedClosure

namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;

public sealed class ReturnStructNullableAsyncShould : ValueTaskServiceTestsBase
{
	[Fact]
	public async Task ReturnNullWithoutSetup()
	{
		using var cts = new CancellationTokenSource();

		var actual = await CreateFixture()
			.ReturnStructNullableAsync(cts.Token);

		Assert.Null(actual);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		using var cts = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnStructNullableAsync(cts.Token)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 18)
			));

		var actual = await CreateFixture()
			.ReturnStructNullableAsync(cts.Token);

		const string expected = "name:Okayama Issei;age:12;yob:2025";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ReturnNullWithSetupAnotherInstance()
	{
		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
		using var ctsInput = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnStructAsync(ctsSetup.Token)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 18)
			));

		var actual = await CreateFixture()
			.ReturnStructNullableAsync(ctsInput.Token);

		Assert.Null(actual);
	}

	[Fact]
	public async Task ThrowsWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		using var cts = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnStructNullableAsync(cts.Token)
			.Throws(new InvalidOperationException(errorMessage));

		Func<Task> actual = async () => await CreateFixture()
			.ReturnStructNullableAsync(cts.Token);

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
			.SetupReturnStructAsync(ctsSetup.Token)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = await CreateFixture()
			.ReturnStructNullableAsync(ctsInput.Token);

		Assert.Null(actual);
	}
}
