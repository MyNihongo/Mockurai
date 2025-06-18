namespace MyNihongo.Mock.Sample.StructTypeServiceTests;

public sealed class ReturnNullableShould : StructTypeServiceTestsBase
{
	[Fact]
	public void ReturnNullWithoutSetup()
	{
		var actual = CreateFixture()
			.ReturnNullable();

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const decimal expected = 2181m;

		StructDependencyServiceMock
			.SetupReturnNullable()
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.ReturnNullable();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		StructDependencyServiceMock
			.SetupReturnNullable()
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnNullable();

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
