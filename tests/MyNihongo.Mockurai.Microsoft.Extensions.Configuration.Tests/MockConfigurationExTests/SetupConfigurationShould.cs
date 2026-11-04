namespace MyNihongo.Mockurai.MicrosoftExtensions.Configuration.Tests.MockConfigurationExTests;

public sealed class SetupConfigurationShould : MockConfigurationExTestsBase
{
	[Fact]
	public void SetupRootValue()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new
			{
				Value = "value",
			});

		var actual = CreateFixture();
		Assert.Equal("value", actual["Value"]);
	}

	[Fact]
	public void SetupRootValueSection()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new
			{
				Value = "value",
			});

		var actual = CreateFixture()
			.GetSection("Value");

		Assert.Equal("value", actual.Value);
		Assert.Equal("value", actual.Key);
		Assert.Equal("value", actual.Path);
	}
}
