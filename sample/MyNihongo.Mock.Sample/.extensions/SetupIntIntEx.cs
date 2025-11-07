namespace MyNihongo.Mock.Sample;

public static class SetupIntIntEx
{
	public static void And(this SetupIntInt @this)
	{
		((ISetupCallbackJoin<Action<int, int>>)@this).And();
	}

	public static void And<T>(this SetupIntInt<T> @this)
	{
		((ISetupCallbackJoin<Action<int, int>, T, Func<int, int, T?>>)@this).And();
	}
}
