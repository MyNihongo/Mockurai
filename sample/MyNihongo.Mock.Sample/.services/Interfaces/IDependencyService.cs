namespace MyNihongo.Mock.Sample;

public interface IDependencyService
{
	int GetShopCount();

	string GetCustomerName(in string customerId);

	void SetShopName(in string shopName);

	Task<int> GetItemCountAsync(int itemId, CancellationToken ct = default);

	ValueTask<decimal> GetItemPriceAsync(int itemId, decimal deliveryCosts, CancellationToken ct = default);
}
