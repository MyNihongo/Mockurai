namespace MyNihongo.Mock.Sample._Generated;

public abstract class SetupWithSeveralParametersTestsBase : SetupTestsBase
{
	protected static T CreateFixture<T>()
		where T : ISetup, new()
	{
		return new T();
	}
}
