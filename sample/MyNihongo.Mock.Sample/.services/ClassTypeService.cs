namespace MyNihongo.Mock.Sample;

public sealed class ClassTypeService : IClassTypeService
{
	private readonly IClassDependencyService _classDependencyService;

	public ClassTypeService(IClassDependencyService classDependencyService)
	{
		_classDependencyService = classDependencyService;
	}

	public void Invoke()
	{
		_classDependencyService.Invoke();
	}

	public void InvokeWithOneParameter(in ClassParameter1 parameter)
	{
		_classDependencyService.InvokeWithOneParameter(parameter);
	}

	public void InvokeWithSeveralParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2)
	{
		_classDependencyService.InvokeWithSeveralParameters(parameter1, parameter2);
	}

	public decimal Return()
	{
		var result = _classDependencyService.Return();
		return result.Age * result.Name.Length + result.DateOfBirth.Year;
	}

	public decimal? ReturnNullable()
	{
		var result = _classDependencyService.ReturnNullable();
		return result is not null ? result.Age * result.Name.Length + result.DateOfBirth.Year : null;
	}

	public string ReturnWithOneParameter(in ClassParameter1 parameter)
	{
		var result = _classDependencyService.ReturnWithOneParameter(parameter);
		return $"name:{result.Name},age:{result.Age}";
	}

	public string? ReturnWithOneParameterNullable(in ClassParameter1 parameter)
	{
		var result = _classDependencyService.ReturnWithOneParameterNullable(parameter);
		return result is not null ? $"name:{result.Name},age:{result.Age}" : null;
	}

	public double ReturnWithSeveralParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2)
	{
		var result = _classDependencyService.ReturnWithSeveralParameters(parameter1, parameter2);
		return result.Age + result.DateOfBirth.Day - result.Name.Length;
	}

	public double? ReturnWithSeveralParametersNullable(in ClassParameter1 parameter1, in ClassParameter1 parameter2)
	{
		var result = _classDependencyService.ReturnWithSeveralParametersNullable(parameter1, parameter2);
		return result is not null ? result.Age + result.DateOfBirth.Day - result.Name.Length : null;
	}
}
