namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsWithRefReadOnlyParameterTests;

public abstract class SetupReturnsWithRefParameterTestsBase : SetupTestsBase
{
	protected static SetupWithRefReadOnlyParameter<T, TReturns> CreateFixture<T, TReturns>()
	{
		return new SetupWithRefReadOnlyParameter<T, TReturns>();
	}

	protected static SetupWithRefReadOnlyParameter<T, TReturns> CreateFixture<T, TReturns>(in It<T> setup)
	{
		var fixture = new SetupWithRefReadOnlyParameter<T, TReturns>();
		fixture.SetupParameter(setup);
		return fixture;
	}
}
