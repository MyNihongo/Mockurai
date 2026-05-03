using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MyNihongo.Mockurai.Utils;

namespace MyNihongo.Mockurai.Tests.Utils.ClassInheritanceTreeTests;

public abstract partial class ClassInheritanceTreeTestsBase
{
	protected static IEnumerable<string> CreateFixture(string input, CancellationToken ct = default)
	{
		var compilation = CreateCompilation(input, ct);

		var items = ClassRegex().Matches(input)
			.Where(x => x.Success)
			.Select(x => x.Value)
			.Select(TransformResult? (x) => new TransformResult(x))
			.ToImmutableArray();

		return new ClassInheritanceTree<TransformResult>(items, compilation)
			.Select(x => x.GetNamedTypeSymbol(compilation)?.Name!);
	}

	private sealed class TransformResult(string name) : ITransformResult
	{
		public INamedTypeSymbol? GetNamedTypeSymbol(Compilation compilation) =>
			compilation.GetTypeByMetadataName(name);

		public override string ToString() =>
			name;
	}

	private static CSharpCompilation CreateCompilation(string input, CancellationToken ct = default)
	{
		return CSharpCompilation.Create(
			assemblyName: "ClassInheritanceTreeTestsBase",
			syntaxTrees: [CSharpSyntaxTree.ParseText(input, cancellationToken: ct)],
			references: [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
		);
	}

	[GeneratedRegex(@"(?<=\bclass\s)Class\S*", RegexOptions.Multiline)]
	private static partial Regex ClassRegex();
}
