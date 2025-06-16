namespace MyNihongo.Mock.Sample;

public interface IRecordTypeService
{
	decimal Return();

	decimal? ReturnNullable();

	string ReturnWithOneParameter(in RecordParameter1 parameter);

	string? ReturnWithOneParameterNullable(in RecordParameter1 parameter);

	double ReturnWithMultipleParameters(in RecordParameter1 parameter1, in RecordParameter1 parameter2);

	double? ReturnWithMultipleParametersNullable(in RecordParameter1 parameter1, in RecordParameter1 parameter2);
}
