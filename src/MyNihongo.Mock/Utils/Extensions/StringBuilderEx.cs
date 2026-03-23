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

		public StringBuilder AppendFieldOrPropertyName(ISymbol? symbol)
		{
			return symbol switch
			{
				IFieldSymbol x => @this.AppendFieldName(x.Name),
				IPropertySymbol x => @this.AppendPropertyName(x.Name),
				_ => @this,
			};
		}

		public StringBuilder AppendInvocationFieldName(string? name, MethodKind? methodKind = null)
		{
			return @this.AppendFieldName(name, methodKind, suffix: MockGeneratorConst.Suffixes.Invocation);
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

		public StringBuilder AppendParameterName(string? name, MethodKind? methodKind = null, string? suffix = null)
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

			if (startIndex + 1 < name.Length)
				@this.Append(name.Substring(startIndex + 1));

			return @this
				.AppendMethodKind(methodKind)
				.Append(suffix);
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

		public StringBuilder AppendRefKindPrefix(RefKind refKind)
		{
			var stringValue = refKind.GetString(pascalCase: true);
			return @this.AppendPropertyName(stringValue);
		}

		public StringBuilder TryAppendNullableAnnotation(ISymbol? symbol)
		{
			var annotation = symbol.GetNullableAnnotation();
			if (annotation == NullableAnnotation.Annotated)
				@this.Append('?');

			return @this;
		}

		public StringBuilder AppendVerifyNoOtherCallsInvocation()
		{
			return @this.Append("?.VerifyNoOtherCalls(_invocationProviders)");
		}

		public StringBuilder AppendDeclaredAccessibility(ISymbol symbol)
		{
			var declaredAccessibility = symbol.DeclaredAccessibility.GetString();
			return @this.Append(declaredAccessibility);
		}
	}
}
