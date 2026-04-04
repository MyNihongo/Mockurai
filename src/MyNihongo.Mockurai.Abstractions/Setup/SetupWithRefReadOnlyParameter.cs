namespace MyNihongo.Mockurai;

public class SetupWithRefReadOnlyParameter<TParameter> : SetupWithParameterBase<TParameter, ActionRefReadOnly<TParameter>>
{
	public void Invoke(ref readonly TParameter parameter)
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
