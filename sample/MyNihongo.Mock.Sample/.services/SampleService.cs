namespace MyNihongo.Mock.Sample;

internal sealed class SampleService : ISampleService
{
	private readonly IDependencyService _dependencyService;

	public SampleService(IDependencyService dependencyService)
	{
		_dependencyService = dependencyService;
	}

	public decimal ReturnWithoutParameters()
	{
		var shopCount = _dependencyService.GetShopCount();
		return shopCount * 1000m;
	}

	public CustomerModel ReturnWithOneParameter(in string customerId)
	{
		var name = _dependencyService.GetCustomerName(customerId);

		return new CustomerModel
		{
			Name = name,
			Age = 32,
		};
	}

	public async Task<decimal> ReturnTaskWithMultipleParametersAsync(int itemId, decimal deliveryCosts, CancellationToken ct = default)
	{
		var itemCount = await _dependencyService.GetItemCountAsync(itemId, ct);
		var itemPrice = await _dependencyService.GetItemPriceAsync(itemId, deliveryCosts, ct);

		return itemCount * itemPrice * 0.75m;
	}
}
