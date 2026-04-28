// ReSharper disable once CheckNamespace

namespace System;

/// <summary>
/// Encapsulates a method that takes a single <see langword="in"/> parameter and does not return a value.
/// </summary>
/// <typeparam name="T">The type of the parameter.</typeparam>
/// <param name="obj">The argument passed by readonly reference.</param>
public delegate void ActionIn<T>(in T obj)
#if NET10_0_OR_GREATER
	where T : allows ref struct
#endif
;
