namespace MyNihongo.Mock.Sample.SampleServiceTests;

public sealed class ReturnWithOneParameterShould : SampleServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const string paramCustomerId = "qOp5SNt2LVqzQ9qS";

		var actual = CreateFixture()
			.ReturnWithOneParameter(paramCustomerId);

		Assert.Empty(actual.Name);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupName = "Okayama Issei";

		DependencyServiceMock
			.SetupGetCustomerName(paramCustomerId)
			.Returns(setupName);

		var actual = CreateFixture()
			.ReturnWithOneParameter(paramCustomerId);

		Assert.Equal(setupName, actual.Name);
	}

	[Fact]
	public void ReturnValueWithAnotherSetup()
	{
		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID",
			setupName = "Okayama Issei";

		DependencyServiceMock
			.SetupGetCustomerName(setupCustomerId)
			.Returns(setupName);

		var actual = CreateFixture()
			.ReturnWithOneParameter(paramCustomerId);

		Assert.Empty(actual.Name);
	}
}
