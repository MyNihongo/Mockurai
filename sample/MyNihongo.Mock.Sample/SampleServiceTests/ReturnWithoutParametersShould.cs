namespace MyNihongo.Mock.Sample.SampleServiceTests;

public sealed class ReturnWithoutParametersShould : SampleServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const decimal expected = 0m;

		var actual = CreateFixture()
			.ReturnWithoutParameters();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const int setupCount = 5;
		const decimal expected = 5_000m;

		DependencyServiceMock
			.SetupGetShopCount()
			.Returns(setupCount);

		var actual = CreateFixture()
			.ReturnWithoutParameters();

		Assert.Equal(expected, actual);
	}
}
