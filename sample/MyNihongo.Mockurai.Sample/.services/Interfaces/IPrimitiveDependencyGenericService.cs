namespace MyNihongo.Mockurai.Sample;

public interface IPrimitiveDependencyService<T>
{
	T GetOnly { get; }

	T GetSet { get; set; }

	void InvokeWithParameter(T parameter);

	void InvokeWithSeveralParameters<TParameter>(TParameter param1, T param2);

	TReturn Return<TReturn>();

	TReturn ReturnWithParameter<TReturn>(T parameter);

	T ReturnWithSeveralParameters<TParameter1, TParameter2>(TParameter1 param1, TParameter2 param2);
}
