// ReSharper disable once CheckNamespace
namespace System;

#if NET8_0
public delegate TResult FuncOut<T, out TResult>(out T arg);
#else
public delegate TResult FuncOut<T, out TResult>(out T arg)
	where T : allows ref struct
	where TResult : allows ref struct;
#endif
