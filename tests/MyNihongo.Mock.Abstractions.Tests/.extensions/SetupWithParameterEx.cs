namespace MyNihongo.Mock.Abstractions.Tests;

public static class SetupWithParameterEx
{
	public static void And<TParameter>(this SetupWithParameter<TParameter> @this)
	{
		((ISetupCallbackJoin<Action<TParameter>>)@this).And();
	}

	public static void And<TParameter, TReturns>(this SetupWithParameter<TParameter, TReturns> @this)
	{
		((ISetupCallbackJoin<Action<TParameter>, TReturns, Func<TParameter, TReturns?>>)@this).And();
	}
}
