namespace MyNihongo.Mock.Tests;

public readonly struct TypeModel(string type, int index, string? refType = null)
{
	public readonly string Type = type;
	public readonly int Index = index;
	public readonly string? RefType = refType;

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
			return $"param{@this.Index}";
		}
		
		public string GetParameterDeclarationString(string? typeNameOverride = null)
		{
			var result = string.IsNullOrEmpty(typeNameOverride)
				? $"{@this.GetTypeString()} {@this.GetParameterNameString()}"
				: typeNameOverride;
			
			return !string.IsNullOrEmpty(@this.RefType)
				? $"{@this.RefType} {result}"
				:  result;
		}

		public string GetTypeString()
		{
			return @this.Type switch
			{
				"Int32" => "int",
				"Single" => "float",
				"Int64" => "long",
				"Double" => "double",
				_ => throw new NotImplementedException($"Unsupported type: `{@this}`"),
			};
		}
	}
}
