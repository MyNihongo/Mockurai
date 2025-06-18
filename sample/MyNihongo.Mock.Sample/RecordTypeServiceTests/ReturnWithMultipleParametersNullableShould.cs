namespace MyNihongo.Mock.Sample.RecordTypeServiceTests;

public sealed class ReturnWithMultipleParametersNullableShould : RecordTypeServiceTestsBase
{
	[Fact]
	public void ReturnNullWithoutSetup()
	{
		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input2);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input2);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence1()
	{
		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence2()
	{
		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullWithInvalidSequence3()
	{
		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input2);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithDifferentInstancesInvalidSequence1()
	{
		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithDifferentInstancesInvalidSequence2()
	{
		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input1);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithDifferentInstancesInvalidSequence3()
	{
		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		const double expected = 15d;
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnNullForThrowsWithInvalidSequence1()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullForThrowsWithInvalidSequence2()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnNullForThrowsWithInvalidSequence3()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 2,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input2);

		Assert.Null(actual);
	}

	[Fact]
	public void ThrowWithDifferentInstancesInvalidSequence1()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithDifferentInstancesInvalidSequence2()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithMultipleParametersNullable(input1, input1);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithDifferentInstancesInvalidSequence3()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input2 = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithMultipleParametersNullable(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithMultipleParametersNullable(input2, input1);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
