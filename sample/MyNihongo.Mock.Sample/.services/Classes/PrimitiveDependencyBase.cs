namespace MyNihongo.Mock.Sample;

public abstract class PrimitiveDependencyBase
{
	public abstract void Invoke();

	public virtual void InvokeWithParameter(float parameter)
	{
	}

	public void CannotOverride()
	{
		Invoke();
	}
}