// ReSharper disable once CheckNamespace
namespace System;

/// <summary>
/// Encapsulates a method that takes a single <see langword="ref"/> parameter and returns a value of type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="T">The type of the parameter.</typeparam>
/// <typeparam name="TResult">The type of the return value.</typeparam>
/// <param name="arg">The argument passed by reference, which the method may read from and write to.</param>
/// <returns>The value produced by the method.</returns>
public delegate TResult FuncRef<T, out TResult>(ref T arg)
#if NET10_0_OR_GREATER
	where T : allows ref struct
	where TResult : allows ref struct
#endif
;
