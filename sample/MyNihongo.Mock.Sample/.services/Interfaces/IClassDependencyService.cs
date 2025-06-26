namespace MyNihongo.Mock.Sample;

public interface IClassDependencyService
{
	void Invoke();

	void InvokeWithOneParameter(in ClassParameter1 parameter);

	void InvokeWithSeveralParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2);

	ClassReturn Return();

	ClassReturn? ReturnNullable();

	ClassReturn ReturnWithOneParameter(in ClassParameter1 parameter);

	ClassReturn? ReturnWithOneParameterNullable(in ClassParameter1 parameter);

	ClassReturn ReturnWithSeveralParameters(ClassParameter1 parameter1, ClassParameter1 parameter2);

	ClassReturn? ReturnWithSeveralParametersNullable(ClassParameter1 parameter1, ClassParameter1 parameter2);
}
