namespace MyNihongo.Mock.Sample.TaskServiceTests;

public sealed class ReturnStructNullableAsyncShould : TaskServiceTestsBase
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

		TaskDependencyServiceMock
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

		TaskDependencyServiceMock
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
}
