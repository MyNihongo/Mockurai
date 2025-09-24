namespace MyNihongo.Mock;

public delegate TResult FuncOut<T, out TResult>(out T arg)
	where T : allows ref struct
	where TResult : allows ref struct;
