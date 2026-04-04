namespace MyNihongo.Mockurai.Abstractions.Tests.Setup.SetupReturnsWithOutParameterTests;

public abstract class SetupReturnsWithOutParameterTestsBase
{
	protected static SetupWithOutParameter<TParameter, TReturns> CreateFixture<TParameter, TReturns>()
	{
		return new SetupWithOutParameter<TParameter, TReturns>();
	}
}
