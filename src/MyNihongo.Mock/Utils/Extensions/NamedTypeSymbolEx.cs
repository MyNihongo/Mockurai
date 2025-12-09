namespace MyNihongo.Mock.Utils;

internal static class NamedTypeSymbolEx
{
	public static string GetAccessibilityString(this INamedTypeSymbol @this)
	{
		return @this.DeclaredAccessibility switch
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
