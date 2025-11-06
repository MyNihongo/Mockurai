namespace MyNihongo.Mock.Sample;

public static class SetupIntRefIntEx
{
	public static void And(this SetupIntRefInt @this)
	{
		((ISetupCallbackJoin<SetupIntRefInt.CallbackDelegate>)@this).And();
	}

	public static void And<T>(this SetupIntRefInt<T> @this)
	{
		((ISetupCallbackJoin<SetupIntRefInt<T>.CallbackDelegate, T, SetupIntRefInt<T>.ReturnsCallbackDelegate>)@this).And();
	}
}
