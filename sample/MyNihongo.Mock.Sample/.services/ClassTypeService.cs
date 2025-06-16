namespace MyNihongo.Mock.Sample;

public sealed class ClassTypeService : IClassTypeService
{
	private readonly IClassDependencyService _classDependencyService;

	public ClassTypeService(IClassDependencyService classDependencyService)
	{
		_classDependencyService = classDependencyService;
	}

	public decimal ReturnWithoutParameters()
	{
		var result = _classDependencyService.Return();
		return result.Age * result.Name.Length + result.DateOfBirth.Year;
	}

	public string ReturnWithOneParameter(in ClassParameter1 parameter)
	{
		var result = _classDependencyService.ReturnWithParameter(parameter);
		return result is null ? string.Empty : $"name:{result.Name},age:{result.Age}";
	}

	public double ReturnWithMultipleParameters(in ClassParameter1 parameter1, in ClassParameter2 parameter2)
	{
		var result = _classDependencyService.ReturnWithMultipleParameters(parameter1, parameter2);
		return result.Age + result.DateOfBirth.Day - result.Name.Length;
	}
}
