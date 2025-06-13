namespace MyNihongo.Mock.Sample;

public interface ISampleService
{
	decimal ReturnWithoutParameters();

	CustomerModel ReturnWithOneParameter(in string customerId);

	double ReturnWithMultipleParameters(in DateOnly date);

	Task<decimal> ReturnTaskWithMultipleParametersAsync(int itemId, decimal deliveryCosts, CancellationToken ct = default);
}
