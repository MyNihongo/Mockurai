namespace MyNihongo.Mock.Sample;

public interface IPrimitiveTypeService
{
	void Invoke();

	void InvokeWithParameter(in string parameter);

	void InvokeWithMultipleParameters(in int parameter1, in int parameter2);

	decimal Return();

	string ReturnWithOneParameter(in string parameter);

	double ReturnWithMultipleParameters(in int parameter1, in int parameter2);
}
