namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsWithRefParameterTests;

public abstract class SetupReturnsWithRefParameterTestsBase : SetupTestsBase
{
	protected static SetupWithRefParameter<T, TReturns> CreateFixture<T, TReturns>()
	{
		return new SetupWithRefParameter<T, TReturns>();
	}

	protected static SetupWithRefParameter<T, TReturns> CreateFixture<T, TReturns>(in It<T> setup)
	{
		var fixture = new SetupWithRefParameter<T, TReturns>();
		fixture.SetupParameter(setup);
		return fixture;
	}
}
