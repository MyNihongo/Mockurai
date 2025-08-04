namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsTests;

public abstract class SetupReturnsTestsBase
{
	protected static Setup<T> CreateFixture<T>()
	{
		return new Setup<T>();
	}
}
