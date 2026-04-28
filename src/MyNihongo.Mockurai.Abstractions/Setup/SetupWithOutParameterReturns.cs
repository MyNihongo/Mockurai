namespace MyNihongo.Mockurai;

/// <summary>
/// A returning mock setup for a member with a single <see langword="out"/> parameter, whose callbacks assign the produced
/// value and whose return-value delegate computes the result.
/// </summary>
/// <typeparam name="TParameter">The argument type produced by the setup.</typeparam>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
public sealed class SetupWithOutParameter<TParameter, TReturns> : SetupBaseReturns<ActionOut<TParameter>, TReturns, FuncOut<TParameter, TReturns>>
{
	/// <summary>
	/// Executes the next queued action, assigning the <see langword="out"/> argument and producing a return value when one was configured.
	/// </summary>
	/// <param name="parameter">The output argument assigned by the configured callback or return-value delegate.</param>
	/// <param name="returnValue">When the method returns <see langword="true"/>, the configured return value; otherwise the default for <typeparamref name="TReturns"/>.</param>
	/// <returns><see langword="true"/> when a return value was produced; <see langword="false"/> when no return value was configured.</returns>
	public bool Execute(out TParameter? parameter, out TReturns? returnValue)
	{
		var x = GetSetup();
		if (x.Callback is not null)
			x.Callback.Invoke(out parameter);
		else
			parameter = default!;

		if (x.Exception is not null)
			throw x.Exception;

		if (x.Returns is not null)
		{
			returnValue = x.Returns(out parameter);
			return true;
		}

		returnValue = default;
		return false;
	}

	/// <inheritdoc/>
	public override void Returns(TReturns returns)
	{
		Returns((out parameter) =>
		{
			parameter = default!;
			return returns;
		});
	}
}
