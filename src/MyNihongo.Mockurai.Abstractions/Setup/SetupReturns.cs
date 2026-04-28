namespace MyNihongo.Mockurai;

/// <summary>
/// A returning mock setup whose configured callback is an <see cref="Action"/> and whose return-value
/// delegate is a <see cref="Func{TReturns}"/>.
/// </summary>
/// <typeparam name="TReturns">The return value type of the mocked member.</typeparam>
public sealed class Setup<TReturns> : SetupBaseReturns<Action, TReturns, Func<TReturns>>
{
	/// <summary>
	/// Executes the next queued action and produces its return value when one was configured.
	/// </summary>
	/// <param name="returnValue">When the method returns <see langword="true"/>, the configured return value; otherwise the default for <typeparamref name="TReturns"/>.</param>
	/// <returns><see langword="true"/> when a return value was produced; <see langword="false"/> when no return value was configured.</returns>
	public bool Execute(out TReturns? returnValue)
	{
		var x = GetSetup();
		x.Callback?.Invoke();

		if (x.Exception is not null)
			throw x.Exception;

		if (x.Returns is not null)
		{
			returnValue = x.Returns();
			return true;
		}

		returnValue = default;
		return false;
	}

	/// <inheritdoc/>
	public override void Returns(TReturns returns)
	{
		Returns(() => returns);
	}
}
