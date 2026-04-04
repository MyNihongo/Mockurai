namespace MyNihongo.Mockurai.Abstractions.Tests.Setup.SetupReturnsTests;

public abstract class SetupReturnsTestsBase
{
	protected static Setup<T> CreateFixture<T>()
	{
		return new Setup<T>();
	}
}
