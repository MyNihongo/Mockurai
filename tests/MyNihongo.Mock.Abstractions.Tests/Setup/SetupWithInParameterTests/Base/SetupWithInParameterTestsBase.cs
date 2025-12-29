namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithInParameterTests;

public abstract class SetupWithInParameterTestsBase : SetupTestsBase
{
	protected static SetupWithParameter<T> CreateFixture<T>()
	{
		return new SetupWithParameter<T>();
	}

	protected static SetupWithInParameter<T> CreateFixture<T>(in It<T> setup)
	{
		var fixture = new SetupWithInParameter<T>();
		fixture.SetupParameter(setup);
		return fixture;
	}
}
