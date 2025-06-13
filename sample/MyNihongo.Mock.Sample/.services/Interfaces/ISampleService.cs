namespace MyNihongo.Mock.Sample;

public interface ISampleService
{
	decimal ReturnWithoutParameters();

	CustomerModel ReturnWithOneParameter(in string customerId);

	Task<decimal> ReturnTaskWithMultipleParametersAsync(int itemId, decimal deliveryCosts, CancellationToken ct = default);
}
