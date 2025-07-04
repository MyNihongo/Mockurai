namespace MyNihongo.Mock.Sample._Generated.InvocationTests;

public sealed class VerifyShould : InvocationTestsBase
{
	[Fact]
	public void Test()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		fixture.Verify(123);
	}
}
