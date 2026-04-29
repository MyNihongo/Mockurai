using System.Text.Encodings.Web;
using System.Text.Json;

namespace MyNihongo.Mockurai;

/// <summary>
/// Extension methods for rendering arbitrary values as strings used in failure messages and snapshot capture.
/// </summary>
public static class ObjectEx
{
	private static readonly JsonSerializerOptions Options = new()
	{
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
	};

	/// <summary>
	/// Serializes the value to JSON, falling back to <see cref="object.ToString"/> when serialization fails.
	/// </summary>
	/// <typeparam name="T">The value type.</typeparam>
	/// <param name="this">The value to render.</param>
	/// <returns>The JSON representation, or the string fallback when serialization throws.</returns>
	public static string SerializeToJson<T>(this T @this)
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
