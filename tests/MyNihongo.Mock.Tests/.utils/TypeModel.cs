namespace MyNihongo.Mock.Tests;

public readonly struct TypeModel
{
	public readonly string Type;
	public readonly int Index;
	public readonly string? RefType, Name;
	public readonly bool IsGeneric;

	public TypeModel(string type, int index, string? refType = null, bool isGeneric = false)
	{
		Type = type;
		Index = index;
		RefType = refType;
		IsGeneric = isGeneric;
	}

	public TypeModel(string type, string name, string? refType = null, bool isGeneric = false)
	{
		Type = type;
		Name = name;
		RefType = refType;
		IsGeneric = isGeneric;
	}

	public override string ToString()
	{
		return !string.IsNullOrEmpty(RefType)
			? $"{char.ToUpperInvariant(RefType[0])}{RefType[1..]}{Type}"
			: Type;
	}
}

public static class TypeModelEx
{
	extension(TypeModel @this)
	{
		public string GetParameterNameString()
		{
			return string.IsNullOrEmpty(@this.Name)
				? $"param{@this.Index}"
				: @this.Name;
		}

		public string GetParameterDeclarationString(string? typeNameOverride = null)
		{
			var result = string.IsNullOrEmpty(typeNameOverride)
				? $"{@this.GetTypeString()} {@this.GetParameterNameString()}"
				: typeNameOverride;

			return !string.IsNullOrEmpty(@this.RefType)
				? $"{@this.RefType} {result}"
				: result;
		}

		public string GetTypeString()
		{
			return @this.Type switch
			{
				"Int32" => "int",
				"Single" => "float",
				"Int64" => "long",
				"Double" => "double",
				"T1" or "T2" or "T3" => @this.Type,
				_ => throw new NotImplementedException($"Unsupported type: `{@this}`"),
			};
		}
	}
}
