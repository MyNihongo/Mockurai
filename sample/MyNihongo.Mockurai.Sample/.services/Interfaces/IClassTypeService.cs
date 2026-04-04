namespace MyNihongo.Mockurai.Sample;

public interface IClassTypeService
{
	void Invoke();

	void InvokeWithOneParameter(in ClassParameter1 parameter);

	void InvokeWithSeveralParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2);

	decimal Return();

	decimal? ReturnNullable();

	string ReturnWithOneParameter(in ClassParameter1 parameter);

	string? ReturnWithOneParameterNullable(in ClassParameter1 parameter);

	double ReturnWithSeveralParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2);

	double? ReturnWithSeveralParametersNullable(in ClassParameter1 parameter1, in ClassParameter1 parameter2);
}
