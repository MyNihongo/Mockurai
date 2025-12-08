namespace MyNihongo.Mock.Sample;

public class PrimitiveDependency
{
	public virtual int Return()
	{
		return 123;
	}

	public string CannotOverride()
	{
		return Return().ToString();
	}
}