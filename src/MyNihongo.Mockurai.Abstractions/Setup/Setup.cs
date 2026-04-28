namespace MyNihongo.Mockurai;

/// <summary>
/// A void mock setup whose configured callback is an <see cref="Action"/>, executed via <see cref="Invoke"/>.
/// </summary>
public sealed class Setup : SetupBase<Action>
{
	/// <summary>
	/// Executes the next queued action: invokes the configured callback (if any) and then throws the configured exception (if any).
	/// </summary>
	public void Invoke()
	{
		var x = GetSetup();
		x.Callback?.Invoke();

		if (x.Exception is not null)
			throw x.Exception;
	}
}
