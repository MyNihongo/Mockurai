namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsWithRefReadOnlyParameterTests;

public abstract class SetupReturnsWithRefParameterTestsBase : SetupTestsBase
{
	protected static SetupWithRefReadOnlyParameter<T, TReturns> CreateFixture<T, TReturns>()
	{
		return new SetupWithRefReadOnlyParameter<T, TReturns>();
	}

	protected static SetupWithRefReadOnlyParameter<T, TReturns> CreateFixture<T, TReturns>(in ItRefReadOnly<T> setup)
	{
		var fixture = new SetupWithRefReadOnlyParameter<T, TReturns>();
		fixture.SetupParameter(setup.ValueSetup);
		return fixture;
	}
}
