namespace MyNihongo.Mockurai.Sample;

public static class SetupInt32RefInt32Ex
{
	public static void And(this SetupInt32RefInt32 @this)
	{
		((ISetupCallbackJoin<SetupInt32RefInt32.CallbackDelegate>)@this).And();
	}

	public static void And<T>(this SetupInt32RefInt32<T> @this)
	{
		((ISetupCallbackJoin<SetupInt32RefInt32<T>.CallbackDelegate, T, SetupInt32RefInt32<T>.ReturnsCallbackDelegate>)@this).And();
	}
}
