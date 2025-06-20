namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public interface ISetup
{
	void Throws(in Exception exception);
}

[Obsolete("Will be generated")]
public interface ISetup<T> : ISetup
{
	void Returns(in T? value);
}

[Obsolete("Will be generated")]
public sealed class Setup : ISetup
{
	private Exception? _exception;

	public void Invoke()
	{
		if (_exception is not null)
			throw _exception;
	}

	public void Throws(in Exception exception)
	{
		_exception = exception;
	}
}

[Obsolete("Will be generated")]
public sealed class SetupWithParameter : ISetup
{
	private Dictionary<int, Exception?>? _values;
	private int? _currentParameter;

	public void Invoke(in int parameterHashCode)
	{
		if (_values is null)
			return;

		// TODO: maybe try _values.GetAlternateLookup() ?
		foreach (var pair in _values)
		{
			if ((parameterHashCode & pair.Key) != pair.Key)
				continue;

			if (pair.Value is not null)
				throw pair.Value;
		}
	}

	public void SetupParameters(in int parameterHashCode)
	{
		_currentParameter = parameterHashCode;
	}

	public void Throws(in Exception exception)
	{
		if (!_currentParameter.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, Exception?>();

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, _currentParameter.Value, out _);
		valueRef = exception;

		_currentParameter = null;
	}
}

[Obsolete("Will be generated")]
public sealed class SetupWithMultipleParameters : ISetup
{
	private Dictionary<int, (int[], Exception?)>? _values;
	private int[]? _currentParameters;

	public void Invoke(in Span<int> parameterHashCodes)
	{
		if (_values is null)
			return;

		foreach (var values in _values.Values)
		{
			for (var i = 0; i < parameterHashCodes.Length; i++)
			{
				if (parameterHashCodes[i] != values.Item1[i])
					goto Continue;
			}

			if (values.Item2 is not null)
				throw values.Item2;

			return;

			Continue: ;
		}
	}

	public void SetupParameters(in int[] parameterHashCodes)
	{
		_currentParameters = parameterHashCodes;
	}

	public void Throws(in Exception exception)
	{
		if (_currentParameters is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, (int[], Exception?)>();

		var hashCode = new HashCode();
		foreach (var currentParameter in _currentParameters)
			hashCode.Add(currentParameter);

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, hashCode.ToHashCode(), out _);
		valueRef = (_currentParameters, exception);

		_currentParameters = null;
	}
}

[Obsolete("Will be generated")]
public sealed class Setup<T> : ISetup<T>
{
	private Exception? _exception;
	private T? _value;

	public T? Invoke()
	{
		if (_exception is not null)
			throw _exception;

		return _value;
	}

	public void Returns(in T? value)
	{
		_value = value;
	}

	public void Throws(in Exception exception)
	{
		_exception = exception;
	}
}

[Obsolete("Will be generated")]
public sealed class SetupWithParameter<T> : ISetup<T>
{
	private Dictionary<int, (T?, Exception?)>? _values;
	private int? _currentParameter;

	public bool TryInvoke(in int parameterHashCode, out T? returnValue)
	{
		if (_values is not null && _values.TryGetValue(parameterHashCode, out var valueSetup))
		{
			if (valueSetup.Item2 is not null)
				throw valueSetup.Item2;

			returnValue = valueSetup.Item1;
			return true;
		}

		returnValue = default;
		return false;
	}

	public void SetupParameters(in int parameterHashCode)
	{
		_currentParameter = parameterHashCode;
	}

	public void Returns(in T? value)
	{
		if (!_currentParameter.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, (T?, Exception?)>();

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, _currentParameter.Value, out _);
		valueRef = (value, null);

		_currentParameter = null;
	}

	public void Throws(in Exception exception)
	{
		if (!_currentParameter.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, (T?, Exception?)>();

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, _currentParameter.Value, out _);
		valueRef = (default, exception);

		_currentParameter = null;
	}
}

[Obsolete("Will be generated")]
public sealed class SetupWithMultipleParameters<T> : ISetup<T>
{
	private Dictionary<int, (int[], T?, Exception?)>? _values;
	private int[]? _currentParameters;

	public bool TryInvoke(in Span<int> parameterHashCodes, out T? returnValue)
	{
		if (_values is null)
		{
			returnValue = default;
			return false;
		}

		foreach (var values in _values.Values)
		{
			for (var i = 0; i < parameterHashCodes.Length; i++)
			{
				if (parameterHashCodes[i] != values.Item1[i])
					goto Continue;
			}

			if (values.Item3 is not null)
				throw values.Item3;

			returnValue = values.Item2;
			return true;

			Continue: ;
		}

		returnValue = default;
		return false;
	}

	public void SetupParameters(in int[] parameterHashCodes)
	{
		_currentParameters = parameterHashCodes;
	}

	public void Returns(in T? value)
	{
		if (_currentParameters is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, (int[], T?, Exception?)>();

		var hashCode = new HashCode();
		foreach (var currentParameter in _currentParameters)
			hashCode.Add(currentParameter);

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, hashCode.ToHashCode(), out _);
		valueRef = (_currentParameters, value, null);

		_currentParameters = null;
	}

	public void Throws(in Exception exception)
	{
		if (_currentParameters is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, (int[], T?, Exception?)>();

		var hashCode = new HashCode();
		foreach (var currentParameter in _currentParameters)
			hashCode.Add(currentParameter);

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, hashCode.ToHashCode(), out _);
		valueRef = (_currentParameters, default, exception);

		_currentParameters = null;
	}
}
