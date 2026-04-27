// ReSharper disable once CheckNamespace
namespace System;

public delegate void ActionOut<T>(out T obj)
#if NET10_0_OR_GREATER
	where T : allows ref struct
#endif
;
