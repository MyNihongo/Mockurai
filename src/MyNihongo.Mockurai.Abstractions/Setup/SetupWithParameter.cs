namespace MyNihongo.Mockurai;

/// <summary>
/// A void mock setup that matches a single argument and invokes <see cref="Action{TParameter}"/> callbacks.
/// </summary>
/// <typeparam name="TParameter">The argument type captured by the setup.</typeparam>
public sealed class SetupWithParameter<TParameter> : SetupWithParameterBase<TParameter, Action<TParameter>>
{
	/// <summary>
	/// Invokes every configured setup whose argument matcher accepts <paramref name="parameter"/>, in matcher-precedence order.
	/// </summary>
	/// <param name="parameter">The argument captured for this invocation.</param>
	public void Invoke(in TParameter parameter)
	{
		if (Setups is null)
			return;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(parameter);

			if (x.Exception is not null)
				throw x.Exception;
		}
	}
}
