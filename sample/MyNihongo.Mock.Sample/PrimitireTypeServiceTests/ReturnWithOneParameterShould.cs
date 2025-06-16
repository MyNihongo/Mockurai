namespace MyNihongo.Mock.Sample.PrimitireTypeServiceTests;

public sealed class ReturnWithOneParameterShould : PrimitireTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const string paramCustomerId = "qOp5SNt2LVqzQ9qS",
			expected = "name:,age:32";

		var actual = CreateFixture()
			.ReturnWithOneParameter(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			nameSetup = "Okayama Issei",
			expected = "name:Okayama Issei,age:32";

		DependencyServiceMock
			.SetupReturnWithOneParameter(parameter)
			.Returns(nameSetup);

		var actual = CreateFixture()
			.ReturnWithOneParameter(parameter);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithAnotherSetup()
	{
		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID",
			setupName = "Okayama Issei",
			expected = "name:,age:32";

		DependencyServiceMock
			.SetupReturnWithOneParameter(setupCustomerId)
			.Returns(setupName);

		var actual = CreateFixture()
			.ReturnWithOneParameter(paramCustomerId);

		Assert.Equal(expected, actual);
	}
}
