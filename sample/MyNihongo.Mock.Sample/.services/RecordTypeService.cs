namespace MyNihongo.Mock.Sample;

public class RecordTypeService : IRecordTypeService
{
	private readonly IRecordDependencyService _recordDependencyService;

	public RecordTypeService(IRecordDependencyService recordDependencyService)
	{
		_recordDependencyService = recordDependencyService;
	}

	public void Invoke()
	{
		_recordDependencyService.Invoke();
	}

	public void InvokeWithParameter(in RecordParameter1 parameter)
	{
		_recordDependencyService.InvokeWithParameter(parameter);
	}

	public void InvokeWithMultipleParameters(in RecordParameter1 parameter1, in RecordParameter1 parameter2)
	{
		_recordDependencyService.InvokeWithMultipleParameters(parameter1, parameter2);
	}

	public decimal Return()
	{
		var result = _recordDependencyService.Return();
		return result.Age * result.Name.Length + result.DateOfBirth.Year;
	}

	public decimal? ReturnNullable()
	{
		var result = _recordDependencyService.ReturnNullable();
		return result is not null ? result.Age * result.Name.Length + result.DateOfBirth.Year : null;
	}

	public string ReturnWithOneParameter(in RecordParameter1 parameter)
	{
		var result = _recordDependencyService.ReturnWithParameter(parameter);
		return $"name:{result.Name},age:{result.Age}";
	}

	public string? ReturnWithOneParameterNullable(in RecordParameter1 parameter)
	{
		var result = _recordDependencyService.ReturnWithParameterNullable(parameter);
		return result is not null ? $"name:{result.Name},age:{result.Age}" : null;
	}

	public double ReturnWithMultipleParameters(in RecordParameter1 parameter1, in RecordParameter1 parameter2)
	{
		var result = _recordDependencyService.ReturnWithMultipleParameters(parameter1, parameter2);
		return result.Age + result.DateOfBirth.Day - result.Name.Length;
	}

	public double? ReturnWithMultipleParametersNullable(in RecordParameter1 parameter1, in RecordParameter1 parameter2)
	{
		var result = _recordDependencyService.ReturnWithMultipleParametersNullable(parameter1, parameter2);
		return result is not null ? result.Age + result.DateOfBirth.Day - result.Name.Length : null;
	}
}
