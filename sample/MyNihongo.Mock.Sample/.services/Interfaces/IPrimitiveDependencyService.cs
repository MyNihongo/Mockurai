namespace MyNihongo.Mock.Sample;

public interface IPrimitiveDependencyService
{
	int Return();

	string ReturnWithParameter(in string parameter);

	decimal ReturnWithMultipleParameters(int parameter1, int parameter2);
}
