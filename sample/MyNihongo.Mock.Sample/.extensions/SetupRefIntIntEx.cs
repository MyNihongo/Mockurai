namespace MyNihongo.Mock.Sample;

public static class SetupRefIntIntEx
{
	public static void And(this SetupRefIntInt @this)
	{
		((ISetupCallbackJoin<SetupRefIntInt.CallbackDelegate>)@this).And();
	}

	public static void And<T>(this SetupRefIntInt<T> @this)
	{
		((ISetupCallbackJoin<SetupRefIntInt<T>.CallbackDelegate, T, SetupRefIntInt<T>.ReturnsCallbackDelegate>)@this).And();
	}
}
