namespace MyNihongo.Mock.Utils;

internal static class MethodSymbolEx
{
	extension(StringBuilder @this)
	{
		public void AppendSetupInvocationFields(IMethodSymbol method, MemberSymbol memberSymbol, int indent)
		{
			@this
				.Indent(indent)
				.Append("private ")
				.AppendSetupType(method)
				.Append("? ")
				.AppendFieldName(memberSymbol.MemberName, method.MethodKind)
				.AppendLine(";");

			@this
				.Indent(indent)
				.Append("private ")
				.AppendInvocationType(method)
				.Append("? ")
				.AppendFieldName(memberSymbol.MemberName, method.MethodKind)
				.AppendLine("Invocation;");
		}

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

		private StringBuilder AppendSetupType(IMethodSymbol methodSymbol)
		{
			if (methodSymbol.ReturnsVoid)
			{
				return methodSymbol.Parameters.Length switch
				{
					0 => @this.Append("Setup"),
					1 => @this
						.Append("SetupWithParameter<")
						.AppendType(methodSymbol.Parameters[0].Type)
						.Append('>'),

					_ => @this.AppendSetupClassName(methodSymbol.Parameters, returnTypeSymbol: null),
				};
			}

			return methodSymbol.Parameters.Length switch
			{
				0 => @this
					.Append("Setup<")
					.AppendType(methodSymbol.ReturnType)
					.Append('>'),

				1 => @this
					.Append("SetupWithParameter<")
					.AppendType(methodSymbol.Parameters[0].Type)
					.Append(", ")
					.AppendType(methodSymbol.ReturnType)
					.Append('>'),

				_ => @this.AppendSetupClassName(methodSymbol.Parameters, methodSymbol.ReturnType),
			};
		}

		private StringBuilder AppendInvocationType(IMethodSymbol methodSymbol)
		{
			return methodSymbol.Parameters.Length switch
			{
				0 => @this.Append("Invocation"),
				1 => @this.Append("Invocation<").AppendType(methodSymbol.Parameters[0].Type).Append('>'),
				_ => @this.AppendInvocationClassName(methodSymbol.Parameters),
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
