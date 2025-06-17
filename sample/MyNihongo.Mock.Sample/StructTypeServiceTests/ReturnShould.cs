namespace MyNihongo.Mock.Sample.StructTypeServiceTests;

public sealed class ReturnShould : StructTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithoutSetup()
	{
		Action actual = () => CreateFixture()
			.Return();

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal("IStructDependencyService#Return() method has not been set up", exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const decimal expected = 2181m;

		StructDependencyServiceMock
			.SetupReturn()
			.Returns(new StructReturn(
				age: 12,
				name: "Okayama Issei",
				dateOfBirth: new DateOnly(2025, 6, 16)
			));

		var actual = CreateFixture()
			.Return();

		Assert.Equal(expected, actual);
	}
}
