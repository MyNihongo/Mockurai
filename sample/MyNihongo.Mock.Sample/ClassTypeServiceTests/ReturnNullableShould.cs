namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class ReturnNullableShould : ClassTypeServiceTestsBase
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

		ClassDependencyServiceMock
			.SetupReturnNullable()
			.Returns(new ClassReturn
			{
				Age = 12,
				Name = "Okayama Issei",
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnNullable();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowCustomException()
	{
		const string errorMessage = nameof(errorMessage);

		ClassDependencyServiceMock
			.SetupReturnNullable()
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnNullable();

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
