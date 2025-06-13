namespace MyNihongo.Mock.Models;

internal readonly struct ConfigurationOptions(in string rootNamespace)
{
	public readonly string RootNamespace = rootNamespace;
}
