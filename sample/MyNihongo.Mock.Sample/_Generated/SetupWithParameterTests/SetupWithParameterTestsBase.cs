namespace MyNihongo.Mock.Sample._Generated.SetupWithParameterTests;

public abstract class SetupWithParameterTestsBase
{
	protected static SetupWithParameter<T> CreateFixture<T>(in It<T> setup)
	{
		var fixture = new SetupWithParameter<T>();
		fixture.SetupParameter(setup.Predicate);
		return fixture;
	}
}
