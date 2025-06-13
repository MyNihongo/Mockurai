namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public interface ISetupReturn<T>
{
	void Returns(in T? value);
}

[Obsolete("Will be generated")]
public sealed class Setup<T> : ISetupReturn<T>
{
	public T? Value { get; private set; }

	public void Returns(in T? value) =>
		Value = value;
}

[Obsolete("Will be generated")]
public sealed class SetupWithParameter<T> : ISetupReturn<T>
{
	private Dictionary<int, T?>? _values;
	private int? _currentParameter;

	public T? GetValue(in int parameterHashCode)
	{
		return _values is not null
			? _values.GetValueOrDefault(parameterHashCode)
			: default;
	}

	public void SetupParameters(in int parameterHashCode)
	{
		_currentParameter = parameterHashCode;
	}

	public void Returns(in T? value)
	{
		if (!_currentParameter.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, T?>();

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, _currentParameter.Value, out _);
		valueRef = value;

		_currentParameter = null;
	}
}

[Obsolete("Will be generated")]
public sealed class SetupWithMultipleParameters<T> : ISetupReturn<T>
{
	private Dictionary<int, (int[], T?)>? _values;
	private int[]? _currentParameters;

	public T? GetValue(in Span<int> parameterHashCodes)
	{
		if (_values is null)
			return default;

		foreach (var (parameters, value) in _values.Values)
		{
			for (var i = 0; i < parameterHashCodes.Length; i++)
			{
				if (parameterHashCodes[i] != parameters[i])
					goto Continue;
			}

			return value;

			Continue: ;
		}

		return default;
	}

	public void SetupParameters(in int[] parameterHashCodes)
	{
		_currentParameters = parameterHashCodes;
	}

	public void Returns(in T? value)
	{
		if (_currentParameters is null)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, (int[], T?)>();

		var hashCode = new HashCode();
		foreach (var currentParameter in _currentParameters)
			hashCode.Add(currentParameter);

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, hashCode.ToHashCode(), out _);
		valueRef = (_currentParameters, value);

		_currentParameters = null;
	}
}
