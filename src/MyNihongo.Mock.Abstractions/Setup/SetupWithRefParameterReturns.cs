namespace MyNihongo.Mock;

public sealed class SetupWithRefParameter<TParameter, TReturns> : SetupWithParameterBase<TParameter, ActionRef<TParameter>, TReturns, FuncRef<TParameter, TReturns?>>
{
	public bool Execute(ref TParameter parameter, out TReturns? returnValue)
	{
		if (Setups is null)
			goto Default;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(ref parameter);

			if (x.Exception is not null)
				throw x.Exception;

			if (x.Returns is not null)
			{
				returnValue = x.Returns(ref parameter);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public override ISetup<ActionRef<TParameter>, TReturns, FuncRef<TParameter, TReturns?>> Returns(TReturns? returns)
	{
		return Returns((ref TParameter _) => returns);
	}
}
