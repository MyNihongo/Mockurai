namespace MyNihongo.Mock.Sample;

public interface ITaskDependencyService
{
	Task InvokeAsync(CancellationToken ct = default);

	Task<int> ReturnPrimitiveAsync(CancellationToken ct = default);

	Task<short?> ReturnPrimitiveNullableAsync(CancellationToken ct = default);

	Task<StructReturn> ReturnStructAsync(CancellationToken ct = default);

	Task<StructReturn?> ReturnStructNullableAsync(CancellationToken ct = default);

	Task<ClassReturn> ReturnClassAsync(CancellationToken ct = default);

	Task<ClassReturn?> ReturnClassNullableAsync(CancellationToken ct = default);

	Task<RecordReturn> ReturnRecordAsync(CancellationToken ct = default);

	Task<RecordReturn?> ReturnRecordNullableAsync(CancellationToken ct = default);
}
