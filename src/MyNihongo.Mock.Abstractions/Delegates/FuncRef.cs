namespace MyNihongo.Mock;

public delegate TResult FuncRef<T, out TResult>(ref T arg)
	where T : allows ref struct
	where TResult : allows ref struct;
