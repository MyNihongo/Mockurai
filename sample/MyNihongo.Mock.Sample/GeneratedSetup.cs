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

	public bool TryGetValue(in int parameterHashCode, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out T? value)
	{
		if (_values is not null && _values.TryGetValue(parameterHashCode, out value))
			return true;

		value = default;
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

	public bool TryGetValue(in Span<int> parameterHashCodes, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out T? value)
	{
		if (_values is null)
		{
			value = default;
			return false;
		}

		foreach (var values in _values.Values)
		{
			for (var i = 0; i < parameterHashCodes.Length; i++)
			{
				if (parameterHashCodes[i] != values.Item1[i])
					goto Continue;
			}

			value = values.Item2;
			return true;

			Continue: ;
		}

		value = default;
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

		_values ??= new Dictionary<int, (int[], T?)>();

		var hashCode = new HashCode();
		foreach (var currentParameter in _currentParameters)
			hashCode.Add(currentParameter);

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, hashCode.ToHashCode(), out _);
		valueRef = (_currentParameters, value);

		_currentParameters = null;
	}
}
