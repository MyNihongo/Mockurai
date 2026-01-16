namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithRefReadOnlyParameterTests;

public sealed class InvokeStructShould : SetupWithRefReadOnlyParameterTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setup = ItRefReadOnly<StructParameter1>.Any();

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var input = new StructParameter1
		{
			Text = "any text",
			Number = 12345678,
		};

		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
