// ReSharper disable once CheckNamespace
namespace System;

#if NET8_0
public delegate TResult FuncIn<T, out TResult>(in T arg);
#else
public delegate TResult FuncIn<T, out TResult>(in T arg)
	where T : allows ref struct
	where TResult : allows ref struct;
#endif
