namespace MyNihongo.Mockurai.Sample;

public interface IRecordDependencyService
{
	void Invoke();

	void InvokeWithParameter(in RecordParameter1 parameter);

	void InvokeWithMultipleParameters(in RecordParameter1 parameter1, in RecordParameter1 parameter2);

	RecordReturn Return();

	RecordReturn? ReturnNullable();

	RecordReturn ReturnWithParameter(in RecordParameter1 parameter);

	RecordReturn? ReturnWithParameterNullable(in RecordParameter1 parameter);

	RecordReturn ReturnWithMultipleParameters(RecordParameter1 parameter1, RecordParameter1 parameter2);

	RecordReturn? ReturnWithMultipleParametersNullable(RecordParameter1 parameter1, RecordParameter1 parameter2);
}
