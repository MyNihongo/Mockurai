namespace MyNihongo.Mock.Models;

internal readonly ref struct MockImplementationResult(string name, string source)
{
	public readonly string Name = name, Source = source;
}
