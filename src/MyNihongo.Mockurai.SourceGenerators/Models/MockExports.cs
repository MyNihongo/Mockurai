namespace MyNihongo.Mockurai.Models;

internal sealed class MockExports
{
	public readonly HashSet<ITypeSymbol> Own = new(TypeSymbolNameComparer.Default);
	public readonly Dictionary<ITypeSymbol, INamedTypeSymbol> Referenced = new(TypeSymbolNameComparer.Default);

	public bool IsLibrary => Own.Count > 0;

	public bool IsOwn(ITypeSymbol typeSymbol) =>
		Own.Contains(typeSymbol);

	public INamedTypeSymbol? TryGetReferenced(ITypeSymbol typeSymbol) =>
		Referenced.TryGetValue(typeSymbol, out var mockType) ? mockType : null;

	public static MockExports Collect(Compilation compilation)
	{
		var exports = new MockExports();

		foreach (var attribute in compilation.Assembly.GetAttributes())
		{
			if (!attribute.IsMockuraiAttribute(MockGeneratorConst.ExportAttribute) || attribute.ConstructorArguments.Length != 1)
				continue;

			if (attribute.ConstructorArguments[0].Value is INamedTypeSymbol type && type.IsExportable())
				exports.Own.Add(type);
		}

		foreach (var assembly in compilation.SourceModule.ReferencedAssemblySymbols)
		{
			if (!assembly.ReferencesAbstractions())
				continue;

			foreach (var attribute in assembly.GetAttributes())
			{
				if (!attribute.IsMockuraiAttribute(MockGeneratorConst.ExportedAttribute) || attribute.ConstructorArguments.Length != 2)
					continue;

				if (attribute.ConstructorArguments[0].Value is not INamedTypeSymbol type || attribute.ConstructorArguments[1].Value is not INamedTypeSymbol mockType)
					continue;

				// The current assembly generates its own export; otherwise, the first exported mock wins
				if (exports.Own.Contains(type) || exports.Referenced.ContainsKey(type))
					continue;

				exports.Referenced.Add(type, mockType);
			}
		}

		return exports;
	}
}

file static class MockExportsEx
{
	private const string AbstractionsAssemblyName = "MyNihongo.Mockurai.Abstractions";

	public static bool IsMockuraiAttribute(this AttributeData @this, string name) =>
		@this.AttributeClass is { } attributeClass
		&& attributeClass.Name == name
		&& attributeClass.ContainingNamespace.ToDisplayString() == MockGeneratorConst.Namespace;

	// Only non-generic public types can be exported for now
	public static bool IsExportable(this INamedTypeSymbol @this) =>
		@this is { TypeArguments.Length: 0, DeclaredAccessibility: Accessibility.Public }
		&& @this.TypeKind is TypeKind.Interface or TypeKind.Class;

	public static bool ReferencesAbstractions(this IAssemblySymbol @this)
	{
		if (@this.Name == AbstractionsAssemblyName)
			return false;

		foreach (var module in @this.Modules)
		foreach (var referencedAssembly in module.ReferencedAssemblies)
			if (referencedAssembly.Name == AbstractionsAssemblyName)
				return true;

		return false;
	}
}
