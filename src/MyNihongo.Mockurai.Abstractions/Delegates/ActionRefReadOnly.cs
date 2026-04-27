// ReSharper disable once CheckNamespace
namespace System;

public delegate void ActionRefReadOnly<T>(ref readonly T obj)
#if NET10_0_OR_GREATER
	where T : allows ref struct
#endif
;
