namespace MyNihongo.Mock.Sample.RecordTypeServiceTests;

public sealed class ReturnWithOneParameterNullableShould : RecordTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
	{
		var input = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithSetupSameInstance()
	{
		var input = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithOneParameterNullable(input)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		const string expected = "name:Okayama Issei,age:12";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetupAnotherInstance()
	{
		var setupParameter = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		Assert.Null(actual);
	}

	[Fact]
	public void ThrowWithSetupAnotherSetup()
	{
		var setupParameter = new RecordParameter1(
			Number: 1,
			Text: "Some text"
		);

		var input = new RecordParameter1(
			Number: 12345678,
			Text: "Another text"
		);

		RecordDependencyServiceMock
			.SetupReturnWithOneParameterNullable(setupParameter)
			.Returns(new RecordReturn(
				Age: 12,
				Name: "Okayama Issei",
				DateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnWithOneParameterNullable(input);

		Assert.Null(actual);
	}
}
