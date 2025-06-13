namespace MyNihongo.Mock.Sample.PrimitireTypeServiceTests;

public sealed class ReturnWithOneParameterShould : PrimitireTypeServiceTestsBase
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
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			nameSetup = "Okayama Issei";

		DependencyServiceMock
			.SetupReturnWithOneParameter(parameter)
			.Returns(nameSetup);

		var actual = CreateFixture()
			.ReturnWithOneParameter(parameter);

		Assert.Equal(nameSetup, actual.Name);
	}

	[Fact]
	public void ReturnValueWithAnotherSetup()
	{
		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID",
			setupName = "Okayama Issei";

		DependencyServiceMock
			.SetupReturnWithOneParameter(setupCustomerId)
			.Returns(setupName);

		var actual = CreateFixture()
			.ReturnWithOneParameter(paramCustomerId);

		Assert.Empty(actual.Name);
	}
}
