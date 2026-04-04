namespace MyNihongo.Mockurai.Sample;

public interface ITaskService
{
	Task InvokeAsync(CancellationToken ct = default);
	
	Task<decimal> ReturnPrimitiveAsync(CancellationToken ct = default);

	Task<int?> ReturnPrimitiveNullableAsync(CancellationToken ct = default);

	Task<string> ReturnStructAsync(CancellationToken ct = default);

	Task<string?> ReturnStructNullableAsync(CancellationToken ct = default);

	Task<string> ReturnClassAsync(CancellationToken ct = default);

	Task<string?> ReturnClassNullableAsync(CancellationToken ct = default);

	Task<string> ReturnRecordAsync(CancellationToken ct = default);

	Task<string?> ReturnRecordNullableAsync(CancellationToken ct = default);
}
