namespace MyNihongo.Mock;

public sealed class SetupWithParameter<TParameter, TReturns> : SetupWithParameterBase<TParameter, Action<TParameter>, TReturns, Func<TParameter, TReturns?>>
{
	public bool Execute(in TParameter parameter, out TReturns? returnValue)
	{
		if (Setups is null)
			goto Default;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(parameter);

			if (x.Exception is not null)
				throw x.Exception;

			if (x.Returns is not null)
			{
				returnValue = x.Returns(parameter);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public override ISetup<Action<TParameter>, TReturns, Func<TParameter, TReturns?>> Returns(TReturns? returns)
	{
		return Returns(_ => returns);
	}
}
