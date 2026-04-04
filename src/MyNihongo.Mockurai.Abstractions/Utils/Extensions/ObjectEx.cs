using System.Text.Json;

namespace MyNihongo.Mockurai;

internal static class ObjectEx
{
	public static string ToJsonString<T>(this T @this)
	{
		try
		{
			return JsonSerializer.Serialize(@this);
		}
		catch
		{
			return @this?.ToString() ?? string.Empty;
		}
	}
}
