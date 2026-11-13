namespace MyNihongo.Mockurai.MicrosoftExtensions.Configuration.Tests.MockConfigurationExTests;

[MockuraiGenerate]
public abstract partial class MockConfigurationExTestsBase
{
	protected readonly IMock<IConfiguration> ConfigurationMock = Mock.Create<IConfiguration>();

	protected IConfiguration CreateFixture()
	{
		return ConfigurationMock.Object;
	}
}
