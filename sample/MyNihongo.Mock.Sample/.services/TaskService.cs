namespace MyNihongo.Mock.Sample;

public sealed class TaskService : ITaskService
{
	private readonly ITaskDependencyService _taskDependencyService;

	public TaskService(ITaskDependencyService taskDependencyService)
	{
		_taskDependencyService = taskDependencyService;
	}
	
	public async Task InvokeAsync(CancellationToken ct = default)
	{
		await _taskDependencyService.InvokeAsync(ct);
	}

	public async Task<decimal> ReturnPrimitiveAsync(CancellationToken ct = default)
	{
		var result = await _taskDependencyService.ReturnPrimitiveAsync(ct);
		return result + 2;
	}

	public async Task<int?> ReturnPrimitiveNullableAsync(CancellationToken ct = default)
	{
		var result = await _taskDependencyService.ReturnPrimitiveNullableAsync(ct);
		return result + 2;
	}

	public async Task<string> ReturnStructAsync(CancellationToken ct = default)
	{
		var result = await _taskDependencyService.ReturnStructAsync(ct);
		return $"name:{result.Name};age:{result.Age};yob:{result.DateOfBirth.Year}";
	}

	public async Task<string?> ReturnStructNullableAsync(CancellationToken ct = default)
	{
		var result = await _taskDependencyService.ReturnStructNullableAsync(ct);
		return result.HasValue ? $"name:{result.Value.Name};age:{result.Value.Age};yob:{result.Value.DateOfBirth.Year}" : null;
	}

	public async Task<string> ReturnClassAsync(CancellationToken ct = default)
	{
		var result = await _taskDependencyService.ReturnClassAsync(ct);
		return $"{result.Name} is {result.Age} years old, born in {result.DateOfBirth.Year}";
	}

	public async Task<string?> ReturnClassNullableAsync(CancellationToken ct = default)
	{
		var result = await _taskDependencyService.ReturnClassNullableAsync(ct);
		return result is not null ? $"{result.Name} is {result.Age} years old, born in {result.DateOfBirth.Year}" : null;
	}

	public async Task<string> ReturnRecordAsync(CancellationToken ct = default)
	{
		var result = await _taskDependencyService.ReturnRecordAsync(ct);
		return $"{result.Name} is {result.Age} years old, born in {result.DateOfBirth.Year}";
	}

	public async Task<string?> ReturnRecordNullableAsync(CancellationToken ct = default)
	{
		var result = await _taskDependencyService.ReturnRecordNullableAsync(ct);
		return result is not null ? $"{result.Name} is {result.Age} years old, born in {result.DateOfBirth.Year}" : null;
	}
}
