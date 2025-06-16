namespace MyNihongo.Mock.Sample;

public interface IRecordDependencyService
{
	RecordReturn Return();

	RecordReturn? ReturnNullable();

	RecordReturn ReturnWithParameter(in RecordParameter1 parameter);

	RecordReturn? ReturnWithParameterNullable(in RecordParameter1 parameter);

	RecordReturn ReturnWithMultipleParameters(RecordParameter1 parameter1, RecordParameter1 parameter2);

	RecordReturn? ReturnWithMultipleParametersNullable(RecordParameter1 parameter1, RecordParameter1 parameter2);
}
