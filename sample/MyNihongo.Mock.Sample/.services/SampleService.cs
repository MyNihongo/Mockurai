namespace MyNihongo.Mock.Sample;

internal sealed class SampleService : ISampleService
{
	private readonly IDependencyService _dependencyService;

	public SampleService(IDependencyService dependencyService)
	{
		_dependencyService = dependencyService;
	}

	public decimal ComputeDeliveryExpenses()
	{
		var shopCount = _dependencyService.GetShopCount();
		return shopCount * 1000m;
	}

	public async Task<decimal> ComputeRevenueAsync(int itemId, decimal deliveryCosts, CancellationToken ct = default)
	{
		var itemCount = await _dependencyService.GetItemCountAsync(itemId, ct);
		var itemPrice = await _dependencyService.GetItemPriceAsync(itemId, deliveryCosts, ct);

		return itemCount * itemPrice * 0.75m;
	}
}
