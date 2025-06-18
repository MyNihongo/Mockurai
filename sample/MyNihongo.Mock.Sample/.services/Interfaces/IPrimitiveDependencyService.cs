namespace MyNihongo.Mock.Sample;

public interface IPrimitiveDependencyService
{
	void Invoke();

	void InvokeWithParameter(in string parameter);

	void InvokeWithMultipleParameters(in int parameter1, in int parameter2);

	int Return();

	string ReturnWithParameter(in string parameter);

	decimal ReturnWithMultipleParameters(int parameter1, int parameter2);
}
