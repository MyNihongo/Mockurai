namespace MyNihongo.Mock;

public sealed class SetupWithRefParameter<TParameter, TReturns> : SetupWithParameterBase<TParameter, TReturns, ActionRef<TParameter>>
{
	public bool Execute(ref TParameter parameter, out TReturns? returnValue)
	{
		if (Setups is null)
			goto Default;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			setup.Callback?.Invoke(ref parameter);

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
}
