namespace MyNihongo.Mock.Sample;

public interface IClassDependencyService
{
	void Invoke();

	void InvokeWithParameter(in ClassParameter1 parameter);

	void InvokeWithMultipleParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2);

	ClassReturn Return();

	ClassReturn? ReturnNullable();

	ClassReturn ReturnWithParameter(in ClassParameter1 parameter);

	ClassReturn? ReturnWithParameterNullable(in ClassParameter1 parameter);

	ClassReturn ReturnWithMultipleParameters(ClassParameter1 parameter1, ClassParameter1 parameter2);

	ClassReturn? ReturnWithMultipleParametersNullable(ClassParameter1 parameter1, ClassParameter1 parameter2);
}
