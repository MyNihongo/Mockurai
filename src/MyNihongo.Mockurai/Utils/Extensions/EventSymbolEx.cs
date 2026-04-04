namespace MyNihongo.Mockurai.Utils;

internal static class EventSymbolEx
{
	extension(IEventSymbol @this)
	{
		public IParameterSymbol? GetDelegateParameter()
		{
			return @this.Type is INamedTypeSymbol { TypeKind: TypeKind.Delegate, DelegateInvokeMethod.Parameters.Length: > 1 } namedTypeSymbol
				? namedTypeSymbol.DelegateInvokeMethod.Parameters[1]
				: null;
		}
	}
}
