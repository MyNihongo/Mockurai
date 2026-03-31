namespace MyNihongo.Mock.Sample;

public static class SetupInt32Int32Ex
{
	public static void And(this SetupInt32Int32 @this)
	{
		((ISetupCallbackJoin<SetupInt32Int32.CallbackDelegate>)@this).And();
	}

	public static void And<T>(this SetupInt32Int32<T> @this)
	{
		((ISetupCallbackJoin<SetupInt32Int32<T>.CallbackDelegate, T, SetupInt32Int32<T>.ReturnsCallbackDelegate>)@this).And();
	}
}
