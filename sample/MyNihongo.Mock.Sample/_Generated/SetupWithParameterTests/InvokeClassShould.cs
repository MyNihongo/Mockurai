namespace MyNihongo.Mock.Sample._Generated.SetupWithParameterTests;

public sealed class InvokeClassShould : SetupWithParameterTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<ClassParameter1>.Any();

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var input = new ClassParameter1
		{
			Text = "any text",
			Number = 12345678,
		};

		var actual = () => fixture.Invoke(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void Todo()
	{
		throw new NotImplementedException();
	}
}
