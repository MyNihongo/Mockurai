namespace MyNihongo.Mock.Sample;

public interface IDependencyService
{
	int GetShopCount();

	Task<int> GetItemCountAsync();

	ValueTask<decimal> GetItemPriceAsync();
}
