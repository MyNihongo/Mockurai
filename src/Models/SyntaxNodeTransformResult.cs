namespace MyNihongo.Mock.Models;

internal readonly struct SyntaxNodeTransformResult(FieldDeclarationSyntax? fieldDeclarationSyntax)
{
	public readonly FieldDeclarationSyntax? FieldDeclarationSyntax = fieldDeclarationSyntax;
}
