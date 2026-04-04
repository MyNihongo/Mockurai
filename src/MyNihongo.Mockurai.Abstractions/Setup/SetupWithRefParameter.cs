namespace MyNihongo.Mockurai;

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

			var x = setup.GetSetup();
			x.Callback?.Invoke(ref parameter);

			if (x.Exception is not null)
				throw x.Exception;
		}
	}
}
