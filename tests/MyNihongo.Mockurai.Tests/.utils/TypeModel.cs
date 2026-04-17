namespace MyNihongo.Mockurai.Tests;

// TODO: this class is super mess. Consider refactoring it at a later point
public readonly struct TypeModel
{
	public readonly string Name;
	public readonly int Index;
	public readonly string? Namespace, RefType;
	public readonly bool IsGeneric, IsNullable;
	public readonly string[] GenericTypes = [];
	public readonly Nested[]? GenericParameters;

	public TypeModel(string type, int index, string? refType = null, bool isGeneric = false, bool isNullable = false)
	{
		Name = type;
		Index = index;
		RefType = refType;
		IsGeneric = isGeneric;
		IsNullable = isNullable;

		if (isGeneric)
			GenericTypes = [type];
	}

	public TypeModel(string type, Nested[] genericParameters, int index, string? @namespace = null, string? refType = null, bool isNullable = false)
	{
		GenericParameters = genericParameters;

		Name = type;
		Namespace = @namespace;
		Index = index;
		RefType = refType;
		IsNullable = isNullable;

		var hashSet = new HashSet<string>();
		var queue = new Queue<Nested>();

		foreach (var nested in genericParameters)
			queue.Enqueue(nested);

		while (queue.TryDequeue(out var nested))
		{
			if (nested.IsGeneric)
				hashSet.Add(nested.Type);

			if (nested.NestedTypes is not null)
			{
				foreach (var nestedType in nested.NestedTypes)
					queue.Enqueue(nestedType);
			}
		}

		GenericTypes = hashSet.ToArray();
	}

	public bool HasGenericTypes => IsGeneric || GenericTypes.Length > 0;

	public override string ToString()
	{
		var name = !string.IsNullOrEmpty(RefType)
			? $"{char.ToUpperInvariant(RefType[0])}{RefType[1..]}{Name}"
			: Name;

		if (IsNullable)
			name += "Nullable";

		if (GenericParameters is not null)
		{
			foreach (var genericParameter in GenericParameters)
				name += genericParameter;
		}

		return name;
	}

	public readonly struct Nested(string type, string? @namespace, Nested[]? nestedTypes, bool isGeneric)
	{
		public readonly string Type = type;
		public readonly string? Namespace = @namespace;
		public readonly Nested[]? NestedTypes = nestedTypes;
		public readonly bool IsGeneric = isGeneric;

		public static implicit operator Nested(string value) =>
			new(value, null, null, false);

		public static implicit operator Nested((string, bool) value) =>
			new(value.Item1, null, null, value.Item2);

		public static implicit operator Nested((string, Nested[]) value) =>
			new(value.Item1, null, value.Item2, false);

		public static implicit operator Nested((string, string, Nested[]) value) =>
			new(value.Item2, value.Item1, value.Item3, false);

		public static implicit operator Nested((string, Nested[], bool) value) =>
			new(value.Item1, null, value.Item2, value.Item3);

		public override string ToString()
		{
			var name = Type;

			if (NestedTypes is not null)
			{
				foreach (var nestedType in NestedTypes)
					name += nestedType;
			}

			return name;
		}

		public string ToString2()
		{
			var name = !string.IsNullOrEmpty(Namespace)
				? $"{Namespace}.{Type.TryGetTypeName() ?? Type}"
				: Type.TryGetTypeName() ?? Type;

			if (NestedTypes is not null)
			{
				name += '<';

				foreach (var nestedType in NestedTypes)
					name += nestedType.ToString2();

				name += '>';
			}

			return name;
		}
	}
}

public static class TypeModelEx
{
	extension(TypeModel @this)
	{
		public string GetParameterNameString(bool appendRefKind = false)
		{
			var name = $"parameter{@this.Index}";

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

		public string GetTypeofTypeString(bool appendBrackets = true)
		{
			var typeString = @this.GetTypeString();

			foreach (var genericType in @this.GenericTypes)
			{
				var replace = $"typeof({genericType}).Name";
				if (appendBrackets)
					replace = '{' + replace + '}';

				typeString = typeString.Replace(genericType, replace);
			}

			return typeString;
		}

		public string GetTypeString()
		{
			var typeString = @this.Name.TryGetTypeName() ?? @this.GetFullTypeName();

			return @this.IsNullable
				? $"{typeString}?"
				: typeString;
		}

		private string GetFullTypeName()
		{
			var value = !string.IsNullOrEmpty(@this.Namespace)
				? $"{@this.Namespace}.{@this.Name}"
				: @this.Name;

			if (@this is { IsGeneric: false, GenericParameters.Length: > 0 })
			{
				value += "<";

				for (var i = 0; i < @this.GenericParameters.Length; i++)
				{
					if (i > 0)
						value += ", ";

					value += @this.GenericParameters[i].ToString2();
				}

				value += ">";
			}

			return value;
		}

		public bool IsInputParameter => !"out".Equals(@this.RefType, StringComparison.InvariantCultureIgnoreCase);
	}

	extension(string @this)
	{
		public string? TryGetTypeName()
		{
			return @this switch
			{
				"Int32" => "int",
				"Single" => "float",
				"Int64" => "long",
				"Double" => "double",
				"String" => "string",
				"Boolean" => "bool",
				_ => null,
			};
		}
	}
}
