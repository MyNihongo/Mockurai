namespace MyNihongo.Mockurai;

internal sealed record SectionInfo(
	string Name,
	object? Value,
	Type SectionType
);
