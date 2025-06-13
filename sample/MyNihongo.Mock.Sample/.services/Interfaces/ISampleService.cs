namespace MyNihongo.Mock.Sample;

public interface ISampleService
{
	decimal ComputeDeliveryExpenses();

	Task<decimal> ComputeRevenueAsync(int itemId, decimal deliveryCosts, CancellationToken ct = default);
}
