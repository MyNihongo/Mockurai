namespace MyNihongo.Mock;

public sealed class SetupWithOutParameter<TParameter> : SetupBase<ActionOut<TParameter>>
{
	public void Invoke(out TParameter parameter)
	{
		var x = GetSetup();
		if (x.Callback is not null)
			x.Callback.Invoke(out parameter);
		else
			parameter = default!;

		if (x.Exception is not null)
			throw x.Exception;
	}
}
