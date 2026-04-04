namespace MyNihongo.Mockurai;

public sealed class SetupWithRefReadOnlyParameter<TParameter, TReturns> : SetupWithParameterBase<TParameter, ActionRefReadOnly<TParameter>, TReturns, FuncRefReadOnly<TParameter, TReturns>>
{
	public bool Execute(ref readonly TParameter parameter, out TReturns? returnValue)
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
				returnValue = x.Returns(in parameter);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}

	public override void Returns(TReturns? returns)
	{
		Returns((ref readonly _) => returns);
	}
}
