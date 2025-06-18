namespace MyNihongo.Mock.Sample;

public interface IStructTypeService
{
	void Invoke();

	void InvokeWithParameter(in StructParameter1 parameter);

	void InvokeWithMultipleParameters(in StructParameter1 parameter1, in StructParameter1 parameter2);

	decimal Return();

	decimal? ReturnNullable();

	string ReturnWithOneParameter(in StructParameter1 parameter);

	string? ReturnWithOneParameterNullable(in StructParameter1 parameter);

	double ReturnWithMultipleParameters(in StructParameter1 parameter1, in StructParameter1 parameter2);

	double? ReturnWithMultipleParametersNullable(in StructParameter1 parameter1, in StructParameter1 parameter2);
}
