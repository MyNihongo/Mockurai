namespace MyNihongo.Mock.Abstractions.Tests;

public static class SetupEx
{
	public static void And(this Mock.Setup @this)
	{
		((ISetupCallbackJoin<Action>)@this).And();
	}

	public static void And<T>(this Setup<T> @this)
	{
		((ISetupCallbackJoin<Action, T, Func<T?>>)@this).And();
	}
}
