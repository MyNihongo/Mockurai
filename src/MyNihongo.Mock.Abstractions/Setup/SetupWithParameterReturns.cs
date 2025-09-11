namespace MyNihongo.Mock;

public sealed class SetupWithParameter<TParameter, TReturns> : SetupWithParameterBase<TParameter, TReturns, Action<TParameter>, Func<TParameter, TReturns?>>
{
	public bool Execute(in TParameter parameter, out TReturns? returnValue)
	{
		if (Setups is null)
			goto Default;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			setup.Callback?.Invoke(parameter);

			if (setup.Exception is not null)
				throw setup.Exception;

			if (setup.Returns is not null)
			{
				returnValue = setup.Returns(parameter);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public override void Returns(TReturns? value)
	{
		Returns(_ => value);
	}
}
