namespace MyNihongo.Mock;

public sealed class Setup<TReturns> : SetupBaseReturns<TReturns, Action, Func<TReturns?>>
{
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

	public override void Returns(TReturns? value)
	{
		Returns(() => value);
	}
}
