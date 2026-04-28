namespace MyNihongo.Mockurai;

/// <summary>
/// A returning mock setup that matches a single <see langword="in"/> argument, invokes <see cref="ActionIn{TParameter}"/> callbacks,
/// and computes return values via <see cref="FuncIn{TParameter, TReturns}"/>.
/// </summary>
/// <typeparam name="TParameter">The argument type captured by the setup.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
public sealed class SetupWithInParameter<TParameter, TReturns> : SetupWithParameterBase<TParameter, ActionIn<TParameter>, TReturns, FuncIn<TParameter, TReturns>>
{
	/// <summary>
	/// Executes the first matching setup for <paramref name="parameter"/> and produces a return value when one was configured.
	/// </summary>
	/// <param name="parameter">The argument captured for this invocation, passed by readonly reference.</param>
	/// <param name="returnValue">When the method returns <see langword="true"/>, the configured return value; otherwise the default for <typeparamref name="TReturns"/>.</param>
	/// <returns><see langword="true"/> when a matching setup produced a return value; <see langword="false"/> otherwise.</returns>
	public bool Execute(in TParameter parameter, out TReturns? returnValue)
	{
		if (Setups is null)
			goto Default;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			var x = setup.GetSetup();
			x.Callback?.Invoke(in parameter);

			if (x.Exception is not null)
				throw x.Exception;

			if (x.Returns is not null)
			{
				returnValue = x.Returns(parameter);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default!;
		return false;
	}

	/// <inheritdoc/>
	public override void Returns(TReturns returns)
	{
		Returns((in _) => returns);
	}
}
