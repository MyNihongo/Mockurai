namespace MyNihongo.Mock.Sample;

public interface IPrimitiveDependencyService
{
	event PrimitiveHandler? Handler;
	event EventHandler<string>? HandlerEvent;

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

	int Return();

	bool Return(out string result);

	T Return<T>();

	string ReturnWithParameter(in string parameter);

	int ReturnWithParameter(ref double parameter);

	TReturn ReturnWithParameter<TParameter, TReturn>(TParameter parameter);

	decimal ReturnWithSeveralParameters(int parameter1, int parameter2);

	decimal ReturnWithSeveralParameters(ref int parameter1, int parameter2);

	decimal ReturnWithSeveralParameters(int parameter1, ref int parameter2);

	decimal ReturnWithSeveralParameters(ref int parameter1, ref int parameter2);
}
