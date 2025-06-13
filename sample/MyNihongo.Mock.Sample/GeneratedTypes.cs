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

[Obsolete("Will be generated")]
public abstract class Mock<T> : IMock<T>
{
	private T? _object;

	public T Object => _object ??= CreateObject();

	protected abstract T CreateObject();
}

[Obsolete("Will be generated")]
public sealed class SampleMock : Mock<IPrimitiveDependencyService>
{
	private Setup<int>? _return;
	private SetupWithParameter<string>? _returnWithOneParameter;
	private SetupWithMultipleParameters<decimal>? _returnWithMultipleParameters;

	public Setup<int> SetupReturn() =>
		_return ??= new Setup<int>();

	public SetupWithParameter<string> SetupReturnWithOneParameter(in string parameter)
	{
		_returnWithOneParameter ??= new SetupWithParameter<string>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameter.SetupParameters(hashCode);
		return _returnWithOneParameter;
	}

	public SetupWithMultipleParameters<decimal> SetupReturnWithMultipleParameters(in int parameter1, in int parameter2)
	{
		_returnWithMultipleParameters ??= new SetupWithMultipleParameters<decimal>();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_returnWithMultipleParameters.SetupParameters(hashCodes);
		return _returnWithMultipleParameters;
	}

	protected override IPrimitiveDependencyService CreateObject() =>
		new Proxy(this);

	private sealed class Proxy : IPrimitiveDependencyService
	{
		private readonly SampleMock _mock;

		public Proxy(SampleMock mock)
		{
			_mock = mock;
		}

		public int Return() =>
			_mock._return?.Value ?? 0;

		public string ReturnWithParameter(in string parameter)
		{
			var hashcode = parameter.GetHashCode();
			return _mock._returnWithOneParameter?.GetValue(hashcode) ?? string.Empty;
		}

		public decimal ReturnWithMultipleParameters(int parameter1, int parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParameters?.GetValue(hashCodes) ?? 0m;
		}

		public void SetShopName(in string shopName)
		{
			throw new NotImplementedException();
		}

		public Task<int> GetItemCountAsync(int itemId, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public ValueTask<decimal> GetItemPriceAsync(int itemId, decimal deliveryCosts, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}
	}
}

[Obsolete("Will be generated")]
public static class SampleEx
{
	public static ISetupReturn<int> SetupReturn(this IMock<IPrimitiveDependencyService> @this) =>
		((SampleMock)@this).SetupReturn();

	public static ISetupReturn<string> SetupReturnWithOneParameter(this IMock<IPrimitiveDependencyService> @this, in string parameter) =>
		((SampleMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetupReturn<decimal> SetupReturnWithMultipleParameters(this IMock<IPrimitiveDependencyService> @this, in int parameter1, in int parameter2) =>
		((SampleMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);
}
