namespace MyNihongo.Mock;

public interface ISetup
{
	void Throws(in Exception exception);
}

public interface ISetup<in T> : ISetup
{
	void Returns(T? returns);
}
