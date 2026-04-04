namespace MyNihongo.Mockurai.Abstractions.Tests.Setup.SetupReturnsWithParameterTests;

public abstract class SetupReturnsWithParameterTestsBase : SetupTestsBase
{
	protected static SetupWithParameter<TParameter, TReturns> CreateFixture<TParameter, TReturns>(It<TParameter> parameter)
	{
		var fixture = CreateFixture<TParameter, TReturns>();
		fixture.SetupParameter(parameter.ValueSetup);
		return fixture;
	}

	protected static SetupWithParameter<TParameter, TReturns> CreateFixture<TParameter, TReturns>()
	{
		return new SetupWithParameter<TParameter, TReturns>();
	}
}
