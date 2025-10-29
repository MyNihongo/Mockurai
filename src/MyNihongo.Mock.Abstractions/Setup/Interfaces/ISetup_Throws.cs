namespace MyNihongo.Mock;

public interface ISetupThrows<TCallback>
{
	ISetup<TCallback> Throws(in Exception exception);
}

public interface ISetupThrowsChain<TCallback>
{
	ISetupThrowsJoin<TCallback> Throws(in Exception exception);
}

public interface ISetupThrowsJoin<TCallback> : ISetup<TCallback>
{
	ISetupCallback<TCallback> And();
}
