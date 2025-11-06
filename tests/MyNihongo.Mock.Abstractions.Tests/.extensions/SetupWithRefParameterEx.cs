namespace MyNihongo.Mock.Abstractions.Tests;

public static class SetupWithRefParameterEx
{
	public static void And<TParameter, TReturns>(this SetupWithRefParameter<TParameter, TReturns> @this)
	{
		((ISetupCallbackJoin<ActionRef<TParameter>, TReturns, FuncRef<TParameter, TReturns?>>)@this).And();
	}
}
