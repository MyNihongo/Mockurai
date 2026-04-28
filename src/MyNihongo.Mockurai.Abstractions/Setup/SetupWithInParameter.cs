namespace MyNihongo.Mockurai;

/// <summary>
/// A void mock setup that matches a single <see langword="in"/> argument and invokes <see cref="ActionIn{TParameter}"/> callbacks.
/// </summary>
/// <typeparam name="TParameter">The argument type captured by the setup.</typeparam>
public sealed class SetupWithInParameter<TParameter> : SetupWithParameterBase<TParameter, ActionIn<TParameter>>
{
	/// <summary>
	/// Invokes every configured setup whose argument matcher accepts <paramref name="parameter"/>, in matcher-precedence order.
	/// </summary>
	/// <param name="parameter">The argument captured for this invocation, passed by readonly reference.</param>
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
