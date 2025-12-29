namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsWithInParameterTests;

public abstract class SetupReturnsWithInParameterTestsBase : SetupTestsBase
{
	protected static SetupWithInParameter<TParameter, TReturns> CreateFixture<TParameter, TReturns>(It<TParameter> parameter)
	{
		var fixture = CreateFixture<TParameter, TReturns>();
		fixture.SetupParameter(parameter);
		return fixture;
	}

	protected static SetupWithInParameter<TParameter, TReturns> CreateFixture<TParameter, TReturns>()
	{
		return new SetupWithInParameter<TParameter, TReturns>();
	}
}
