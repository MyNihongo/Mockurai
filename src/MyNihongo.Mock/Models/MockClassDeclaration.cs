namespace MyNihongo.Mock.Models;

public sealed class MockClassDeclaration
{
	public readonly ITypeSymbol Type;
	public readonly IFieldSymbol? Field;
	public readonly IPropertySymbol? Property;

	public MockClassDeclaration(ITypeSymbol type, IFieldSymbol field)
	{
		Type = type;
		Field = field;
	}

	public MockClassDeclaration(ITypeSymbol type, IPropertySymbol property)
	{
		Type = type;
		Property = property;
	}

	public override string ToString()
	{
		if (Field is not null)
			return $"Field: ({Type}, {Field})";
		if (Property is not null)
			return $"Property ({Type}, {Property})";

		return string.Empty;
	}
}
