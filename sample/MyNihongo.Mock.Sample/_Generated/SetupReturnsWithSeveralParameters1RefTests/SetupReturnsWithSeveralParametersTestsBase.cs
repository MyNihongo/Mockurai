namespace MyNihongo.Mock.Sample._Generated.SetupReturnsWithSeveralParameters1RefTests;

public abstract class SetupReturnsWithSeveralParametersTestsBase : SetupTestsBase
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
