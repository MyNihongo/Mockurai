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

	public async Task<decimal> ComputeRevenueAsync()
	{
		var itemCount = await _dependencyService.GetItemCountAsync();
		var itemPrice = await _dependencyService.GetItemPriceAsync();

		return itemCount * itemPrice * 0.75m;
	}
}
