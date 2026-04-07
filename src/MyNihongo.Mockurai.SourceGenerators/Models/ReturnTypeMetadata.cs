namespace MyNihongo.Mockurai.Models;

internal readonly ref struct ReturnTypeMetadata(ITypeSymbol? returnType, string? staticInitializer)
{
	public readonly ITypeSymbol? ReturnType = returnType;
	public readonly string? StaticInitializer = staticInitializer;
}
