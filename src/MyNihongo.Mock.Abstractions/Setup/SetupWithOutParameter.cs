namespace MyNihongo.Mock;

public sealed class SetupWithOutParameter<TParameter> : SetupBase<ActionOut<TParameter>>
{
	public void Invoke(out TParameter? parameter)
	{
		if (CallbackDelegate is not null)
			CallbackDelegate.Invoke(out parameter);
		else
			parameter = default!;

		if (Exception is not null)
			throw Exception;
	}
}
