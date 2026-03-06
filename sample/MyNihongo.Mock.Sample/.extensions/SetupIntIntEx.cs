namespace MyNihongo.Mock.Sample;

public static class SetupIntIntEx
{
	public static void And(this SetupIntInt @this)
	{
		((ISetupCallbackJoin<SetupIntInt.CallbackDelegate>)@this).And();
	}

	public static void And<T>(this SetupIntInt<T> @this)
	{
		((ISetupCallbackJoin<SetupIntInt<T>.CallbackDelegate, T, SetupIntInt<T>.ReturnsCallbackDelegate>)@this).And();
	}
}
