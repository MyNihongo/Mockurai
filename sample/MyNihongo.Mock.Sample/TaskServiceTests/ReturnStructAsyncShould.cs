// ReSharper disable AccessToDisposedClosure
namespace MyNihongo.Mock.Sample.TaskServiceTests;

public sealed class ReturnStructAsyncShould : TaskServiceTestsBase
{
	[Fact]
	public async Task ThrowWithoutSetup()
	{
		using var cts = new CancellationTokenSource();

		Func<Task> actual = () => CreateFixture()
			.ReturnStructAsync(cts.Token);

		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
		Assert.Equal("ITaskDependencyService#ReturnStructAsync() method has not been set up", exception.Message);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		using var cts = new CancellationTokenSource();

		TaskDependencyServiceMock
			.SetupReturnStructAsync(cts.Token)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 18)
			));

		var actual = await CreateFixture()
			.ReturnStructAsync(cts.Token);

		const string expected = "name:Okayama Issei;age:12;yob:2025";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ThrowWithSetupAnotherInstance()
	{
		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
		using var ctsInput = new CancellationTokenSource();

		TaskDependencyServiceMock
			.SetupReturnStructAsync(ctsSetup.Token)
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 18)
			));

		Func<Task> actual = () => CreateFixture()
			.ReturnStructAsync(ctsInput.Token);

		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
		Assert.Equal("ITaskDependencyService#ReturnStructAsync() method has not been set up", exception.Message);
	}
}
