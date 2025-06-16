namespace MyNihongo.Mock.Sample;

public interface IClassDependencyService
{
	ClassReturn Return();

	ClassReturn? ReturnNullable();

	ClassReturn ReturnWithParameter(in ClassParameter1 parameter);

	ClassReturn? ReturnWithParameterNullable(in ClassParameter1 parameter);

	ClassReturn ReturnWithMultipleParameters(ClassParameter1 parameter1, ClassParameter1 parameter2);

	ClassReturn? ReturnWithMultipleParametersNullable(ClassParameter1 parameter1, ClassParameter1 parameter2);
}
