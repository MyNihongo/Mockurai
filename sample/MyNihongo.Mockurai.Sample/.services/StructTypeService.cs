namespace MyNihongo.Mockurai.Sample;

public sealed class StructTypeService : IStructTypeService
{
	private readonly IStructDependencyService _structDependencyService;

	public StructTypeService(IStructDependencyService structDependencyService)
	{
		_structDependencyService = structDependencyService;
	}
	
	public void Invoke()
	{
		_structDependencyService.Invoke();
	}

	public void InvokeWithParameter(in StructParameter1 parameter)
	{
		_structDependencyService.InvokeWithParameter(parameter);
	}

	public void InvokeWithMultipleParameters(in StructParameter1 parameter1, in StructParameter1 parameter2)
	{
		_structDependencyService.InvokeWithMultipleParameters(parameter1, parameter2);
	}

	public decimal Return()
	{
		var result = _structDependencyService.Return();
		return result.Age * result.Name.Length + result.DateOfBirth.Year;
	}

	public decimal? ReturnNullable()
	{
		var result = _structDependencyService.ReturnNullable();
		return result.HasValue ? result.Value.Age * result.Value.Name.Length + result.Value.DateOfBirth.Year : null;
	}

	public string ReturnWithOneParameter(in StructParameter1 parameter)
	{
		var result = _structDependencyService.ReturnWithParameter(parameter);
		return $"name:{result.Name},age:{result.Age}";
	}

	public string? ReturnWithOneParameterNullable(in StructParameter1 parameter)
	{
		var result = _structDependencyService.ReturnWithParameterNullable(parameter);
		return result.HasValue ? $"name:{result.Value.Name},age:{result.Value.Age}" : null;
	}

	public double ReturnWithMultipleParameters(in StructParameter1 parameter1, in StructParameter1 parameter2)
	{
		var result = _structDependencyService.ReturnWithMultipleParameters(parameter1, parameter2);
		return result.Age + result.DateOfBirth.Day - result.Name.Length;
	}

	public double? ReturnWithMultipleParametersNullable(in StructParameter1 parameter1, in StructParameter1 parameter2)
	{
		var result = _structDependencyService.ReturnWithMultipleParametersNullable(parameter1, parameter2);
		return result.HasValue ? result.Value.Age + result.Value.DateOfBirth.Day - result.Value.Name.Length : null;
	}
}
