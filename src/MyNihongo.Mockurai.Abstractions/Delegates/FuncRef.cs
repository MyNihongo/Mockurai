// ReSharper disable once CheckNamespace
namespace System;

#if NET8_0
public delegate TResult FuncRef<T, out TResult>(ref T arg);
#else
public delegate TResult FuncRef<T, out TResult>(ref T arg)
	where T : allows ref struct
	where TResult : allows ref struct;
#endif
