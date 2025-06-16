namespace MyNihongo.Mock.Sample.RecordTypeServiceTests;

public sealed class ReturnNullableShould : RecordTypeServiceTestsBase
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

		RecordDependencyServiceMock
			.SetupReturnNullable()
			.Returns(new RecordReturn
			{
				Age = 12,
				Name = "Okayama Issei",
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.ReturnNullable();

		Assert.Equal(expected, actual);
	}
}
