// ReSharper disable once CheckNamespace

namespace System;

/// <summary>
/// Encapsulates a method that takes a single <see langword="in"/> parameter and returns a value of type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="T">The type of the parameter.</typeparam>
/// <typeparam name="TResult">The type of the return value.</typeparam>
/// <param name="arg">The argument passed by readonly reference.</param>
/// <returns>The value produced by the method.</returns>
public delegate TResult FuncIn<T, out TResult>(in T arg)
#if NET10_0_OR_GREATER
	where T : allows ref struct
	where TResult : allows ref struct
#endif
;
