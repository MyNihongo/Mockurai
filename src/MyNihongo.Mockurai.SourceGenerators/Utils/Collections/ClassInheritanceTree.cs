using System.Collections;

namespace MyNihongo.Mockurai.Utils;

internal sealed class ClassInheritanceTree<T> : IEnumerable<ITransformResult>
	where T : ITransformResult
{
	private readonly Node[] _rootNodes;

	public ClassInheritanceTree(ImmutableArray<T?> transformResults, Compilation compilation)
	{
		_rootNodes = CreateRootNodeArray(transformResults, compilation);
	}

	public IEnumerator<ITransformResult> GetEnumerator()
	{
		var queue = new Queue<Node>();

		foreach (var node in _rootNodes)
			queue.Enqueue(node);

		while (queue.Count > 0)
		{
			var node = queue.Dequeue();
			yield return node;

			foreach (var childNode in node.Children)
				queue.Enqueue(childNode);
		}
	}

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();

	private static Node[] CreateRootNodeArray(ImmutableArray<T?> transformResults, Compilation compilation)
	{
		List<Node> rootNodes = [], nonRootNodes = [];
		var dictionary = new Dictionary<INamedTypeSymbol, Node>(SymbolEqualityComparer.Default);

		foreach (var transformResult in transformResults)
		{
			var namedTypeSymbol = transformResult?.GetNamedTypeSymbol(compilation);
			if (namedTypeSymbol is null)
				continue;

			var node = new Node(transformResult!, namedTypeSymbol);
			dictionary[namedTypeSymbol] = node;

			if (namedTypeSymbol.BaseType is null || namedTypeSymbol.BaseType.SpecialType == SpecialType.System_Object)
				rootNodes.Add(node);
			else if (dictionary.TryGetValue(namedTypeSymbol.BaseType, out var parentNode))
				parentNode.Children.Add(node);
			else
				nonRootNodes.Add(node);
		}

		foreach (var node in nonRootNodes)
		{
			if (!dictionary.TryGetValue(node.MockClass.BaseType!, out var parentNode))
				rootNodes.Add(node);
			else
				parentNode.Children.Add(node);
		}

		return rootNodes.ToArray();
	}

	private sealed class Node(T transformResult, INamedTypeSymbol mockClass) : ITransformResult
	{
		private readonly T _transformResult = transformResult;
		public readonly List<Node> Children = [];

		public INamedTypeSymbol MockClass { get; private set; } = mockClass;

		public INamedTypeSymbol? GetNamedTypeSymbol(Compilation compilation)
		{
			var mockClass = _transformResult.GetNamedTypeSymbol(compilation);
			if (mockClass is not null)
				MockClass = mockClass;

			return mockClass;
		}

		public override string ToString() =>
			MockClass.ToString();
	}
}
