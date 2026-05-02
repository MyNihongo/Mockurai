namespace MyNihongo.Mockurai.Models;

internal sealed class SyntaxNodeTransformResult(ClassDeclarationSyntax? mockContainerClass) : ITransformResult
{
	public INamedTypeSymbol? GetNamedTypeSymbol(Compilation compilation) =>
		compilation.TryGetNamedTypeSymbol(mockContainerClass);
}
