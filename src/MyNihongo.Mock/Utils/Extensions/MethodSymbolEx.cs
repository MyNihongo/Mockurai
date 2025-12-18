namespace MyNihongo.Mock.Utils;

internal static class MethodSymbolEx
{
	extension(StringBuilder @this)
	{
		public StringBuilder AppendSetupMethod(IMethodSymbol method, MemberSymbol memberSymbol, int indent)
		{
			@this
				.Indent(indent)
				.Append("public ")
				.AppendSetupType(method)
				.Append(" Setup")
				.AppendMethodName(method)
				.Append('(')
				.AppendItParameters(method.Parameters)
				.AppendLine(")")
				.Indent(indent++).AppendLine("{");

			@this
				.Indent(indent)
				.AppendFieldName(memberSymbol.MemberName, method.MethodKind)
				.Append(" ??= new ")
				.AppendSetupType(method)
				.AppendLine("();");

			if (method.Parameters.Length > 0)
			{
				@this
					.Indent(indent)
					.AppendFieldName(memberSymbol.MemberName, method.MethodKind)
					.Append(".SetupParameter");

				if (method.Parameters.Length > 1)
					@this.Append('s');

				@this
					.Append('(')
					.AppendParameterNames(method.Parameters)
					.AppendLine(");");
			}

			@this
				.Indent(indent)
				.Append("return ")
				.AppendFieldName(memberSymbol.MemberName, method.MethodKind)
				.AppendLine(";");

			return @this
				.Indent(--indent).Append('}');
		}

		public StringBuilder AppendSetupType(IMethodSymbol method)
		{
			if (method.ReturnsVoid)
			{
				return method.Parameters.Length switch
				{
					0 => @this.Append("Setup"),
					1 => @this
						.Append("SetupWithParameter<")
						.AppendType(method.Parameters[0].Type)
						.Append('>'),

					_ => @this.AppendSetupClassName(method.Parameters, returnTypeSymbol: null),
				};
			}

			return method.Parameters.Length switch
			{
				0 => @this
					.Append("Setup<")
					.AppendType(method.ReturnType)
					.Append('>'),

				1 => @this
					.Append("SetupWithParameter<")
					.AppendType(method.Parameters[0].Type)
					.Append(", ")
					.AppendType(method.ReturnType)
					.Append('>'),

				_ => @this.AppendSetupClassName(method.Parameters, method.ReturnType),
			};
		}

		private StringBuilder AppendMethodName(IMethodSymbol method)
		{
			var methodName = method.AssociatedSymbol?.Name ?? method.Name;

			return @this
				.AppendMethodKind(method.MethodKind)
				.AppendPropertyName(methodName);
		}

		private StringBuilder AppendItParameters(ImmutableArray<IParameterSymbol> parameters)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				@this
					.Append("in It")
					.AppendPropertyName(parameters[i].RefKind.GetString())
					.Append('<')
					.AppendType(parameters[i].Type)
					.Append("> ")
					.AppendParameterName(parameters[i].Name);
			}

			return @this;
		}
	}
}
