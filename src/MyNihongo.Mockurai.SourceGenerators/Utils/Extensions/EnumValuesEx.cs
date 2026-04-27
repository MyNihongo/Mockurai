namespace MyNihongo.Mockurai.Utils;

internal static class EnumValuesEx
{
	extension(Accessibility @this)
	{
		public bool IsInternal => @this is Accessibility.Internal or Accessibility.ProtectedAndInternal or Accessibility.ProtectedOrInternal;

		public string GetString()
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

	extension(RefKind @this)
	{
		public string GetString(bool pascalCase = false)
		{
			return @this switch
			{
				RefKind.Ref => "ref",
				RefKind.Out => "out",
				RefKind.In => "in",
				RefKind.RefReadOnlyParameter => !pascalCase ? "ref readonly" : "refReadOnly",
				_ => string.Empty,
			};
		}

		public string GetModifierString()
		{
			return @this switch
			{
				RefKind.Ref => "ref",
				RefKind.RefReadOnlyParameter => "in",
				RefKind.Out => "out",
				RefKind.In => "in",
				_ => string.Empty,
			};
		}
	}
}
