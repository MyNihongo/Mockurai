using System.Globalization;

namespace MyNihongo.Mockurai.Tests;

public sealed class GlobalTestSetup
{
	public GlobalTestSetup()
	{
		var culture = (CultureInfo)new CultureInfo("ja-JP").Clone();
		culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";

		CultureInfo.DefaultThreadCurrentCulture = culture;
		CultureInfo.DefaultThreadCurrentUICulture = culture;

		CultureInfo.CurrentCulture = culture;
		CultureInfo.CurrentUICulture = culture;
	}
}
