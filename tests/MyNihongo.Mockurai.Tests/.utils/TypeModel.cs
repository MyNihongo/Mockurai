namespace MyNihongo.Mockurai.Tests;

public readonly struct TypeModel
{
	public readonly string Type;
	public readonly int Index;
	public readonly string? RefType, Name;
	public readonly bool IsGeneric, IsNullable;

	public TypeModel(string type, int index, string? refType = null, bool isGeneric = false, bool isNullable = false)
	{
		Type = type;
		Index = index;
		RefType = refType;
		IsGeneric = isGeneric;
		IsNullable = isNullable;
	}

	public TypeModel(string type, string name, string? refType = null, bool isGeneric = false, bool isNullable = false)
	{
		Type = type;
		Name = name;
		RefType = refType;
		IsGeneric = isGeneric;
		IsNullable = isNullable;
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
		public string GetParameterNameString(bool appendRefKind = false)
		{
			var name = string.IsNullOrEmpty(@this.Name)
				? $"param{@this.Index}"
				: @this.Name;

			return appendRefKind && !string.IsNullOrEmpty(@this.RefType)
				? $"{@this.RefType} {name}"
				: name;
		}

		public string GetCamelCaseNameString()
		{
			var parameterName = @this.GetParameterNameString();
			return char.ToUpperInvariant(parameterName[0]) + parameterName[1..];
		}

		public string GetParameterDeclarationString(string? typeNameOverride = null, bool appendRefKind = true)
		{
			var result = !string.IsNullOrEmpty(typeNameOverride)
				? @this.RefType == "out" ? @this.GetParameterNameString() : typeNameOverride
				: $"{@this.GetTypeString()} {@this.GetParameterNameString()}";

			return appendRefKind && !string.IsNullOrEmpty(@this.RefType)
				? $"{@this.RefType} {result}"
				: result;
		}

		public string GetTypeString()
		{
			var typeString = @this.Type switch
			{
				"Int32" => "int",
				"Single" => "float",
				"Int64" => "long",
				"Double" => "double",
				"String" => "string",
				"T" or "T1" or "T2" or "T3" => @this.Type,
				_ => throw new NotImplementedException($"Unsupported type: `{@this}`"),
			};

			return @this.IsNullable
				? $"{typeString}?"
				: typeString;
		}

		public bool IsInputParameter => !"out".Equals(@this.RefType, StringComparison.InvariantCultureIgnoreCase);
	}
}
