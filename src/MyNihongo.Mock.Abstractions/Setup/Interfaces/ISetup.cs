namespace MyNihongo.Mock;

public interface ISetup<TCallback> : ISetupCallbackChain<TCallback>, ISetupThrowsChain<TCallback>;
