// ReSharper disable once CheckNamespace
namespace System;

/// <summary>
/// Encapsulates a method that takes a single <see langword="out"/> parameter and does not return a value.
/// </summary>
/// <typeparam name="T">The type of the parameter.</typeparam>
/// <param name="obj">The argument assigned by the method before it returns.</param>
public delegate void ActionOut<T>(out T obj)
#if NET10_0_OR_GREATER
	where T : allows ref struct
#endif
;
