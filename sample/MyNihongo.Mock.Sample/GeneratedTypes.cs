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
	private int? _currentParameters;

	public T? GetValue(in int parameterHashcode)
	{
		return _values is not null
			? _values.GetValueOrDefault(parameterHashcode)
			: default;
	}

	public void SetupParameters(in int parameterHashcode)
	{
		_currentParameters = parameterHashcode;
	}

	public void Returns(in T? value)
	{
		if (!_currentParameters.HasValue)
			throw new InvalidOperationException("Parameters are not set, call SetupParameters first!");

		_values ??= new Dictionary<int, T?>();

		ref var valueRef = ref System.Runtime.InteropServices.CollectionsMarshal.GetValueRefOrAddDefault(_values, _currentParameters.Value, out _);
		valueRef = value;

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
public sealed class SampleMock : Mock<IDependencyService>
{
	private readonly Setup<int> _getShopCount = new();
	private readonly SetupWithParameter<string> _getCustomerName = new();

	public Setup<int> SetupGetShopCount() =>
		_getShopCount;

	public SetupWithParameter<string> SetupGetCustomerName(in string customerId)
	{
		var hashCode = customerId.GetHashCode();
		_getCustomerName.SetupParameters(hashCode);
		return _getCustomerName;
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
			_mock._getShopCount.Value;

		public string GetCustomerName(in string customerId)
		{
			var hashcode = customerId.GetHashCode();
			return _mock._getCustomerName.GetValue(hashcode) ?? string.Empty;
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
}
