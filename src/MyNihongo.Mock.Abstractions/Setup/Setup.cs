namespace MyNihongo.Mock;

public sealed class Setup : SetupBase<Action>
{
	public void Invoke()
	{
		CallbackDelegate?.Invoke();

		if (Exception is not null)
			throw Exception;
	}
}
