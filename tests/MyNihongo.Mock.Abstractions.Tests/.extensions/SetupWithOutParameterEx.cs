namespace MyNihongo.Mock.Abstractions.Tests;

public static class SetupWithOutParameterEx
{
	public static void And<TParameter, TReturns>(this SetupWithOutParameter<TParameter, TReturns> @this)
	{
		((ISetupCallbackJoin<ActionOut<TParameter>, TReturns, FuncOut<TParameter, TReturns?>>)@this).And();
	}
}
