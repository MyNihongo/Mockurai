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

			Continue:
			continue;
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
public sealed class SampleMock : Mock<IDependencyService>
{
	private Setup<int>? _getShopCount;
	private SetupWithParameter<string>? _getCustomerName;
	private SetupWithMultipleParameters<decimal>? _getCustomerSpending;

	public Setup<int> SetupGetShopCount() =>
		_getShopCount ??= new Setup<int>();

	public SetupWithParameter<string> SetupGetCustomerName(in string customerId)
	{
		_getCustomerName ??= new SetupWithParameter<string>();

		var hashCode = customerId.GetHashCode();
		_getCustomerName.SetupParameters(hashCode);
		return _getCustomerName;
	}

	public SetupWithMultipleParameters<decimal> SetupGetCustomerSpending(in int year, in int month)
	{
		_getCustomerSpending ??= new SetupWithMultipleParameters<decimal>();

		var hashCodes = new[]
		{
			year.GetHashCode(),
			month.GetHashCode(),
		};

		_getCustomerSpending.SetupParameters(hashCodes);
		return _getCustomerSpending;
	}

	protected override IDependencyService CreateObject() =>
		new Proxy(this);

	private sealed class Proxy : IDependencyService
	{
		private readonly SampleMock _mock;

		public Proxy(SampleMock mock)
		{
			_mock = mock;
		}

		public int GetShopCount() =>
			_mock._getShopCount?.Value ?? 0;

		public string GetCustomerName(in string customerId)
		{
			var hashcode = customerId.GetHashCode();
			return _mock._getCustomerName?.GetValue(hashcode) ?? string.Empty;
		}

		public decimal GetCustomerSpending(int year, int month)
		{
			Span<int> hashCodes = stackalloc int[] { year.GetHashCode(), month.GetHashCode() };
			return _mock._getCustomerSpending?.GetValue(hashCodes) ?? 0m;
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
	public static ISetupReturn<int> SetupGetShopCount(this IMock<IDependencyService> @this) =>
		((SampleMock)@this).SetupGetShopCount();

	public static ISetupReturn<string> SetupGetCustomerName(this IMock<IDependencyService> @this, in string customerId) =>
		((SampleMock)@this).SetupGetCustomerName(customerId);

	public static ISetupReturn<decimal> SetupGetCustomerSpending(this IMock<IDependencyService> @this, in int year, in int month) =>
		((SampleMock)@this).SetupGetCustomerSpending(year, month);
}
