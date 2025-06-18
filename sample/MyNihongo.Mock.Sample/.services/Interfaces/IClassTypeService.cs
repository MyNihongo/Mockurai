namespace MyNihongo.Mock.Sample;

public interface IClassTypeService
{
	void Invoke();

	void InvokeWithParameter(in ClassParameter1 parameter);

	void InvokeWithMultipleParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2);

	decimal Return();

	decimal? ReturnNullable();

	string ReturnWithOneParameter(in ClassParameter1 parameter);

	string? ReturnWithOneParameterNullable(in ClassParameter1 parameter);

	double ReturnWithMultipleParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2);

	double? ReturnWithMultipleParametersNullable(in ClassParameter1 parameter1, in ClassParameter1 parameter2);
}
