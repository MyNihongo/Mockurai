namespace MyNihongo.Mockurai.Sample;

public interface IPrimitiveDependencyService
{
	event PrimitiveHandler? Handler;
	event EventHandler<string>? HandlerEvent;

	int GetOnly { get; }

	decimal SetOnly { set; }

	string GetInit { get; set; }

	string? this[int key] { get; set; }

	void Invoke();

	void Invoke(out int result);

	void Invoke<T>();

	Task InvokeAsync();

	void InvokeWithParameter(in string parameter);

	void InvokeWithParameter(in int parameter);

	void InvokeWithParameter(ref decimal parameter);

	void InvokeWithParameter<T>(T parameter);

	Task InvokeWithParameterAsync(int parameter);

	void InvokeWithSeveralParameters(int parameter1, int parameter2);

	void InvokeWithSeveralParameters(ref int parameter1, int parameter2);

	void InvokeWithSeveralParameters(int parameter1, ref int parameter2);

	void InvokeWithSeveralParameters(ref int parameter1, ref int parameter2);

	void InvokeWithSeveralParameters<T>(T parameter1, int parameter2);

	Task InvokeWithSeveralParametersAsync(int parameter1, int parameter2);

	int Return();

	bool Return(out string result);

	T Return<T>();

	ValueTask<bool> ReturnAsync();

	string ReturnWithParameter(in string parameter);

	int ReturnWithParameter(ref double parameter);

	TReturn ReturnWithParameter<TParameter, TReturn>(TParameter parameter);

	ValueTask<int> ReturnWithParameterAsync(string parameter);

	decimal ReturnWithSeveralParameters(int parameter1, int parameter2);

	decimal ReturnWithSeveralParameters(ref int parameter1, int parameter2);

	decimal ReturnWithSeveralParameters(int parameter1, ref int parameter2);

	decimal ReturnWithSeveralParameters(ref int parameter1, ref int parameter2);

	TReturn ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref TParameter1 parameter1, TParameter2 parameter2);

	ValueTask<decimal> ReturnWithSeveralParametersAsync(int parameter1, int parameter2);
}
