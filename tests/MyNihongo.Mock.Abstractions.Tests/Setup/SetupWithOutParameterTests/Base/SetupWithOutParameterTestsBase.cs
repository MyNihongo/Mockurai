namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithOutParameterTests;

public abstract class SetupWithOutParameterTestsBase : SetupTestsBase
{
	protected static SetupWithOutParameter<T> CreateFixture<T>()
	{
		return new SetupWithOutParameter<T>();
	}
}
