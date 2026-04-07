namespace MyNihongo.Mockurai.Models;

internal readonly struct SyntaxNodeTransformResult(ClassDeclarationSyntax? mockContainerClass)
{
	public readonly ClassDeclarationSyntax? MockContainerClass = mockContainerClass;
}
