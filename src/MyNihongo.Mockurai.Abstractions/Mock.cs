namespace MyNihongo.Mockurai;

/// <summary>
/// Provides methods for creating mock instances of objects for testing purposes.
/// </summary>
public sealed class Mock
{
	/// <summary>
	/// Creates and returns a mock wrapper of type <typeparamref name="T"/> for testing purposes.
	/// </summary>
	/// <typeparam name="T">The type of the object to mock.</typeparam>
	/// <returns>An instance of <see cref="IMock{T}"/> representing the mock.</returns>
	public static IMock<T> Create<T>()
	{
		throw new NotImplementedException("Must be implemented with interceptors");
	}
}
