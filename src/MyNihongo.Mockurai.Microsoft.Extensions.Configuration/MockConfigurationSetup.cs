namespace MyNihongo.Mockurai;

internal sealed class MockConfigurationSetup : IConfigurationSetup
{
	private readonly IMock<IConfiguration> _mock;

	public MockConfigurationSetup(IMock<IConfiguration> mock)
	{
		_mock = mock;
	}

	public void Returns(object? configurationObject)
	{
		var root = ConfigurationTreeBuilder.Build(configurationObject);

		_mock
			.SetupGetSection()
			.Returns(key => root.GetSection(key));

		_mock
			.SetupGetChildren()
			.Returns(() => root.GetChildren());

		_mock
			.SetupGetIndexer()
			.Returns(key => root[key]);

		_mock
			.SetupSetIndexer()
			.Callback((key, value) => root[key] = value);

		_mock
			.SetupGetReloadToken()
			.Returns(NullChangeToken.Instance);
	}
}
