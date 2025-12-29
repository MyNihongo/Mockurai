namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithRefReadOnlyParameterTests;

public abstract class SetupWithRefReadOnlyParameterTestsBase : SetupTestsBase
{
	protected static SetupWithRefReadOnlyParameter<T> CreateFixture<T>()
	{
		return new SetupWithRefReadOnlyParameter<T>();
	}

	protected static SetupWithRefReadOnlyParameter<T> CreateFixture<T>(in It<T> setup)
	{
		var fixture = new SetupWithRefReadOnlyParameter<T>();
		fixture.SetupParameter(setup);
		return fixture;
	}
}
