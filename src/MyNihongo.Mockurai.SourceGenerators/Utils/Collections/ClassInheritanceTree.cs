using System.Collections;

namespace MyNihongo.Mockurai.Utils;

internal sealed class ClassInheritanceTree<T> : IEnumerable<INamedTypeSymbol>
	where T : ITransformResult
{
	private readonly Node[] _nodes;

	public ClassInheritanceTree(ImmutableArray<T?> transformResults, Compilation compilation)
	{
		_nodes = CreateRootNodeArray(transformResults, compilation);
	}

	public IEnumerator<INamedTypeSymbol> GetEnumerator()
	{
		var queue = new Queue<Node>();

		foreach (var node in _nodes)
			queue.Enqueue(node);

		while (queue.Count > 0)
		{
			var node = queue.Dequeue();
			yield return node.MockClass;

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

			var node = new Node(namedTypeSymbol);
			dictionary.Add(namedTypeSymbol, node);

			if (namedTypeSymbol.BaseType is null || namedTypeSymbol.BaseType.SpecialType == SpecialType.System_Object)
				rootNodes.Add(node);
			else if (dictionary.TryGetValue(namedTypeSymbol.BaseType, out var parentNode))
				parentNode.Children.Add(node);
			else
				nonRootNodes.Add(node);
		}

		foreach (var node in nonRootNodes)
		{
			if (node.MockClass.BaseType is null)
				continue;

			if (!dictionary.TryGetValue(node.MockClass.BaseType, out var parentNode))
				rootNodes.Add(node);
			else
				parentNode.Children.Add(node);
		}

		return rootNodes.ToArray();
	}

	private sealed class Node(INamedTypeSymbol mockClass)
	{
		public readonly INamedTypeSymbol MockClass = mockClass;
		public readonly List<Node> Children = [];

		public override string ToString()
		{
			return MockClass.ToString();
		}
	}
}
