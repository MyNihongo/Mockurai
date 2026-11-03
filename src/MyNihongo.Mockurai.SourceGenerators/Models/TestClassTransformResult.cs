namespace MyNihongo.Mockurai.Models;

internal sealed class TestClassTransformResult(ClassDeclarationSyntax? mockContainerClass) : ITransformResult
{
	public INamedTypeSymbol? GetNamedTypeSymbol(Compilation compilation) =>
		compilation.TryGetNamedTypeSymbol(mockContainerClass);
}
