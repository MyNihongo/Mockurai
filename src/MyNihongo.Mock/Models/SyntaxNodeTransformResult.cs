namespace MyNihongo.Mock.Models;

internal readonly struct SyntaxNodeTransformResult(ClassDeclarationSyntax? mockContainerClass)
{
	public readonly ClassDeclarationSyntax? MockContainerClass = mockContainerClass;
}
