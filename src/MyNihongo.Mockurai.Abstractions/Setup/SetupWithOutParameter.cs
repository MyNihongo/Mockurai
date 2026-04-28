namespace MyNihongo.Mockurai;

/// <summary>
/// A void mock setup for a member with a single <see langword="out"/> parameter, whose callback assigns the produced value.
/// </summary>
/// <typeparam name="TParameter">The argument type produced by the setup.</typeparam>
public sealed class SetupWithOutParameter<TParameter> : SetupBase<ActionOut<TParameter>>
{
	/// <summary>
	/// Executes the next queued action and assigns the <see langword="out"/> value via the configured callback,
	/// or assigns <see langword="default"/> when no callback was configured.
	/// </summary>
	/// <param name="parameter">The output argument assigned by the configured callback.</param>
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
