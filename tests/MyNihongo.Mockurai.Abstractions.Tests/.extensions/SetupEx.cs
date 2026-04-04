namespace MyNihongo.Mockurai.Abstractions.Tests;

public static class SetupEx
{
	public static void And(this Mockurai.Setup @this)
	{
		((ISetupCallbackJoin<Action>)@this).And();
	}

	public static void And<T>(this Setup<T> @this)
	{
		((ISetupCallbackJoin<Action, T, Func<T>>)@this).And();
	}
}
