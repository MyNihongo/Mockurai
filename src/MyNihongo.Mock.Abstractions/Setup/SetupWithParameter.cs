namespace MyNihongo.Mock;

public sealed class SetupWithParameter<TParameter> : SetupWithParameterBase<TParameter, Action<TParameter>>
{
	public void Invoke(in TParameter parameter)
	{
		if (Setups is null)
			return;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			var (callback, exception) = setup.GetSetup();
			callback?.Invoke(parameter);

			if (exception is not null)
				throw exception;
		}
	}
}
