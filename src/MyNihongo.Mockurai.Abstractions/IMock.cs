namespace MyNihongo.Mockurai;

/// <summary>
/// Represents a generated mock that exposes the underlying instance of <typeparamref name="T"/> for use under test.
/// </summary>
/// <typeparam name="T">The type being mocked.</typeparam>
public interface IMock<out T>
{
	/// <summary>
	/// Gets the mocked instance that can be passed to the system under test.
	/// </summary>
	T Object { get; }
}
