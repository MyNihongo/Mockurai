namespace MyNihongo.Mock.Sample.SampleServiceTests;

public sealed class ComputeDeliveryExpensesShould : SampleServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const decimal expected = 0m;

		var actual = CreateFixture()
			.ComputeDeliveryExpenses();

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
			.ComputeDeliveryExpenses();

		Assert.Equal(expected, actual);
	}
}
