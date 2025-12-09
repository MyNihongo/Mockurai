namespace MyNihongo.Mock.Utils;

internal static class NamedTypeSymbolEx
{
	public static string GetString(this Accessibility @this)
	{
		return @this switch
		{
			Accessibility.Private => "private",
			Accessibility.ProtectedAndInternal => "private internal",
			Accessibility.Protected => "protected",
			Accessibility.Internal => "internal",
			Accessibility.ProtectedOrInternal => "protected internal",
			Accessibility.Public => "public",
			_ => string.Empty,
		};
	}
}
