namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithMultipleParametersShould : PrimitiveTypeServiceTestsBase
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

	[Fact]
	public void ReturnValueWithInvalidSequence1()
	{
		const double expected = 0d;
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithMultipleParameters(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(parameter1, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence2()
	{
		const double expected = 0d;
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithMultipleParameters(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(parameter2, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence3()
	{
		const double expected = 0d;
		const decimal resultSetup = 15m;
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithMultipleParameters(parameter1, parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(parameter2, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter1 = 2025, parameter2 = 6;

		DependencyServiceMock
			.SetupReturnWithMultipleParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithMultipleParameters(parameter1, parameter2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence1()
	{
		const double expected = 0d;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithMultipleParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(parameter1, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence2()
	{
		const double expected = 0d;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithMultipleParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(parameter2, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence3()
	{
		const double expected = 0d;
		const int parameter1 = 2025, parameter2 = 6;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithMultipleParameters(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithMultipleParameters(parameter2, parameter2);

		Assert.Equal(expected, actual);
	}
}
