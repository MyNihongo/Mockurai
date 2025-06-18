namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;

public sealed class ReturnRecordNullableAsyncShould : ValueTaskServiceTestsBase
{
	[Fact]
	public async Task ReturnNullWithoutSetup()
	{
		using var cts = new CancellationTokenSource();

		var actual = await CreateFixture()
			.ReturnRecordNullableAsync(cts.Token);

		Assert.Null(actual);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		using var cts = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnRecordNullableAsync(cts.Token)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 18)
			));

		var actual = await CreateFixture()
			.ReturnRecordNullableAsync(cts.Token);

		const string expected = "Okayama Issei is 12 years old, born in 2025";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ReturnNullWithSetupAnotherInstance()
	{
		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
		using var ctsInput = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnRecordAsync(ctsSetup.Token)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 18)
			));

		var actual = await CreateFixture()
			.ReturnRecordNullableAsync(ctsInput.Token);

		Assert.Null(actual);
	}
}
