namespace MyNihongo.Mockurai.Utils;

internal static class TypeSymbolEx
{
	extension(ITypeSymbol @this)
	{
		public IEnumerable<ISymbol> GetOverridableMembers()
		{
			return @this.GetMembers()
				.Where(static x => x.IsPublic && x is { IsStatic: false, IsSealed: false } && (x.IsOverride || x.IsVirtual || x.IsAbstract));
		}

		public IEnumerable<ISymbol> GetInterfaceMembers()
		{
			foreach (var member in @this.GetMembers())
				yield return member;

			foreach (var interfaceSymbol in @this.AllInterfaces)
			{
				foreach (var member in interfaceSymbol.GetMembers())
					yield return member;
			}
		}

		/// <summary>
		/// Returns members that prevent compilation, but are not relevant to mocking (e.g. abstract members of parent classes).
		/// </summary>
		/// <returns>Enumeration of irrelevant members that must be overriden.</returns>
		public IEnumerable<ISymbol> GetIrrelevantOverridableMembers()
		{
			if (@this.TypeKind != TypeKind.Class)
				return [];

			return @this.GetMembers()
				.Where(static x => !x.IsPublic && x.IsAbstract);
		}
	}

	// TODO: when there is more time try to optimize appending instead of appending strings of ITypeSymbol, IPropertySymbol, etc
	extension(StringBuilder @this)
	{
		public StringBuilder AppendMockClassName(ITypeSymbol typeSymbol, bool appendGenericTypes = true)
		{
			var name = typeSymbol.Name;
			if (name.Length > 0 && name[0] == 'I')
				@this.Append(name.Substring(1));
			else
				@this.Append(name);

			@this.Append("Mock");

			return appendGenericTypes
				? @this.AppendGenericTypes(typeSymbol, useSeparator: false)
				: @this;
		}

		public StringBuilder AppendGenericTypes(ITypeSymbol typeSymbol, bool useSeparator = true, bool appendTypeOfName = false)
		{
			return typeSymbol is INamedTypeSymbol { TypeArguments.Length: > 0 } namedTypeSymbol
				? @this.AppendGenericTypeArguments(namedTypeSymbol.TypeArguments, useSeparator, appendTypeOfName)
				: @this;
		}

		public StringBuilder AppendGenericTypes(ImmutableArray<ITypeSymbol> typeArguments, bool useSeparator = true)
		{
			return typeArguments.Length > 0
				? @this.AppendGenericTypeArguments(typeArguments, useSeparator, appendTypeOfName: false)
				: @this;
		}

		private StringBuilder AppendGenericTypeArguments(ImmutableArray<ITypeSymbol> typeArguments, bool useSeparator, bool appendTypeOfName)
		{
			@this.Append('<');

			if (useSeparator)
			{
				for (var i = 0; i < typeArguments.Length; i++)
				{
					if (i > 0)
						@this.Append(", ");

					if (appendTypeOfName)
						@this.AppendTypeofName(typeArguments[i], typeOverride: null, appendStringInterpolation: true);
					else
						@this.AppendType(typeArguments[i]);
				}
			}
			else
			{
				foreach (var typeArgument in typeArguments)
					@this.AppendType(typeArgument);
			}

			return @this.Append('>');
		}

		public StringBuilder AppendTypeofName(ITypeSymbol typeParameterSymbol, string? typeOverride, bool appendStringInterpolation = false)
		{
			if (appendStringInterpolation)
				@this.Append('{');

			@this
				.Append("typeof(")
				.AppendType(typeParameterSymbol, typeOverride)
				.Append(").Name");

			if (appendStringInterpolation)
				@this.Append('}');

			return @this;
		}

		public StringBuilder TryAppendOverride(ISymbol symbol)
		{
			return symbol.ContainingType.TypeKind == TypeKind.Class && (symbol.IsAbstract || symbol.IsVirtual)
				? @this.Append("override ")
				: @this;
		}

		public StringBuilder AppendItSetupType(ITypeSymbol typeSymbol, bool isNullable = false, string? typeOverride = null)
		{
			@this
				.Append("ItSetup<")
				.AppendType(typeSymbol, typeOverride)
				.Append('>');

			return isNullable
				? @this.Append('?')
				: @this;
		}

		public StringBuilder AppendType(ITypeSymbol typeSymbol, string? typeOverride = null)
		{
			if (!string.IsNullOrEmpty(typeOverride))
				return @this.Append(typeOverride);

			// TODO: should not be needed after refactoring
			return typeSymbol is IArrayTypeSymbol
				? @this.Append(typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
				: @this.Append(typeSymbol);
		}

		public StringBuilder AppendTypeNamespaceAndName(ITypeSymbol typeSymbol)
		{
			var startIndex = @this.Length;
			var namespaceSymbol = typeSymbol.ContainingNamespace;

			while (namespaceSymbol is not null && !namespaceSymbol.IsGlobalNamespace)
			{
				@this.Insert(startIndex, '.');
				@this.Insert(startIndex, namespaceSymbol.Name);
				namespaceSymbol = namespaceSymbol.ContainingNamespace;
			}

			var containingType = typeSymbol.ContainingType;
			while (containingType is not null)
			{
				@this.Append(containingType.Name).Append('.');
				containingType = containingType.ContainingType;
			}

			return @this.Append(typeSymbol.Name);
		}
	}
}
