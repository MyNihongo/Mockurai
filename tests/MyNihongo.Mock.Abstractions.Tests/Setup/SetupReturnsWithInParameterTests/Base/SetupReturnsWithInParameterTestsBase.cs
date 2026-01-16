namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsWithInParameterTests;

public abstract class SetupReturnsWithInParameterTestsBase : SetupTestsBase
{
	protected static SetupWithInParameter<TParameter, TReturns> CreateFixture<TParameter, TReturns>(ItIn<TParameter> parameter)
	{
		var fixture = CreateFixture<TParameter, TReturns>();
		fixture.SetupParameter(parameter.ValueSetup);
		return fixture;
	}

	protected static SetupWithInParameter<TParameter, TReturns> CreateFixture<TParameter, TReturns>()
	{
		return new SetupWithInParameter<TParameter, TReturns>();
	}
}
