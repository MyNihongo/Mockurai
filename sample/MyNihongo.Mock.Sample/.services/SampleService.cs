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

	public double ReturnWithMultipleParameters(in DateOnly date)
	{
		var spending = _dependencyService.GetCustomerSpending(date.Year, date.Month);
		var spendingDouble = Convert.ToDouble(spending);

		return Math.Pow(spendingDouble, 2d);
	}

	public async Task<decimal> ReturnTaskWithMultipleParametersAsync(int itemId, decimal deliveryCosts, CancellationToken ct = default)
	{
		var itemCount = await _dependencyService.GetItemCountAsync(itemId, ct);
		var itemPrice = await _dependencyService.GetItemPriceAsync(itemId, deliveryCosts, ct);

		return itemCount * itemPrice * 0.75m;
	}
}
