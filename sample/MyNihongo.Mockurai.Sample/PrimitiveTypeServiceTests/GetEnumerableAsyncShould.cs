namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class GetEnumerableAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ReturnValues()
	{
		string[] setupValues = ["Okayama", "Issei"];
		using var cts = new CancellationTokenSource();

		DependencyServiceMock
			.SetupGetEnumerableAsync()
			.Returns(setupValues.ToAsyncEnumerable());

		var actual = await CreateFixture()
			.GetEnumerableAsync(cts.Token)
			.ToArrayAsync(cts.Token);

		Assert.Equal(setupValues, actual);
	}

	[Fact]
	public async Task ReturnValuesEx()
	{
		string[] setupValues = ["Okayama", "Issei"];
		using var cts = new CancellationTokenSource();

		DependencyServiceMock
			.SetupGetEnumerableAsync()
			.Returns([..setupValues]);

		var actual = await CreateFixture()
			.GetEnumerableAsync(cts.Token)
			.ToArrayAsync(cts.Token);

		Assert.Equivalent(setupValues, actual, true);
	}

	[Fact]
	public async Task ReturnMultipleValuesEx()
	{
		string[] setupValues1 = ["Okayama", "Issei"], setupValues2 = ["Issei", "Okayama"];
		using var cts = new CancellationTokenSource();

		DependencyServiceMock
			.SetupGetEnumerableAsync()
			.Returns([..setupValues1])
			.Returns([..setupValues2]);

		var fixture = CreateFixture();
		var actual1 = await fixture.GetEnumerableAsync(cts.Token)
			.ToArrayAsync(cts.Token);

		var actual2 = await fixture.GetEnumerableAsync(cts.Token)
			.ToArrayAsync(cts.Token);

		Assert.Equivalent(setupValues1, actual1, true);
		Assert.Equivalent(setupValues2, actual2, true);
	}
}
