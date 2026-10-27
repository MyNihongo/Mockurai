namespace MyNihongo.Mockurai;

/// <summary>
/// Provides extension methods for configuring mock setups.
/// </summary>
public static class SetupEx
{
    /// <summary>
    /// Configures a mock setup returning an asynchronous enumeration to return elements from the specified enumerable collection.
    /// </summary>
    /// <typeparam name="TCallback">The type of the callback.</typeparam>
    /// <typeparam name="TReturns">The type of the items yielded by the asynchronous enumeration.</typeparam>
    /// <typeparam name="TReturnsCallback">The type of the returns callback.</typeparam>
    /// <param name="this">The setup object to configure.</param>
    /// <param name="returns">The enumerable collection of items to return asynchronously.</param>
    /// <returns>
    /// An object that can be used to further configure the mock setup.
    /// </returns>
    public static ISetupReturnsThrowsJoin<TCallback, IAsyncEnumerable<TReturns>, TReturnsCallback> Returns<TCallback, TReturns, TReturnsCallback>(
		this ISetupReturnsThrowsStart<TCallback, IAsyncEnumerable<TReturns>, TReturnsCallback> @this,
		IEnumerable<TReturns> returns)
	{
		return @this
			.Returns(returns.ToAsyncEnumerable());
	}
}
