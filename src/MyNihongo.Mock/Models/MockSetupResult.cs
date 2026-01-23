namespace MyNihongo.Mock.Models;

internal readonly ref struct MockSetupResult(string name, string source)
{
	public readonly string Name = name, Source = source;
}
