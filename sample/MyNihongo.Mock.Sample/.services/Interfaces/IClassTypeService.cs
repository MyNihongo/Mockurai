namespace MyNihongo.Mock.Sample;

public interface IClassTypeService
{
	decimal Return();

	decimal? ReturnNullable();

	string ReturnWithOneParameter(in ClassParameter1 parameter);

	string? ReturnWithOneParameterNullable(in ClassParameter1 parameter);

	double ReturnWithMultipleParameters(in ClassParameter1 parameter1, in ClassParameter2 parameter2);

	double? ReturnWithMultipleParametersNullable(in ClassParameter1 parameter1, in ClassParameter2 parameter2);
}
