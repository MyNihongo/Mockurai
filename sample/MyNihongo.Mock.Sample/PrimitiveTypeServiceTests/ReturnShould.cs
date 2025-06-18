namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnShould : PrimitiveTypeServiceTestsBase
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

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturn()
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.Return();

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
