using System.Text.Encodings.Web;
using System.Text.Json;

namespace MyNihongo.Mockurai;

public static class ObjectEx
{
	private static readonly JsonSerializerOptions Options = new()
	{
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
	};

	public static string ToJsonString<T>(this T @this)
	{
		try
		{
			return JsonSerializer.Serialize(@this, Options);
		}
		catch
		{
			return @this?.ToString() ?? string.Empty;
		}
	}
}
