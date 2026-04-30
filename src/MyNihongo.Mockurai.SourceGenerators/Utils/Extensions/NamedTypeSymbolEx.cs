namespace MyNihongo.Mockurai.Utils;

internal static class NamedTypeSymbolEx
{
	extension(INamedTypeSymbol @this)
	{
		public IMethodSymbol? TryGetBaseClassMethod(string methodName, bool canOverride)
		{
			ITypeSymbol baseType = @this;
			while (baseType.TryGetBaseType(out baseType))
			{
				// In case the method has been added manually
				var query = baseType.GetMembers(methodName)
					.OfType<IMethodSymbol>()
					.Where(static x => x.DeclaredAccessibility is Accessibility.Public or Accessibility.Protected);

				if (canOverride)
					query = query.Where(static x => x.CanOverride);

				var method = query.FirstOrDefault();
				if (method is not null)
					return method;
			}

			return null;
		}

		public ITypeSymbol? TryGetBaseClassWithNestedClassName(string className)
		{
			ITypeSymbol baseType = @this;
			while (baseType.TryGetBaseType(out baseType))
			{
				// In case the method has been added manually
				var hasMethod = baseType.GetTypeMembers(className)
					.Where(static x => x.TypeKind == TypeKind.Class)
					.Where(static x => x.DeclaredAccessibility is Accessibility.Public or Accessibility.Protected)
					.Any();

				if (hasMethod)
					return baseType;
			}

			return null;
		}
	}
}
