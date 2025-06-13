namespace MyNihongo.Mock.Sample;

public interface ISampleService
{
	decimal ComputeDeliveryExpenses();

	Task<decimal> ComputeRevenueAsync();
}
