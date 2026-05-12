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
}
