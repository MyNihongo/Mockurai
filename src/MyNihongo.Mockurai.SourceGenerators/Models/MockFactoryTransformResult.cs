using Microsoft.CodeAnalysis.CSharp;

namespace MyNihongo.Mockurai.Models;

internal sealed class MockFactoryTransformResult(InvocationExpressionSyntax invocation)
{
	public MockFactoryInvocation? GetMockFactoryInvocation(Compilation compilation)
	{
		var semanticModel = compilation.GetSemanticModel(invocation.SyntaxTree);
		var symbol = semanticModel
			.GetSymbolInfo(invocation)
			.Symbol;

		if (symbol is not IMethodSymbol { Name: MockGeneratorConst.FactoryMethodName, TypeArguments.Length: 1 } methodSymbol)
			return null;

		if (methodSymbol.ContainingType is not { Name: MockGeneratorConst.FactoryClassName } containingType || containingType.ContainingNamespace.ToDisplayString() != MockGeneratorConst.Namespace)
			return null;

		var typeArgument = methodSymbol.TypeArguments[0];
		if (typeArgument is ITypeParameterSymbol)
			return null;

		var location = semanticModel.GetInterceptableLocation(invocation);
		return location is not null
			? new MockFactoryInvocation(typeArgument, location)
			: null;
	}
}
