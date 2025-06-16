namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class ReturnShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
	{
		Action actual = () => CreateFixture()
			.Return();

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IClassDependencyService#Return() method has not been set up", exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const decimal expected = 2181m;

		ClassDependencyServiceMock
			.SetupReturn()
			.Returns(new ClassReturn
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
