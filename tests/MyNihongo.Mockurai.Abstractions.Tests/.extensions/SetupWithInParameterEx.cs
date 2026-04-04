namespace MyNihongo.Mockurai.Abstractions.Tests;

public static class SetupWithInParameterEx
{
	public static void And<TParameter>(this SetupWithInParameter<TParameter> @this)
	{
		((ISetupCallbackJoin<ActionIn<TParameter>>)@this).And();
	}

	public static void And<TParameter, TReturns>(this SetupWithInParameter<TParameter, TReturns> @this)
	{
		((ISetupCallbackJoin<ActionIn<TParameter>, TReturns, FuncIn<TParameter, TReturns>>)@this).And();
	}
}
