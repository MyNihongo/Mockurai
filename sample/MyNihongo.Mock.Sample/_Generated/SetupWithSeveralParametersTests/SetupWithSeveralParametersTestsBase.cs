namespace MyNihongo.Mock.Sample._Generated.SetupWithSeveralParametersTests;

public class SetupWithSeveralParametersTestsBase
{
	protected static SetupIntInt CreateFixture(in It<int> setup1, in It<int> setup2)
	{
		var fixture = new SetupIntInt();
		//fixture.SetupParameters(setup1.Predicate, setup2.Predicate);
		return fixture;
	}
}
