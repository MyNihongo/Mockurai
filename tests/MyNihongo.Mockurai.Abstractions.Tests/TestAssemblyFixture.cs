using System.Globalization;

namespace MyNihongo.Mockurai.Abstractions.Tests;

public sealed class TestAssemblyFixture : IAsyncLifetime
{
	public ValueTask InitializeAsync()
	{
		const string dateFormat = "MM/dd/yyyy", timeFormat = "HH:mm:ss";

		var culture = new CultureInfo("en-US")
		{
			DateTimeFormat =
			{
				ShortDatePattern = dateFormat,
				ShortTimePattern = timeFormat,
				LongTimePattern = timeFormat,
			},
		};

		CultureInfo.DefaultThreadCurrentCulture = culture;
		CultureInfo.DefaultThreadCurrentUICulture = culture;

		return ValueTask.CompletedTask;
	}

	public ValueTask DisposeAsync()
	{
		return ValueTask.CompletedTask;
	}
}
