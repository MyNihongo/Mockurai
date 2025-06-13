namespace MyNihongo.Mock.Sample.SampleServiceTests;

public sealed class ReturnWithMultipleParametersShould : SampleServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const double expected = 0d;
		var input = new DateOnly(2025, 6, 13);

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(input);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const double expected = 0d;
		const decimal resultSetup = 123_456m;
		const int yearSetup = 2025, monthSetup = 6;
		var input = new DateOnly(yearSetup, monthSetup, 13);

		DependencyServiceMock
			.SetupGetCustomerSpending(yearSetup, monthSetup)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(input);

		Assert.Equal(expected, actual);
	}
}
