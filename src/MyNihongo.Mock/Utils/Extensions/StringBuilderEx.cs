namespace MyNihongo.Mock.Utils;

internal static class StringBuilderEx
{
	extension(StringBuilder @this)
	{
		public StringBuilder Indent(int times)
		{
			for (var i = 0; i < times; ++i)
				@this.Append('\t');

			return @this;
		}

		public StringBuilder AppendInvocationFieldName(string? name, MethodKind? methodKind = null)
		{
			return @this.AppendFieldName(name, methodKind, suffix: "Invocation");
		}

		public StringBuilder AppendFieldName(string? name, MethodKind? methodKind = null, string? suffix = null)
		{
			if (string.IsNullOrEmpty(name))
				return @this;

			if (name![0] == '_')
				return @this.Append(name);

			@this
				.Append('_')
				.Append(char.ToLower(name[0]));

			if (name.Length > 1)
				@this.Append(name.Substring(1));

			return @this
				.AppendMethodKind(methodKind)
				.Append(suffix);
		}

		public StringBuilder AppendParameterName(string? name)
		{
			if (string.IsNullOrEmpty(name))
				return @this;

			var startIndex = 0;
			if (name![startIndex] == '_')
			{
				if (name.Length <= ++startIndex)
					return @this;
			}

			@this.Append(char.ToLower(name[startIndex]));

			return startIndex + 1 < name.Length
				? @this.Append(name.Substring(startIndex + 1))
				: @this;
		}

		public StringBuilder AppendPropertyName(string? name)
		{
			if (string.IsNullOrEmpty(name))
				return @this;

			var startIndex = 0;
			if (name![startIndex] == '_')
			{
				if (name.Length <= ++startIndex)
					return @this;
			}

			@this.Append(char.ToUpper(name[startIndex]));

			return startIndex + 1 < name.Length
				? @this.Append(name.Substring(startIndex + 1))
				: @this;
		}

		public StringBuilder AppendMethodKind(MethodKind? methodKind)
		{
			switch (methodKind)
			{
				case MethodKind.PropertyGet:
					@this.Append("Get");
					break;
				case MethodKind.PropertySet:
					@this.Append("Set");
					break;
				case MethodKind.EventAdd:
					@this.Append("Add");
					break;
				case MethodKind.EventRemove:
					@this.Append("Remove");
					break;
			}

			return @this;
		}
	}
}
