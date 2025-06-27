namespace MyNihongo.Mock.Sample._Generated.SetupTests;

public sealed class InvokePrimitiveShould : SetupTestsBase
{
	[Fact]
	public void ThrowException()
	{
		const string errorMessage = nameof(errorMessage);

		var fixture = CreateFixture();
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

		var actual = () => fixture.Invoke();

		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowException()
	{
		CreateFixture()
			.Invoke();
	}
}
