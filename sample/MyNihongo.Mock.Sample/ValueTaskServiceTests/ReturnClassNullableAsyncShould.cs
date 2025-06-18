namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;

public sealed class ReturnClassNullableAsyncShould : ValueTaskServiceTestsBase
{
	[Fact]
	public async Task ReturnNullWithoutSetup()
	{
		using var cts = new CancellationTokenSource();

		var actual = await CreateFixture()
			.ReturnClassNullableAsync(cts.Token);

		Assert.Null(actual);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		using var cts = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnClassNullableAsync(cts.Token)
			.Returns(new ClassReturn
			{
				Age = 12,
				Name = "Okayama Issei",
				DateOfBirth = new DateOnly(2025, 6, 18),
			});

		var actual = await CreateFixture()
			.ReturnClassNullableAsync(cts.Token);

		const string expected = "Okayama Issei is 12 years old, born in 2025";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ReturnNullWithSetupAnotherInstance()
	{
		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
		using var ctsInput = new CancellationTokenSource();

		ValueTaskDependencyServiceMock
			.SetupReturnClassAsync(ctsSetup.Token)
			.Returns(new ClassReturn
			{
				Age = 12,
				Name = "Okayama Issei",
				DateOfBirth = new DateOnly(2025, 6, 18),
			});

		var actual = await CreateFixture()
			.ReturnClassNullableAsync(ctsInput.Token);

		Assert.Null(actual);
	}
}
