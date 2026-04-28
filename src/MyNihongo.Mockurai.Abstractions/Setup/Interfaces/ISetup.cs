namespace MyNihongo.Mockurai;

/// <summary>
/// The starting point of a void mock setup, allowing either a callback or an exception to be configured.
/// </summary>
/// <typeparam name="TCallback">The callback delegate type invoked when the mocked member is called.</typeparam>
public interface ISetup<TCallback> : ISetupCallbackStart<TCallback>, ISetupThrowsStart<TCallback>;
