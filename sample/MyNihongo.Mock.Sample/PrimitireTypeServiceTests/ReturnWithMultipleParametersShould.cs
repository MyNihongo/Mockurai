namespace MyNihongo.Mock.Sample.PrimitireTypeServiceTests;

public sealed class ReturnWithMultipleParametersShould : PrimitireTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const double expected = 0d;
		const int parameter1 = 2025, parameter2 = 6;

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(parameter1, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const double expected = 225d;
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithMultipleParameters(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(parameter1, parameter2);

		Assert.Equal(expected, actual);
	}
}
