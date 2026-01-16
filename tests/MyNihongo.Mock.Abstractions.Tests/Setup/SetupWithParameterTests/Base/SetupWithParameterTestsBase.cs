namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithParameterTests;

public abstract class SetupWithParameterTestsBase : SetupTestsBase
{
	protected static SetupWithParameter<T> CreateFixture<T>()
	{
		return new SetupWithParameter<T>();
	}

	protected static SetupWithParameter<T> CreateFixture<T>(in It<T> setup)
	{
		var fixture = new SetupWithParameter<T>();
		fixture.SetupParameter(setup.ValueSetup);
		return fixture;
	}
}
