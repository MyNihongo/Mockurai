namespace MyNihongo.Mockurai;

public interface ISetupThrowsStart<TCallback>
{
	ISetupThrowsJoin<TCallback> Throws(in Exception exception);
}

public interface ISetupThrowsJoin<TCallback> : ISetup<TCallback>
{
	ISetupCallbackReset<TCallback> And();
}

public interface ISetupThrowsReset<TCallback>
{
	ISetup<TCallback> Throws(in Exception exception);
}
