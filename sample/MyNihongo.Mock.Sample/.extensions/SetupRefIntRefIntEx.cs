namespace MyNihongo.Mock.Sample;

public static class SetupRefIntRefIntEx
{
	public static void And(this SetupRefIntRefInt @this)
	{
		((ISetupCallbackJoin<SetupRefIntRefInt.CallbackDelegate>)@this).And();
	}

	public static void And<T>(this SetupRefIntRefInt<T> @this)
	{
		((ISetupCallbackJoin<SetupRefIntRefInt<T>.CallbackDelegate, T, SetupRefIntRefInt<T>.ReturnsCallbackDelegate>)@this).And();
	}
}
