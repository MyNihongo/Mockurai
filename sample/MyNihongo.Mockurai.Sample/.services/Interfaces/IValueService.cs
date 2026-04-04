namespace MyNihongo.Mockurai.Sample;

public interface IValueTaskService
{
	ValueTask InvokeAsync(CancellationToken ct = default);

	ValueTask<decimal> ReturnPrimitiveAsync(CancellationToken ct = default);

	ValueTask<int?> ReturnPrimitiveNullableAsync(CancellationToken ct = default);

	ValueTask<string> ReturnStructAsync(CancellationToken ct = default);

	ValueTask<string?> ReturnStructNullableAsync(CancellationToken ct = default);

	ValueTask<string> ReturnClassAsync(CancellationToken ct = default);

	ValueTask<string?> ReturnClassNullableAsync(CancellationToken ct = default);

	ValueTask<string> ReturnRecordAsync(CancellationToken ct = default);

	ValueTask<string?> ReturnRecordNullableAsync(CancellationToken ct = default);
}
