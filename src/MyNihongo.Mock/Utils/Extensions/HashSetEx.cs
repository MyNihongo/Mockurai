namespace MyNihongo.Mock.Utils;

internal static class HashSetEx
{
	public static void AddAll(this HashSet<ITypeSymbol> @this, List<MockClassDeclaration> mocks)
	{
		// OriginalDefinition has the generic type (e.g. T), not the implementation type (e.g. int, string, etc.)
		foreach (var mock in mocks)
			@this.Add(mock.Type.OriginalDefinition);
	}
}
