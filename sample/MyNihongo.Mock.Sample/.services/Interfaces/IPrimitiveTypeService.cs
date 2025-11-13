namespace MyNihongo.Mock.Sample;

public interface IPrimitiveTypeService : IDisposable
{
	event EventHandler<string>? HandlerEvent;

	int Sum { get; }

	int GetOnly { get; }

	decimal SetOnly { set; }

	string GetInit { get; set; }

	void Invoke();

	void Invoke(out int result);

	void Invoke<T>();

	void InvokeWithParameter(in string parameter);

	void InvokeWithParameter(in int parameter);

	void InvokeWithParameter(ref decimal parameter);

	void InvokeWithParameter<T>(T parameter);

	void InvokeWithSeveralParameters(int parameter1, int parameter2);

	void InvokeWithSeveralParameters(ref int parameter1, int parameter2);

	void InvokeWithSeveralParameters(int parameter1, ref int parameter2);

	void InvokeWithSeveralParameters(ref int parameter1, ref int parameter2);

	void InvokeWithSeveralParameters<T>(T parameter1, int parameter2);

	decimal Return();

	bool Return(out string result);

	T Return<T>();

	string ReturnWithParameter(in string parameter);

	int ReturnWithParameter(ref double parameter);

	TReturn ReturnWithParameter<TParameter, TReturn>(TParameter parameter);

	double ReturnWithSeveralParameters(int parameter1, int parameter2);

	double ReturnWithSeveralParameters(ref int parameter1, int parameter2);

	double ReturnWithSeveralParameters(int parameter1, ref int parameter2);

	double ReturnWithSeveralParameters(ref int parameter1, ref int parameter2);

	TReturn ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref TParameter1 parameter1, TParameter2 parameter2);
}
