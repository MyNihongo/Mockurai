namespace MyNihongo.Mockurai.Utils;

internal static class ArrayTypeSymbolEx
{
	extension(StringBuilder @this)
	{
		public void AppendArrayBrackets(IArrayTypeSymbol arrayTypeSymbol)
		{
			@this.Append('[');

			for (var i = 1; i < arrayTypeSymbol.Rank; i++)
				@this.Append(',');

			@this.Append(']');
		}
	}
}
