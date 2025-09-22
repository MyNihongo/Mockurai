namespace MyNihongo.Mock.Sample;

public interface IPrimitiveTypeService : IDisposable
{
	event EventHandler<string>? HandlerEvent;

	int Sum { get; }

	int GetOnly { get; }

	decimal SetOnly { set; }

	string GetInit { get; set; }

	void Invoke();

	void InvokeWithParameter(in string parameter);

	void InvokeWithParameter(in int parameter);

	void InvokeWithParameter(ref decimal parameter);

	void InvokeWithSeveralParameters(int parameter1, int parameter2);

	void InvokeWithSeveralParameters(ref int parameter1, int parameter2);

	void InvokeWithSeveralParameters(int parameter1, ref int parameter2);

	void InvokeWithSeveralParameters(ref int parameter1, ref int parameter2);

	decimal Return();

	string ReturnWithParameter(in string parameter);

	int ReturnWithParameter(ref double parameter);

	double ReturnWithSeveralParameters(int parameter1, int parameter2);

	double ReturnWithSeveralParameters(ref int parameter1, int parameter2);

	double ReturnWithSeveralParameters(int parameter1, ref int parameter2);

	double ReturnWithSeveralParameters(ref int parameter1, ref int parameter2);
}
