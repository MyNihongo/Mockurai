namespace MyNihongo.Mockurai;

public sealed class SetupWithInParameter<TParameter> : SetupWithParameterBase<TParameter, ActionIn<TParameter>>
{
	public void Invoke(in TParameter parameter)
	{
		if (Setups is null)
			return;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(in parameter);

			if (x.Exception is not null)
				throw x.Exception;
		}
	}
}
