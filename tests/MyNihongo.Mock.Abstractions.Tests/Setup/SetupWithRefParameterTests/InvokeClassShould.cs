namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithRefParameterTests;

public sealed class InvokeClassShould : SetupWithRefParameterTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setup = ItRef<ClassParameter1>.Any();

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var input = new ClassParameter1
		{
			Text = "any text",
			Number = 12345678,
		};

		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
