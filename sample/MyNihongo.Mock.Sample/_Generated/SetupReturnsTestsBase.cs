namespace MyNihongo.Mock.Sample._Generated;

public abstract class SetupReturnsTestsBase : SetupTestsBase
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
