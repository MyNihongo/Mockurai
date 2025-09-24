namespace MyNihongo.Mock;

public sealed class SetupWithOutParameterReturns<TParameter, TReturns> : SetupBaseReturns<TReturns, ActionOut<TParameter>, FuncOut<TParameter, TReturns?>>
{
	public bool Execute(out TParameter? parameter, out TReturns? returnValue)
	{
		if (CallbackDelegate is not null)
			CallbackDelegate.Invoke(out parameter);
		else
			parameter = default!;

		if (Exception is not null)
			throw Exception;

		if (ReturnsDelegate is not null)
		{
			returnValue = ReturnsDelegate(out parameter);
			return true;
		}

		returnValue = default;
		return false;
	}

	public override void Returns(TReturns? value)
	{
		Returns((out TParameter parameter) =>
		{
			parameter = default!;
			return value;
		});
	}
}
