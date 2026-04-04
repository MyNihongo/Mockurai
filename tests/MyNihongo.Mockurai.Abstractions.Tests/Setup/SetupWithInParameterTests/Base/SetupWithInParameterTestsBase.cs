namespace MyNihongo.Mockurai.Abstractions.Tests.Setup.SetupWithInParameterTests;

public abstract class SetupWithInParameterTestsBase : SetupTestsBase
{
	protected static SetupWithParameter<T> CreateFixture<T>()
	{
		return new SetupWithParameter<T>();
	}

	protected static SetupWithInParameter<T> CreateFixture<T>(in ItIn<T> setup)
	{
		var fixture = new SetupWithInParameter<T>();
		fixture.SetupParameter(setup.ValueSetup);
		return fixture;
	}
}
