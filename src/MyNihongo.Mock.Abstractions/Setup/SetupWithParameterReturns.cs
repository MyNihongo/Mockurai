namespace MyNihongo.Mock;

public sealed class SetupWithParameter<TParameter, TReturns> : SetupWithParameterBase<TParameter, TReturns, Action<TParameter>>
{
	public bool Execute(in TParameter parameter, out TReturns? returnValue)
	{
		if (Setups is null)
			goto Default;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			setup.Callback?.Invoke(parameter);

			if (setup.Exception is not null)
				throw setup.Exception;

			if (setup.Returns is not null)
			{
				returnValue = setup.Returns(parameter);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}
}

public sealed class SetupWithRefParameter<TParameter, TReturns> : SetupWithParameterBase<TParameter, TReturns, ActionRef<TParameter>>
{
	public bool Execute(ref TParameter parameter, out TReturns? returnValue)
	{
		if (Setups is null)
			goto Default;

		foreach (var setup in Setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Check(parameter))
				continue;

			setup.Callback?.Invoke(ref parameter);

			if (setup.Exception is not null)
				throw setup.Exception;

			if (setup.Returns is not null)
			{
				returnValue = setup.Returns(parameter);
				return true;
			}

			returnValue = default;
			return false;
		}

		Default:
		returnValue = default;
		return false;
	}
}

public abstract class SetupWithParameterBase<TParameter, TReturns, TDelegate> : ISetup<TReturns>
{
	private static readonly Comparer SortComparer = new();
	protected SetupContainer<Item>? Setups;
	private Item? _currentSetup;

	public void SetupParameter(in It<TParameter> parameter)
	{
		_currentSetup = new Item(parameter.ValueSetup);

		Setups ??= new SetupContainer<Item>(SortComparer);
		Setups.Add(_currentSetup);
	}

	public void Callback(in TDelegate callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Callback = callback;
	}

	public void Returns(TReturns? value)
	{
		Returns(_ => value);
	}

	public void Returns(in Func<TParameter, TReturns?> value)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Returns = value;
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Exception = exception;
	}

	protected sealed class Item(in It<TParameter>.Setup? parameter)
	{
		public readonly It<TParameter>.Setup? Parameter = parameter;
		public TDelegate? Callback;
		public Func<TParameter, TReturns?>? Returns;
		public Exception? Exception;
	}

	private sealed class Comparer : IComparer<Item>
	{
		public int Compare(Item? x, Item? y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x?.Parameter.HasValue == true)
				xSort += x.Parameter.Value.Sort;

			if (y?.Parameter.HasValue == true)
				ySort += y.Parameter.Value.Sort;

			return xSort.CompareTo(ySort);
		}
	}
}
