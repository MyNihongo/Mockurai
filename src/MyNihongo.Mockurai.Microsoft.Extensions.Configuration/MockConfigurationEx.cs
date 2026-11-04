namespace MyNihongo.Mockurai;

public static class MockConfigurationEx
{
	public static IConfigurationSetup SetupConfiguration<T>(this IMock<T> @this)
		where T : class, IConfiguration
	{
		return new MockConfigurationSetup<T>(@this);
	}
}
