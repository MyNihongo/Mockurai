// ReSharper disable once CheckNamespace
namespace System;

public delegate TResult FuncRefReadOnly<T, out TResult>(ref readonly T arg)
#if NET10_0_OR_GREATER
	where T : allows ref struct
	where TResult : allows ref struct
#endif
;
