namespace MyNihongo.Mock;

#if NET8_0
public delegate TResult FuncRefReadOnly<T, out TResult>(ref readonly T arg);
#else
public delegate TResult FuncRefReadOnly<T, out TResult>(ref readonly T arg)
	where T : allows ref struct
	where TResult : allows ref struct;
#endif
