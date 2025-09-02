namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithParameterTests;

public sealed class InvokeStructShould : SetupWithParameterTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<StructParameter1>.Any();

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var input = new StructParameter1
		{
			Text = "any text",
			Number = 12345678,
		};

		var actual = () => fixture.Invoke(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
