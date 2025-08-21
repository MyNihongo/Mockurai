namespace MyNihongo.Mock.Sample;

public interface IPrimitiveDependencyService
{
	event PrimitiveHandler? Handler;
	event EventHandler<string>? HandlerEvent;

	int GetOnly { get; }

	decimal SetOnly { set; }

	string GetInit { get; set; }

	void Invoke();

	void InvokeWithParameter(in string parameter);

	void InvokeWithParameter(in int parameter);

	void InvokeWithSeveralParameters(in int parameter1, in int parameter2);

	int Return();

	string ReturnWithParameter(in string parameter);

	decimal ReturnWithSeveralParameters(int parameter1, int parameter2);
}
