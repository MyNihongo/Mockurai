namespace MyNihongo.Mock.Sample;

public interface IStructTypeService
{
	decimal Return();

	decimal? ReturnNullable();

	string ReturnWithOneParameter(in StructParameter1 parameter);

	string? ReturnWithOneParameterNullable(in StructParameter1 parameter);

	double ReturnWithMultipleParameters(in StructParameter1 parameter1, in StructParameter1 parameter2);

	double? ReturnWithMultipleParametersNullable(in StructParameter1 parameter1, in StructParameter1 parameter2);
}
