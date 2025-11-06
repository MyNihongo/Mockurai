namespace MyNihongo.Mock.Abstractions.Tests;

public static class SetupEx
{
	public static void And<T>(this Setup<T> @this)
	{
		((ISetupCallbackJoin<Action, T, Func<T?>>)@this).And();
	}
}
