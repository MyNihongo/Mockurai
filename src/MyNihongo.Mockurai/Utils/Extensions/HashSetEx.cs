namespace MyNihongo.Mockurai.Utils;

internal static class HashSetEx
{
	public static void AddAll(this HashSet<ITypeSymbol> @this, List<MockClassDeclaration> mocks)
	{
		// OriginalDefinition has the generic type (e.g. T), not the implementation type (e.g. int, string, etc.)
		foreach (var mock in mocks)
			@this.Add(mock.Type.OriginalDefinition);
	}

	public static void TryAddAll<T>(this HashSet<T> @this, IReadOnlyList<T>? items)
	{
		if (items is null || items.Count == 0)
			return;

		foreach (var item in items)
			@this.Add(item);
	}
}
