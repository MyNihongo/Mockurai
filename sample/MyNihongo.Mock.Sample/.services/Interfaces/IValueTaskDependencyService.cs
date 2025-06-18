namespace MyNihongo.Mock.Sample;

public interface IValueTaskDependencyService
{
	ValueTask InvokeAsync(CancellationToken ct = default);

	ValueTask<int> ReturnPrimitiveAsync(CancellationToken ct = default);

	ValueTask<short?> ReturnPrimitiveNullableAsync(CancellationToken ct = default);

	ValueTask<StructReturn> ReturnStructAsync(CancellationToken ct = default);

	ValueTask<StructReturn?> ReturnStructNullableAsync(CancellationToken ct = default);

	ValueTask<ClassReturn> ReturnClassAsync(CancellationToken ct = default);

	ValueTask<ClassReturn?> ReturnClassNullableAsync(CancellationToken ct = default);

	ValueTask<RecordReturn> ReturnRecordAsync(CancellationToken ct = default);

	ValueTask<RecordReturn?> ReturnRecordNullableAsync(CancellationToken ct = default);
}
