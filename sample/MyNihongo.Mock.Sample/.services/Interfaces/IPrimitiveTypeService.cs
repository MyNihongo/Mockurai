namespace MyNihongo.Mock.Sample;

public interface IPrimitiveTypeService
{
	public event PrimitiveHandler? Handler;

	int GetOnly { get; }

	decimal SetOnly { set; }

	string GetInit { get; set; }

	void Invoke();

	void InvokeWithParameter(in string parameter);

	void InvokeWithParameter(in int parameter);

	void InvokeWithSeveralParameters(in int parameter1, in int parameter2);

	decimal Return();

	string ReturnWithParameter(in string parameter);

	double ReturnWithSeveralParameters(in int parameter1, in int parameter2);
}
