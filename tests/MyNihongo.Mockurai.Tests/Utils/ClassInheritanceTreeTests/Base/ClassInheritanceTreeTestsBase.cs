using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MyNihongo.Mockurai.Utils;

namespace MyNihongo.Mockurai.Tests.Utils.ClassInheritanceTreeTests;

public abstract class ClassInheritanceTreeTestsBase
{
	protected IEnumerable<INamedTypeSymbol> CreateFixture(ImmutableArray<TransformResult?> items)
	{
		return new ClassInheritanceTree<TransformResult>(items, null!)
			.Select(x => x.GetNamedTypeSymbol(null!)!);
	}

	protected sealed class TransformResult(INamedTypeSymbol namedTypeSymbol) : ITransformResult
	{
		public INamedTypeSymbol GetNamedTypeSymbol(Compilation compilation) =>
			namedTypeSymbol;
	}
}
