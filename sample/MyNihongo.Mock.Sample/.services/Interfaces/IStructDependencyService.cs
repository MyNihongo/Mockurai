namespace MyNihongo.Mock.Sample;

public interface IStructDependencyService
{
	StructReturn Return();

	StructReturn? ReturnNullable();

	StructReturn ReturnWithParameter(in StructParameter1 parameter);

	StructReturn? ReturnWithParameterNullable(in StructParameter1 parameter);

	StructReturn ReturnWithMultipleParameters(StructParameter1 parameter1, StructParameter1 parameter2);

	StructReturn? ReturnWithMultipleParametersNullable(StructParameter1 parameter1, StructParameter1 parameter2);
}
