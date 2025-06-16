namespace MyNihongo.Mock.Sample.PrimitireTypeServiceTests;

public sealed class ReturnShould : PrimitireTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const decimal expected = 0m;

		var actual = CreateFixture()
			.Return();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const int setupCount = 5;
		const decimal expected = 5_000m;

		DependencyServiceMock
			.SetupReturn()
			.Returns(setupCount);

		var actual = CreateFixture()
			.Return();

		Assert.Equal(expected, actual);
	}
}
