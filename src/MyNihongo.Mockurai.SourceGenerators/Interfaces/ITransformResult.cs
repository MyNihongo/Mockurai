namespace MyNihongo.Mockurai;

internal interface ITransformResult
{
	INamedTypeSymbol? GetNamedTypeSymbol(Compilation compilation);
}
