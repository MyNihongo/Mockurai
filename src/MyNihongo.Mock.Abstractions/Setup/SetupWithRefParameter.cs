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

			var (callback, exception) = setup.GetSetup();
			callback?.Invoke(ref parameter);

			if (exception is not null)
				throw exception;
		}
	}
}
