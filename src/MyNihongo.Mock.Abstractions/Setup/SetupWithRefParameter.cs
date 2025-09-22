namespace MyNihongo.Mock;

public class SetupWithRefParameter<TParameter> : SetupWithParameterBase<TParameter, ActionRef<TParameter>>
{
	public void Invoke(ref TParameter parameter)
	{
		if (Setups is null)
			return;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			setup.Callback?.Invoke(ref parameter);

			if (setup.Exception is not null)
				throw setup.Exception;
		}
	}
}
