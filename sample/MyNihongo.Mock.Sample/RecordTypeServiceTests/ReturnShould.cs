namespace MyNihongo.Mock.Sample.RecordTypeServiceTests;

public sealed class ReturnShould : RecordTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
	{
		Action actual = () => CreateFixture()
			.Return();

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IRecordDependencyService#Return() method has not been set up", exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const decimal expected = 2181m;

		RecordDependencyServiceMock
			.SetupReturn()
			.Returns(new RecordReturn
			{
				Age = 12,
				Name = "Okayama Issei",
				DateOfBirth = new DateOnly(2025, 6, 16),
			});

		var actual = CreateFixture()
			.Return();

		Assert.Equal(expected, actual);
	}
}
