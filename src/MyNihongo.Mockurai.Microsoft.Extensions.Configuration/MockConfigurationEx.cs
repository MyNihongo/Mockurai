namespace MyNihongo.Mockurai;

/// <summary>
/// Extension methods for setting up mocks of <see cref="IConfiguration"/>.
/// </summary>
public static class MockConfigurationEx
{
	/// <summary>
	/// Begins setting up the mocked <see cref="IConfiguration"/> so that
	/// <see cref="IConfiguration.GetSection"/>, <see cref="IConfiguration.GetChildren"/>, the indexer,
	/// as well as binder extensions such as <c>GetValue&lt;T&gt;</c>, <c>Get&lt;T&gt;</c> and <c>Bind</c> work against the provided object.
	/// </summary>
	/// <param name="this">The mock created with <c>Mock.Create&lt;IConfiguration&gt;()</c>.</param>
	/// <returns>A setup which accepts the configuration object.</returns>
	/// <exception cref="NotSupportedException">Thrown when the mock has not been created for <see cref="IConfiguration"/>.</exception>
	public static IConfigurationSetup SetupConfiguration(this IMock<IConfiguration> @this)
	{
		return new MockConfigurationSetup(@this);
	}
}
