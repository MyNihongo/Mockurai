namespace MyNihongo.Mock;

public sealed class Setup<TReturns> : SetupBaseReturns<TReturns, Action, Func<TReturns?>>
{
	public bool Execute(out TReturns? returnValue)
	{
		CallbackDelegate?.Invoke();

		if (Exception is not null)
			throw Exception;

		if (ReturnsDelegate is not null)
		{
			returnValue = ReturnsDelegate();
			return true;
		}

		returnValue = default;
		return false;
	}

	public override void Returns(TReturns? value)
	{
		Returns(() => value);
	}
}
