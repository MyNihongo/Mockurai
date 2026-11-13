using System.Globalization;

namespace MyNihongo.Mockurai;

internal static class ObjectEx
{
	public static string? SerializeValue(this object? @this) =>
		@this switch
		{
			null => null,
			string x => x,
			bool x => x ? "true" : "false",
			Enum x => x.ToString(),
			DateTime x => x.ToString("O", CultureInfo.InvariantCulture),
			DateTimeOffset x => x.ToString("O", CultureInfo.InvariantCulture),
			DateOnly x => x.ToString("O", CultureInfo.InvariantCulture),
			TimeOnly x => x.ToString("O", CultureInfo.InvariantCulture),
			Uri x => x.OriginalString,
			IFormattable x => x.ToString(null, CultureInfo.InvariantCulture),
			_ => @this.ToString(),
		};
}
