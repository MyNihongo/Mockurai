namespace MyNihongo.Mockurai;

public static class SetupEx
{
	public static ISetupReturnsThrowsJoin<TCallback, IAsyncEnumerable<TReturns>, TReturnsCallback> Returns<TCallback, TReturns, TReturnsCallback>(
		this ISetupReturnsThrowsStart<TCallback, IAsyncEnumerable<TReturns>, TReturnsCallback> @this,
		IEnumerable<TReturns> returns)
	{
		return @this
			.Returns(returns.ToAsyncEnumerable());
	}
}
