namespace MyNihongo.Mock.Utils;

internal static class EnumValuesEx
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

	public static string GetString(this RefKind @this)
	{
		return @this switch
		{
			RefKind.Ref => "ref",
			RefKind.Out => "out",
			RefKind.In => "in",
			RefKind.RefReadOnlyParameter => "refReadOnly",
			_ => string.Empty,
		};
	}
}
