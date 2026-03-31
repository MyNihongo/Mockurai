namespace MyNihongo.Mock.Sample;

public static class SetupRefInt32RefInt32Ex
{
	public static void And(this SetupRefInt32RefInt32 @this)
	{
		((ISetupCallbackJoin<SetupRefInt32RefInt32.CallbackDelegate>)@this).And();
	}

	public static void And<T>(this SetupRefInt32RefInt32<T> @this)
	{
		((ISetupCallbackJoin<SetupRefInt32RefInt32<T>.CallbackDelegate, T, SetupRefInt32RefInt32<T>.ReturnsCallbackDelegate>)@this).And();
	}
}
