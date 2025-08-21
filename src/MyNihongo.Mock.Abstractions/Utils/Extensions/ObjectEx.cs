using System.Text.Json;

namespace MyNihongo.Mock;

internal static class ObjectEx
{
	public static string ToJsonString<T>(this T @this)
	{
		try
		{
			return JsonSerializer.Serialize(@this);
		}
		catch (Exception e)
		{
			return e.ToString();
		}
	}
}
