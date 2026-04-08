namespace MyNihongo.Mockurai.Utils;

internal static class CompilationEx
{
	public static INamedTypeSymbol? TryGetNamedTypeSymbol(this Compilation @this, ClassDeclarationSyntax? classDeclarationSyntax)
	{
		if (classDeclarationSyntax is null)
			return null;

		var symbol = @this
			.GetSemanticModel(classDeclarationSyntax.SyntaxTree)
			.GetDeclaredSymbol(classDeclarationSyntax);

		return symbol as INamedTypeSymbol;
	}
}
