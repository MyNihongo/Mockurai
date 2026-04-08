namespace MyNihongo.Mockurai.Sample;

public interface IPrimitiveTypeService : IDisposable
{
	event EventHandler<string>? HandlerEvent;

	int Sum { get; }

	int GetOnly { get; }

	string GetOnlyGeneric { get; }

	decimal SetOnly { set; }

	string GetInit { get; set; }

	string GetSetGeneric { get; set; }

	string this[int key] { get; set; }

	void Invoke();

	void Invoke(out int result);

	void Invoke<T>();

	Task InvokeAsync();

	void InvokeClass();

	Task InvokeWhenAllAsync();

	Task InvokeRunAsync();

	void InvokeWithParameter(in string parameter);

	void InvokeWithParameter(in int parameter);

	void InvokeWithParameter(ref decimal parameter);

	void InvokeWithParameter<T>(T parameter);

	Task InvokeWithParameterAsync(int parameter);

	void InvokeWithParameterGeneric(string parameter);

	void InvokeWithParameterClass(float parameter);

	void InvokeWithSeveralParameters(int parameter1, int parameter2);

	void InvokeWithSeveralParameters(ref int parameter1, int parameter2);

	void InvokeWithSeveralParameters(int parameter1, ref int parameter2);

	void InvokeWithSeveralParameters(ref int parameter1, ref int parameter2);

	void InvokeWithSeveralParameters<T>(T parameter1, int parameter2);

	Task InvokeWithSeveralParametersAsync(int parameter1, int parameter2);

	void InvokeWithSeveralParametersGeneric<TParameter>(TParameter parameter1, string parameter2);

	decimal Return();

	bool Return(out string result);

	T Return<T>();

	ValueTask<bool> ReturnAsync();

	T ReturnGeneric<T>();

	int ReturnClass();

	string ReturnWithParameter(in string parameter);

	int ReturnWithParameter(ref double parameter);

	TReturn ReturnWithParameter<TParameter, TReturn>(TParameter parameter);

	ValueTask<int> ReturnWithParameterAsync(string parameter);

	TReturn ReturnWithParameterGeneric<TReturn>(string parameter);

	double ReturnWithSeveralParameters(int parameter1, int parameter2);

	double ReturnWithSeveralParameters(ref int parameter1, int parameter2);

	double ReturnWithSeveralParameters(int parameter1, ref int parameter2);

	double ReturnWithSeveralParameters(ref int parameter1, ref int parameter2);

	TReturn ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref TParameter1 parameter1, TParameter2 parameter2);

	ValueTask<decimal> ReturnWithSeveralParametersAsync(int parameter1, int parameter2);

	string ReturnWithSeveralParametersGeneric<TParameter1, TParameter2>(TParameter1 parameter1, TParameter2 parameter2);
}
