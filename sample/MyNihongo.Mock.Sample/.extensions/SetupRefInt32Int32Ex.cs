namespace MyNihongo.Mock.Sample;

public static class SetupRefInt32Int32Ex
{
	public static void And(this SetupRefInt32Int32 @this)
	{
		((ISetupCallbackJoin<SetupRefInt32Int32.CallbackDelegate>)@this).And();
	}

	public static void And<T>(this SetupRefInt32Int32<T> @this)
	{
		((ISetupCallbackJoin<SetupRefInt32Int32<T>.CallbackDelegate, T, SetupRefInt32Int32<T>.ReturnsCallbackDelegate>)@this).And();
	}
}
