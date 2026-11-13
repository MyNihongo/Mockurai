namespace MyNihongo.Mockurai;

internal static class TypeEx
{
	public static bool IsScalar(this Type type)
	{
		type = Nullable.GetUnderlyingType(type) ?? type;

		return type.IsPrimitive
			|| type.IsEnum
			|| type == typeof(string)
			|| type == typeof(decimal)
			|| type == typeof(DateTime)
			|| type == typeof(DateTimeOffset)
			|| type == typeof(DateOnly)
			|| type == typeof(TimeOnly)
			|| type == typeof(TimeSpan)
			|| type == typeof(Guid)
			|| type == typeof(Uri)
			|| type == typeof(Half)
			|| type == typeof(Int128)
			|| type == typeof(UInt128);
	}
}
