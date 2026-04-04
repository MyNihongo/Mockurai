namespace MyNihongo.Mockurai.Abstractions.Tests;

public static class SetupWithRefReadOnlyParameterEx
{
	public static void And<TParameter>(this SetupWithRefReadOnlyParameter<TParameter> @this)
	{
		((ISetupCallbackJoin<ActionRefReadOnly<TParameter>>)@this).And();
	}

	public static void And<TParameter, TReturns>(this SetupWithRefReadOnlyParameter<TParameter, TReturns> @this)
	{
		((ISetupCallbackJoin<ActionRefReadOnly<TParameter>, TReturns, FuncRefReadOnly<TParameter, TReturns>>)@this).And();
	}
}
