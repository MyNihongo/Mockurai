namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithRefParameterTests;

public abstract class SetupWithRefParameterTestsBase : SetupTestsBase
{
	protected static SetupWithRefParameter<T> CreateFixture<T>()
	{
		return new SetupWithRefParameter<T>();
	}

	protected static SetupWithRefParameter<T> CreateFixture<T>(in It<T> setup)
	{
		var fixture = new SetupWithRefParameter<T>();
		fixture.SetupParameter(setup.ValueSetup);
		return fixture;
	}
}
