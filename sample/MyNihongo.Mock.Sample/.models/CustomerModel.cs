namespace MyNihongo.Mock.Sample;

public sealed record CustomerModel
{
	public required string Name { get; init; }

	public required int Age { get; init; }
}
