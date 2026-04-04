namespace MyNihongo.Mockurai.Sample;

public sealed class ValueTaskService : IValueTaskService
{
	private readonly IValueTaskDependencyService _valueTaskDependencyService;

	public ValueTaskService(IValueTaskDependencyService valueTaskDependencyService)
	{
		_valueTaskDependencyService = valueTaskDependencyService;
	}

	public async ValueTask InvokeAsync(CancellationToken ct = default)
	{
		await _valueTaskDependencyService.InvokeAsync(ct);
	}

	public async ValueTask<decimal> ReturnPrimitiveAsync(CancellationToken ct = default)
	{
		var result = await _valueTaskDependencyService.ReturnPrimitiveAsync(ct);
		return result + 2;
	}

	public async ValueTask<int?> ReturnPrimitiveNullableAsync(CancellationToken ct = default)
	{
		var result = await _valueTaskDependencyService.ReturnPrimitiveNullableAsync(ct);
		return result + 2;
	}

	public async ValueTask<string> ReturnStructAsync(CancellationToken ct = default)
	{
		var result = await _valueTaskDependencyService.ReturnStructAsync(ct);
		return $"name:{result.Name};age:{result.Age};yob:{result.DateOfBirth.Year}";
	}

	public async ValueTask<string?> ReturnStructNullableAsync(CancellationToken ct = default)
	{
		var result = await _valueTaskDependencyService.ReturnStructNullableAsync(ct);
		return result.HasValue ? $"name:{result.Value.Name};age:{result.Value.Age};yob:{result.Value.DateOfBirth.Year}" : null;
	}

	public async ValueTask<string> ReturnClassAsync(CancellationToken ct = default)
	{
		var result = await _valueTaskDependencyService.ReturnClassAsync(ct);
		return $"{result.Name} is {result.Age} years old, born in {result.DateOfBirth.Year}";
	}

	public async ValueTask<string?> ReturnClassNullableAsync(CancellationToken ct = default)
	{
		var result = await _valueTaskDependencyService.ReturnClassNullableAsync(ct);
		return result is not null ? $"{result.Name} is {result.Age} years old, born in {result.DateOfBirth.Year}" : null;
	}

	public async ValueTask<string> ReturnRecordAsync(CancellationToken ct = default)
	{
		var result = await _valueTaskDependencyService.ReturnRecordAsync(ct);
		return $"{result.Name} is {result.Age} years old, born in {result.DateOfBirth.Year}";
	}

	public async ValueTask<string?> ReturnRecordNullableAsync(CancellationToken ct = default)
	{
		var result = await _valueTaskDependencyService.ReturnRecordNullableAsync(ct);
		return result is not null ? $"{result.Name} is {result.Age} years old, born in {result.DateOfBirth.Year}" : null;
	}
}
