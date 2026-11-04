namespace MyNihongo.Mockurai;

internal static class TypeEx
{
	public static bool IsPrimitive(this Type type)
	{
		if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			return IsPrimitiveType(type.GetGenericArguments()[0]);

		return IsPrimitiveType(type);

		static bool IsPrimitiveType(Type type) =>
			type.IsPrimitive
			|| type.IsEnum
			|| type == typeof(string)
			|| type == typeof(decimal);
	}
}
