namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public interface ISetup
{
	void Throws(in Exception exception);
}

[Obsolete("Will be generated")]
public interface ISetup<in T> : ISetup
{
	void Returns(T? value);
}

[Obsolete("Will be generated")]
public sealed class Setup : ISetup
{
	private Exception? _exception;
	private Action? _callback;

	public void Invoke()
	{
		_callback?.Invoke();

		if (_exception is not null)
			throw _exception;
	}

	public void Callback(in Action callback)
	{
		_callback = callback;
	}

	public void Throws(in Exception exception)
	{
		_exception = exception;
	}
}

[Obsolete("Will be generated")]
public sealed class SetupWithParameter<TParameter> : ISetup
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<Item>? _setups;
	private Item? _currentSetup;

	public void Invoke(in TParameter parameter)
	{
		if (_setups is null)
			return;

		foreach (var setup in _setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Predicate(parameter))
				continue;

			setup.Callback?.Invoke(parameter);

			if (setup.Exception is not null)
				throw setup.Exception;
		}
	}

	public void SetupParameter(in It<TParameter> parameter)
	{
		_currentSetup = new Item(parameter.ValueSetup);
		
		_setups ??= new SetupContainer<Item>(SortComparer);
		_setups.Add(_currentSetup);
	}

	public void Callback(in Action<TParameter> callback)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Callback = callback;
	}

	public void Throws(in Exception exception)
	{
		if (_currentSetup is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_currentSetup.Exception  = exception;
	}
	
	private sealed class Item(in It<TParameter>.Setup? parameter)
	{
		public readonly It<TParameter>.Setup? Parameter = parameter;
		public Action<TParameter>?  Callback;
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

[Obsolete("Will be generated")]
public sealed class Setup<T> : ISetup<T>
{
	private Exception? _exception;
	private Func<T?>? _returns;
	private Action? _callback;

	public bool Execute(out T? returnValue)
	{
		_callback?.Invoke();

		if (_exception is not null)
			throw _exception;

		if (_returns is not null)
		{
			returnValue = _returns();
			return true;
		}

		returnValue = default;
		return false;
	}

	public void Callback(in Action callback)
	{
		_callback = callback;
	}

	public void Returns(T? value)
	{
		Returns(() => value);
	}

	public void Returns(in Func<T?> value)
	{
		_returns = value;
	}

	public void Throws(in Exception exception)
	{
		_exception = exception;
	}
}

[Obsolete("Will be generated")]
public sealed class SetupWithParameter<TParameter, TReturns> : ISetup<TReturns>
{
	private static readonly Comparer SortComparer = new();
	private SetupContainer<(It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Func<TParameter, TReturns?>? Returns, Exception? Exception)>? _setups;
	private (It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Func<TParameter, TReturns?>? Returns, Exception? Exception)? _currentSetup;

	public bool Execute(in TParameter parameter, out TReturns? returnValue)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Predicate(parameter))
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

	public void SetupParameter(in It<TParameter> parameter)
	{
		_currentSetup = (parameter.ValueSetup, null, null, null);
	}

	public void Callback(in Action<TParameter> callback)
	{
		_setups ??= new SetupContainer<(It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Func<TParameter, TReturns?>? Returns, Exception? Exception)>(SortComparer);
		_currentSetup = _setups.Add((_currentSetup?.Parameter, callback, _currentSetup?.Returns, _currentSetup?.Exception));
	}

	public void Returns(TReturns? value)
	{
		Returns(_ => value);
	}

	public void Returns(in Func<TParameter, TReturns?> value)
	{
		_setups ??= new SetupContainer<(It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Func<TParameter, TReturns?>? Returns, Exception? Exception)>(SortComparer);
		_currentSetup = _setups.Add((_currentSetup?.Parameter, _currentSetup?.Callback, value, _currentSetup?.Exception));
	}

	public void Throws(in Exception exception)
	{
		_setups ??= new SetupContainer<(It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Func<TParameter, TReturns?>? Returns, Exception? Exception)>(SortComparer);
		_currentSetup = _setups.Add((_currentSetup?.Parameter, _currentSetup?.Callback, _currentSetup?.Returns, exception));
	}

	private sealed class Comparer : IComparer<(It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Func<TParameter, TReturns?>? Returns, Exception? Exception)>
	{
		public int Compare((It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Func<TParameter, TReturns?>? Returns, Exception? Exception) x, (It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Func<TParameter, TReturns?>? Returns, Exception? Exception) y)
		{
			var xSort = 0;
			var ySort = 0;

			if (x.Parameter.HasValue)
				xSort += x.Parameter.Value.Sort;

			if (y.Parameter.HasValue)
				ySort += y.Parameter.Value.Sort;

			return xSort.CompareTo(ySort);
		}
	}
}
