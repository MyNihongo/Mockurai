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

	public Setup<int> SetupGetShopCount() =>
		_getShopCount;

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
			throw new NotImplementedException();
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
}
