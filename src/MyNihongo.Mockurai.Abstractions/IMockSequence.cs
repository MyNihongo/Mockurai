namespace MyNihongo.Mockurai;

/// <summary>
/// Pairs a mock with the <see cref="VerifyIndex"/> cursor used to verify invocations in sequence.
/// </summary>
/// <typeparam name="T">The type being mocked.</typeparam>
public interface IMockSequence<out T>
{
	/// <summary>
	/// Gets the shared cursor advanced by each sequential verification call.
	/// </summary>
	VerifyIndex VerifyIndex { get; }

	/// <summary>
	/// Gets the underlying mock whose invocations are verified in sequence.
	/// </summary>
	IMock<T> Mock { get; }
}

/// <summary>
/// Default implementation of <see cref="IMockSequence{T}"/>.
/// </summary>
/// <typeparam name="T">The type being mocked.</typeparam>
public sealed class MockSequence<T> : IMockSequence<T>
{
	/// <inheritdoc/>
	public required VerifyIndex VerifyIndex { get; init; }

	/// <inheritdoc/>
	public required IMock<T> Mock { get; init; }
}
