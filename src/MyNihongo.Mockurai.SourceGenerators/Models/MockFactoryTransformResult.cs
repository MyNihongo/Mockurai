namespace MyNihongo.Mockurai.Models;

internal sealed class MockFactoryTransformResult(InvocationExpressionSyntax invocation)
{
	public ITypeSymbol? GetMockedTypeSymbol(Compilation compilation)
	{
		var symbol = compilation
			.GetSemanticModel(invocation.SyntaxTree)
			.GetSymbolInfo(invocation)
			.Symbol;

		if (symbol is not IMethodSymbol { Name: MockGeneratorConst.FactoryMethodName, TypeArguments.Length: 1 } methodSymbol)
			return null;

		if (methodSymbol.ContainingType is not { Name: MockGeneratorConst.FactoryClassName } containingType || containingType.ContainingNamespace.ToDisplayString() != MockGeneratorConst.Namespace)
			return null;

		var typeArgument = methodSymbol.TypeArguments[0];
		return typeArgument is not ITypeParameterSymbol
			? typeArgument
			: null;
	}
}
