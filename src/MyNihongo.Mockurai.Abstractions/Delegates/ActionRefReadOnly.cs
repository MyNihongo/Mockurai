// ReSharper disable once CheckNamespace
namespace System;

#if NET8_0
public delegate void ActionRefReadOnly<T>(ref readonly T obj);
#else
public delegate void ActionRefReadOnly<T>(ref readonly T obj)
	where T : allows ref struct;
#endif
