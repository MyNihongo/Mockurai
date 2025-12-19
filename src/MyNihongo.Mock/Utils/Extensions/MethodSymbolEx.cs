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

			@this.AppendInvocationField(method, memberSymbol, indent);
		}

		public void AppendInvocationField(IMethodSymbol method, MemberSymbol memberSymbol, int indent)
		{
			@this
				.Indent(indent)
				.Append("private ")
				.AppendInvocationType(method)
				.Append("? ")
				.AppendInvocationFieldName(memberSymbol.MemberName, method.MethodKind)
				.AppendLine(";");
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

		public void AppendVerifyMethods(IMethodSymbol methodSymbol, ITypeSymbol mockedTypeSymbol, MemberSymbol memberSymbol, int indent)
		{
			// Verify times
			@this
				.Indent(indent)
				.Append("public void Verify")
				.AppendMethodName(methodSymbol)
				.Append('(')
				.AppendItParameters(methodSymbol.Parameters, appendComma: true)
				.AppendLine("in Times times)")
				.Indent(indent++).AppendLine("{");

			@this
				.Indent(indent)
				.AppendInvocationDeclaration(methodSymbol, mockedTypeSymbol, memberSymbol)
				.AppendLine();

			@this
				.Indent(indent)
				.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append(".Verify(")
				.AppendParameterNames(methodSymbol.Parameters, appendComma: true)
				.Append("times, _invocationProviders);").AppendLine();

			@this
				.Indent(--indent)
				.AppendLine("}")
				.AppendLine();

			// Verify index
			@this
				.Indent(indent)
				.Append("public long Verify")
				.AppendMethodName(methodSymbol)
				.Append('(')
				.AppendItParameters(methodSymbol.Parameters, appendComma: true)
				.AppendLine("long index)")
				.Indent(indent++).AppendLine("{");

			@this
				.Indent(indent)
				.AppendInvocationDeclaration(methodSymbol, mockedTypeSymbol, memberSymbol)
				.AppendLine();

			@this
				.Indent(indent)
				.Append("return ")
				.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append(".Verify(")
				.AppendParameterNames(methodSymbol.Parameters, appendComma: true)
				.Append("index, _invocationProviders);").AppendLine();

			@this
				.Indent(--indent)
				.Append('}');
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

		private StringBuilder AppendItParameters(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				@this
					.Append("in It")
					.AppendPropertyName(parameters[i].RefKind.GetString())
					.Append('<')
					.AppendType(parameters[i].Type)
					.Append("> ")
					.AppendParameterName(parameters[i].Name);

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		private StringBuilder AppendInvocationDeclaration(IMethodSymbol methodSymbol, ITypeSymbol mockedTypeSymbol, MemberSymbol memberSymbol)
		{
			@this
				.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append(" ??= new ")
				.AppendInvocationType(methodSymbol)
				.Append("(\"")
				.Append(mockedTypeSymbol.Name)
				.AppendGenericTypes(mockedTypeSymbol)
				.Append('.')
				.AppendPropertyName(memberSymbol.Symbol.Name)
				.Append('.');

			switch (methodSymbol.MethodKind)
			{
				case MethodKind.PropertyGet:
					@this.Append("get");
					break;
				case MethodKind.PropertySet:
					@this.Append("set = {0}");
					break;
			}

			return @this
				.Append("\");");
		}
	}
}
