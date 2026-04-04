// ReSharper disable once CheckNamespace
namespace System;

#if NET8_0
public delegate void ActionIn<T>(in T obj);
#else
public delegate void ActionIn<T>(in T obj)
	where T : allows ref struct;
#endif
