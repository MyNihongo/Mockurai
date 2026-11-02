using Microsoft.CodeAnalysis.CSharp;

namespace MyNihongo.Mockurai.Models;

internal sealed class MockFactoryInvocation(ITypeSymbol type, InterceptableLocation location)
{
	public readonly ITypeSymbol Type = type;
	public readonly InterceptableLocation Location = location;
}
