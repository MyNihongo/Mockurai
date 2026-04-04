namespace MyNihongo.Mockurai;

public sealed class Setup : SetupBase<Action>
{
	public void Invoke()
	{
		var x = GetSetup();
		x.Callback?.Invoke();

		if (x.Exception is not null)
			throw x.Exception;
	}
}
