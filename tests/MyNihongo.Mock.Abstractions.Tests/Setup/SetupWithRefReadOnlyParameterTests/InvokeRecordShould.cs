namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithRefReadOnlyParameterTests;

public sealed class InvokeRecordShould : SetupWithRefReadOnlyParameterTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setup = ItRefReadOnly<RecordParameter1>.Any();

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var input = new RecordParameter1(
			Text: "any text",
			Number: 12345678
		);

		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}
}
