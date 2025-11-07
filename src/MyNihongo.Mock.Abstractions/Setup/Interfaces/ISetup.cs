namespace MyNihongo.Mock;

public interface ISetup<TCallback> : ISetupCallbackStart<TCallback>, ISetupThrowsStart<TCallback>;
