namespace MyNihongo.Mock.Tests;

public readonly struct TypeModel(string type, int index)
{
	public readonly string Type = type;
	public readonly int Index = index;

	public override string ToString()
	{
		return Type;
	}
}
