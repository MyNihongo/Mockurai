namespace MyNihongo.Mock.Sample._Generated.SetupReturnsWithSeveralParametersTests;

public abstract class SetupReturnsWithSeveralParametersTestsBase
{
	protected static T CreateFixture<T>()
		where T : ISetup<string>, new()
	{
		return new T();
	}

	protected static T CreateFixtureInt<T>()
		where T : ISetup<int>, new()
	{
		return new T();
	}
}
