namespace MyNihongo.Mock;

public sealed class SetupWithOutParameter<TParameter, TReturns> : SetupBaseReturns<TReturns, ActionOut<TParameter>, FuncOut<TParameter, TReturns?>>
{
	public bool Execute(out TParameter? parameter, out TReturns? returnValue)
	{
		var x = GetSetup();
		if (x.Callback is not null)
			x.Callback.Invoke(out parameter);
		else
			parameter = default!;

		if (x.Exception is not null)
			throw x.Exception;

		if (x.Returns is not null)
		{
			returnValue = x.Returns(out parameter);
			return true;
		}

		returnValue = default;
		return false;
	}

	public override void Returns(TReturns? returns)
	{
		Returns((out TParameter parameter) =>
		{
			parameter = default!;
			return returns;
		});
	}
}
