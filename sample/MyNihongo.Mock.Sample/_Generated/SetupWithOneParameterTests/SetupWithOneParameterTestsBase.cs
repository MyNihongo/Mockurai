namespace MyNihongo.Mock.Sample._Generated.SetupWithOneParameterTests;

public abstract class SetupWithOneParameterTestsBase
{
	protected static SetupWithParameter<T> CreateFixture<T>()
	{
		return new SetupWithParameter<T>();
	}

	protected static SetupWithParameter<T> CreateFixture<T>(in It<T> setup)
	{
		var fixture = new SetupWithParameter<T>();
		fixture.SetupParameter(setup);
		return fixture;
	}
}
