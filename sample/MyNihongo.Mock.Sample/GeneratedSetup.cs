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
	private SetupContainer<(It<TParameter>.Setup Parameter, Exception Exception)>? _setups;
	private It<TParameter>.Setup? _tempSetup;
	private Exception? _defaultException;

	public void Invoke(in TParameter parameter)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (!setup.Parameter.Predicate(parameter))
				continue;

			throw setup.Exception;
		}

		Default:
		if (_defaultException is not null)
			throw _defaultException;
	}

	public void SetupParameter(in It<TParameter> parameter)
	{
		_tempSetup = parameter.ValueSetup;
	}

	public void Throws(in Exception exception)
	{
		if (!_tempSetup.HasValue)
		{
			_defaultException = exception;
			return;
		}

		_setups ??= [];
		_setups.Add((_tempSetup.Value, exception));
		_tempSetup = null;
	}
}

[Obsolete("Will be generated")]
public sealed class Setup<T> : ISetup<T>
{
	private Exception? _exception;
	private Func<T?>? _returns;

	public bool Execute(out T? returnValue)
	{
		if (_exception is not null)
			throw _exception;

		returnValue = _returns is not null ? _returns() : default;
		return true;
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
	private SetupContainer<(It<TParameter>.Setup? Parameter, Func<TParameter, TReturns?>? Returns, Exception? Exception)>? _setups;
	private It<TParameter>.Setup? _tempSetup;

	public bool Execute(in TParameter parameter, out TReturns? returnValue)
	{
		if (_setups is null)
			goto Default;

		foreach (var setup in _setups)
		{
			if (setup.Parameter.HasValue && !setup.Parameter.Value.Predicate(parameter))
				continue;

			if (setup.Exception is not null)
				throw setup.Exception;

			returnValue = setup.Returns is not null ? setup.Returns(parameter) : default;
			return true;
		}

		Default:
		returnValue = default;
		return false;
	}

	public void SetupParameter(in It<TParameter> parameter)
	{
		_tempSetup = parameter.ValueSetup;
	}

	public void Returns(TReturns? value)
	{
		Returns(_ => value);
	}

	public void Returns(in Func<TParameter, TReturns?> value)
	{
		_setups ??= [];
		_setups.Add((_tempSetup, value, null));
		_tempSetup = null;
	}

	public void Throws(in Exception exception)
	{
		_setups ??= [];
		_setups.Add((_tempSetup, null, exception));
		_tempSetup = null;
	}
}
