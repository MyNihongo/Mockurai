namespace MyNihongo.Mockurai;

/// <summary>
/// Configures the values returned by a mocked <see cref="IConfiguration"/>.
/// </summary>
public interface IConfigurationSetup
{
	/// <summary>
	/// Sets up the mocked <see cref="IConfiguration"/> from an object graph.
	/// Property names (or dictionary keys) become configuration keys, nested objects become sections, and collection items are keyed by their index (<c>0</c>, <c>1</c>, ...).
	/// Calling this method again replaces the previous setup.
	/// </summary>
	/// <param name="configurationObject">
	/// An object (e.g. an anonymous object, a POCO, a dictionary or an <see cref="System.Dynamic.ExpandoObject"/>) describing the configuration tree.
	/// <see langword="null"/> sets up an empty configuration.
	/// </param>
	/// <exception cref="InvalidOperationException">Thrown when <paramref name="configurationObject"/> is a scalar value or a non-dictionary collection.</exception>
	void Returns(object? configurationObject);
}
