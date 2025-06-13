namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class Setup<T>
{
	public T? Value { get; set; }
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

	public void SetupGetShopCount(int count) =>
		_getShopCount.Value = count;

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

		public Task<int> GetItemCountAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask<decimal> GetItemPriceAsync()
		{
			throw new NotImplementedException();
		}
	}
}

[Obsolete("Will be generated")]
public static class SampleEx
{
	public static void SetupGetShopCount(this IMock<IDependencyService> @this, in int returnValue)
	{
		((SampleMock)@this).SetupGetShopCount(returnValue);
	}
}
