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
	private SetupContainer<(It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Exception? Exception)>? _setups;
	private (It<TParameter>.Setup? Parameter, Action<TParameter>? Callback, Exception? Exception)? _currentSetup;
	private Exception? _defaultException;
	private Action<TParameter>? _defaultCallback;

	public void Invoke(in TParameter parameter)
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
		}

		Default:
		_defaultCallback?.Invoke(parameter);

		if (_defaultException is not null)
			throw _defaultException;
	}

	public void SetupParameter(in It<TParameter> parameter)
	{
		_currentSetup = (parameter.ValueSetup, null, null);
	}

	public void Callback(in Action<TParameter> callback)
	{
		if (!_currentSetup.HasValue)
		{
			_defaultCallback = callback;
			return;
		}

		_setups ??= [];
		_currentSetup = _setups.Add((_currentSetup.Value.Parameter, callback, _currentSetup.Value.Exception));
	}

	public void Throws(in Exception exception)
	{
		if (!_currentSetup.HasValue)
		{
			_defaultException = exception;
			return;
		}

		_setups ??= [];
		_currentSetup = _setups.Add((_currentSetup.Value.Parameter, _currentSetup.Value.Callback, exception));
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
		_setups ??= [];
		_currentSetup = _setups.Add((_currentSetup?.Parameter, callback, _currentSetup?.Returns, _currentSetup?.Exception));
	}

	public void Returns(TReturns? value)
	{
		Returns(_ => value);
	}

	public void Returns(in Func<TParameter, TReturns?> value)
	{
		_setups ??= [];
		_currentSetup = _setups.Add((_currentSetup?.Parameter, _currentSetup?.Callback, value, _currentSetup?.Exception));
	}

	public void Throws(in Exception exception)
	{
		_setups ??= [];
		_currentSetup = _setups.Add((_currentSetup?.Parameter, _currentSetup?.Callback, _currentSetup?.Returns, exception));
	}
}
