namespace MyNihongo.Mock.Sample;

public interface IClassDependencyService
{
	ClassReturn Return();

	ClassReturn? ReturnWithParameter(in ClassParameter1 parameter);

	ClassReturn ReturnWithMultipleParameters(ClassParameter1 parameter1, ClassParameter2 parameter2);
}
