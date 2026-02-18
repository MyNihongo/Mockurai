namespace MyNihongo.Mock.Tests;

public readonly struct TypeModel(string name, int index)
{
	public readonly string Name = name;
	public readonly int Index = index;
}
