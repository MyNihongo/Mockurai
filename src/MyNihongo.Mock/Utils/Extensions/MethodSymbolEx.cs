namespace MyNihongo.Mock.Utils;

internal static class MethodSymbolEx
{
	extension(IMethodSymbol @this)
	{
		private bool TryGetGenericTypeCount(MockedTypeSymbol mockedTypeSymbol, out int count)
		{
			if (mockedTypeSymbol.GenericTypeParameterNames is null)
			{
				count = @this.TypeArguments.Length;
			}
			else
			{
				count = 0;
				foreach (var genericType in @this.TypeArguments)
				{
					if (!mockedTypeSymbol.GenericTypeParameterNames.Contains(genericType.Name))
						count++;
				}
			}

			return count > 0;
		}
	}

	extension(StringBuilder @this)
	{
		public void AppendSetupInvocationFields(IMethodSymbol method, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			@this
				.Indent(indent)
				.Append("private ")
				.AppendSetupType(method, mockedTypeSymbol)
				.Append("? ")
				.AppendFieldName(memberSymbol.MemberName, method.MethodKind)
				.AppendLine(";");

			@this.AppendInvocationField(method, mockedTypeSymbol, memberSymbol, indent);
		}

		public void AppendInvocationField(IMethodSymbol method, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			@this
				.Indent(indent)
				.Append("private ")
				.AppendInvocationType(method, mockedTypeSymbol)
				.Append("? ")
				.AppendInvocationFieldName(memberSymbol.MemberName, method.MethodKind)
				.AppendLine(";");
		}

		public StringBuilder AppendSetupMethod(IMethodSymbol method, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
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
				.AppendSetupType(method, mockedTypeSymbol)
				.AppendLine("();");

			if (method.Parameters.TryGetInputParameters(out var parameters))
			{
				@this
					.Indent(indent)
					.AppendFieldName(memberSymbol.MemberName, method.MethodKind)
					.Append(".SetupParameter");

				if (parameters.Length > 1)
					@this.Append('s');

				@this
					.Append('(')
					.AppendParameterNames(parameters)
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

		public void AppendVerifyMethods(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
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

		private StringBuilder AppendSetupType(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol)
		{
			return methodSymbol.TryGetGenericTypeCount(mockedTypeSymbol, out var genericTypeCount)
				? @this.AppendGenericSetupType(methodSymbol, genericTypeCount)
				: @this.AppendSetupType(methodSymbol);
		}

		private StringBuilder AppendSetupType(IMethodSymbol methodSymbol)
		{
			if (methodSymbol.ReturnsVoid)
			{
				return methodSymbol.Parameters.Length switch
				{
					0 => @this.Append("Setup"),
					1 => @this.AppendSetupWithParameter(methodSymbol.Parameters[0]).Append('>'),
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
					.AppendSetupWithParameter(methodSymbol.Parameters[0])
					.Append(", ")
					.AppendType(methodSymbol.ReturnType)
					.Append('>'),

				_ => @this.AppendSetupClassName(methodSymbol.Parameters, methodSymbol.ReturnType),
			};
		}

		private StringBuilder AppendGenericSetupType(IMethodSymbol methodSymbol, int genericTypeCount)
		{
			@this
				.Append("System.Collections.Concurrent.ConcurrentDictionary<")
				.AppendTypesType(genericTypeCount)
				.Append(", ");

			var setupType = methodSymbol is { ReturnsVoid: true, Parameters.Length: 0 } ? "Setup" : "object";
			return @this.Append(setupType).Append('>');
		}

		private StringBuilder AppendSetupWithParameter(IParameterSymbol parameterSymbol)
		{
			return @this
				.Append("SetupWith")
				.AppendRefKind(parameterSymbol.RefKind)
				.Append("Parameter<")
				.AppendType(parameterSymbol.Type);
		}

		private StringBuilder AppendInvocationType(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol)
		{
			return methodSymbol.TryGetGenericTypeCount(mockedTypeSymbol, out var genericTypeCount)
				? @this.AppendGenericInvocationType(genericTypeCount)
				: @this.AppendInvocationType(methodSymbol);
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

		private StringBuilder AppendGenericInvocationType(int genericTypeCount)
		{
			@this.Append("InvocationDictionary");

			return genericTypeCount > 1
				? @this.AppendTypesType(genericTypeCount)
				: @this;
		}

		private StringBuilder AppendTypesType(int typesCount)
		{
			if (typesCount == 1)
				return @this.Append("Type");

			@this.Append('(');
			for (var i = 0; i < typesCount; i++)
			{
				if (i < 0)
					@this.Append(", ");

				@this.Append("Type");
			}

			return @this.Append(')');
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
					.AppendRefKind(parameters[i].RefKind)
					.Append('<')
					.AppendType(parameters[i].Type)
					.Append("> ")
					.AppendParameterName(parameters[i].Name);

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		private StringBuilder AppendInvocationDeclaration(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol)
		{
			@this
				.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append(" ??= new ")
				.AppendInvocationType(methodSymbol, mockedTypeSymbol)
				.Append("(\"")
				.Append(mockedTypeSymbol.TypeSymbol.Name)
				.AppendGenericTypes(mockedTypeSymbol.TypeSymbol)
				.Append('.')
				.AppendPropertyName(memberSymbol.Symbol.Name);

			switch (methodSymbol.MethodKind)
			{
				case MethodKind.PropertyGet:
					@this.Append(".get");
					break;
				case MethodKind.PropertySet:
					@this.Append(".set = {0}");
					break;
				case MethodKind.EventAdd:
					@this.Append(".add += {0}");
					break;
				case MethodKind.EventRemove:
					@this.Append(".remove -= {0}");
					break;
				default:
					@this.AppendParameterPlaceholders(methodSymbol);
					break;
			}

			return @this
				.Append('"')
				.TryAppendParameterPrefixes(methodSymbol)
				.Append(");");
		}

		private void AppendParameterPlaceholders(IMethodSymbol methodSymbol)
		{
			@this.Append('(');

			for (var i = 0; i < methodSymbol.Parameters.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				@this
					.Append('{')
					.Append(i)
					.Append('}');
			}

			@this.Append(')');
		}

		private StringBuilder TryAppendParameterPrefixes(IMethodSymbol methodSymbol)
		{
			if (methodSymbol.Parameters.Length == 0)
				return @this;

			var appendParameterNumber = methodSymbol.Parameters.Length > 1;

			for (var i = 0; i < methodSymbol.Parameters.Length; i++)
			{
				var refKindString = methodSymbol.Parameters[i].RefKind.GetString(camelCase: false);
				if (string.IsNullOrEmpty(refKindString))
					continue;

				@this.Append(", prefix");
				if (appendParameterNumber)
					@this.Append(i + 1);

				@this
					.Append(": \"")
					.Append(refKindString)
					.Append('"');
			}

			return @this;
		}
	}
}
