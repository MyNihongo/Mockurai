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

		private bool TryGetGenericTypeNames(MockedTypeSymbol mockedTypeSymbol, out ImmutableArray<string> result)
		{
			if (mockedTypeSymbol.GenericTypeParameterNames is null)
			{
				result =
				[
					..@this.TypeArguments
						.Select(static x => x.Name),
				];
			}
			else
			{
				var builder = ImmutableArray.CreateBuilder<string>();
				foreach (var genericType in @this.TypeArguments)
				{
					if (!mockedTypeSymbol.GenericTypeParameterNames.Contains(genericType.Name))
						builder.Add(genericType.Name);
				}

				result = builder.ToImmutable();
			}

			return result.Length > 0;
		}

		public ITypeSymbol? TryGetReturnType()
		{
			if (@this.ReturnsVoid)
				return null;

			var returnType = @this.ReturnType;
			if (returnType.Name is "Task" or "ValueTask" && returnType.ContainingNamespace.ToString() == "System.Threading.Tasks" && returnType is INamedTypeSymbol returnTypeNamedSymbol)
			{
				return !returnTypeNamedSymbol.TypeArguments.IsDefaultOrEmpty
					? returnTypeNamedSymbol.TypeArguments[0]
					: null;
			}

			return returnType;
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

		public StringBuilder AppendSetupMethod(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			@this
				.Indent(indent)
				.Append("public ")
				.AppendSetupType(methodSymbol)
				.Append(" Setup")
				.AppendMethodName(methodSymbol)
				.AppendGenericTypes(methodSymbol.TypeArguments)
				.Append('(')
				.AppendItParameters(methodSymbol.Parameters)
				.AppendLine(")")
				.Indent(indent++).AppendLine("{");

			@this
				.Indent(indent)
				.AppendFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append(" ??= new ")
				.AppendSetupType(methodSymbol, mockedTypeSymbol)
				.AppendLine("();");

			if (methodSymbol.TryGetGenericTypeNames(mockedTypeSymbol, out var genericTypeNames))
			{
				@this
					.Indent(indent)
					.Append("var ")
					.AppendParameterName(memberSymbol.MemberName, methodSymbol.MethodKind)
					.Append(" = (")
					.AppendSetupType(methodSymbol)
					.Append(')')
					.AppendFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
					.Append(".GetOrAdd(")
					.AppendTypesDeclaration(genericTypeNames)
					.Append(", static _ => new ")
					.AppendSetupType(methodSymbol)
					.AppendLine("());");
			}

			if (methodSymbol.Parameters.TryGetInputParameters(out var parameters))
			{
				@this
					.Indent(indent)
					.AppendVariableName(memberSymbol.MemberName, methodSymbol.MethodKind, isField: genericTypeNames.IsDefaultOrEmpty)
					.Append(".SetupParameter");

				if (parameters.Length > 1)
					@this.Append('s');

				@this
					.Append('(')
					.AppendParameterNames(parameters, suffix: MockGeneratorConst.Suffixes.ValueSetup)
					.AppendLine(");");
			}

			return @this
				.Indent(indent)
				.Append("return ")
				.AppendVariableName(memberSymbol.MemberName, methodSymbol.MethodKind, isField: genericTypeNames.IsDefaultOrEmpty)
				.AppendLine(";")
				.Indent(--indent).Append('}');
		}

		public void AppendVerifyMethods(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			var areGenericTypesEmpty = !methodSymbol.TryGetGenericTypeCount(mockedTypeSymbol, out _);

			// Verify times
			@this
				.Indent(indent)
				.Append("public void Verify")
				.AppendMethodName(methodSymbol)
				.AppendGenericTypes(methodSymbol.TypeArguments)
				.Append('(')
				.AppendItParameters(methodSymbol.Parameters, appendComma: true)
				.AppendLine("in Times times)")
				.Indent(indent++).AppendLine("{");

			@this
				.Indent(indent)
				.AppendInvocationDeclaration(methodSymbol, mockedTypeSymbol, memberSymbol, indent)
				.AppendLine();

			@this
				.Indent(indent)
				.AppendAppendInvocationVariableName(memberSymbol.MemberName, methodSymbol.MethodKind, isField: areGenericTypesEmpty)
				.Append(".Verify(")
				.AppendParameterNames(methodSymbol.Parameters, suffix: MockGeneratorConst.Suffixes.ValueSetup, appendComma: true)
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
				.AppendGenericTypes(methodSymbol.TypeArguments)
				.Append('(')
				.AppendItParameters(methodSymbol.Parameters, appendComma: true)
				.AppendLine("long index)")
				.Indent(indent++).AppendLine("{");

			@this
				.Indent(indent)
				.AppendInvocationDeclaration(methodSymbol, mockedTypeSymbol, memberSymbol, indent)
				.AppendLine();

			@this
				.Indent(indent)
				.Append("return ")
				.AppendAppendInvocationVariableName(memberSymbol.MemberName, methodSymbol.MethodKind, isField: areGenericTypesEmpty)
				.Append(".Verify(")
				.AppendParameterNames(methodSymbol.Parameters, suffix: MockGeneratorConst.Suffixes.ValueSetup, appendComma: true)
				.Append("index, _invocationProviders);").AppendLine();

			@this
				.Indent(--indent)
				.Append('}');
		}

		private StringBuilder AppendVariableName(string? name, MethodKind? methodKind, bool isField)
		{
			return isField
				? @this.AppendFieldName(name, methodKind)
				: @this.AppendParameterName(name, methodKind);
		}

		private StringBuilder AppendAppendInvocationVariableName(string? name, MethodKind? methodKind, bool isField)
		{
			return isField
				? @this.AppendInvocationFieldName(name, methodKind)
				: @this.AppendParameterName(name, methodKind, suffix: MockGeneratorConst.Suffixes.Invocation);
		}

		private StringBuilder AppendSetupType(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol)
		{
			return methodSymbol.TryGetGenericTypeCount(mockedTypeSymbol, out var genericTypeCount)
				? @this.AppendGenericSetupType(methodSymbol, genericTypeCount)
				: @this.AppendSetupType(methodSymbol);
		}

		private StringBuilder AppendSetupType(IMethodSymbol methodSymbol)
		{
			var returnType = methodSymbol.TryGetReturnType();
			if (returnType is null)
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
					.AppendType(returnType)
					.Append('>'),

				1 => @this
					.AppendSetupWithParameter(methodSymbol.Parameters[0])
					.Append(", ")
					.AppendType(returnType)
					.Append('>'),

				_ => @this.AppendSetupClassName(methodSymbol.Parameters, returnType),
			};
		}

		private StringBuilder AppendGenericSetupType(IMethodSymbol methodSymbol, int genericTypeCount)
		{
			@this
				.Append("System.Collections.Concurrent.ConcurrentDictionary<")
				.AppendTypesType(genericTypeCount)
				.Append(", ");

			var returnType = methodSymbol.TryGetReturnType();
			var setupType = returnType is null && methodSymbol.Parameters.IsDefaultOrEmpty ? "Setup" : "object";
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

			if (genericTypeCount <= 1)
				return @this;

			return @this
				.Append('<')
				.AppendTypesType(genericTypeCount)
				.Append('>');
		}

		private StringBuilder AppendTypesType(int typesCount)
		{
			const string typeString = "System.Type";

			if (typesCount == 1)
				return @this.Append(typeString);

			@this.Append('(');
			for (var i = 0; i < typesCount; i++)
			{
				if (i > 0)
					@this.Append(", ");

				@this.Append(typeString);
			}

			return @this.Append(')');
		}

		private StringBuilder AppendTypesDeclaration(ImmutableArray<string> typeNames)
		{
			if (typeNames.Length == 1)
				return @this.Append("typeof(").Append(typeNames[0]).Append(')');

			@this.Append('(');
			for (var i = 0; i < typeNames.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				@this
					.Append("typeof(")
					.Append(typeNames[i])
					.Append(')');
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

		public StringBuilder AppendItSetupParameters(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false, bool isNullable = false)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				@this
					.Append("in ")
					.AppendItSetupType(parameters[i].Type, isNullable)
					.Append(' ')
					.AppendParameterName(parameters[i].Name);

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		private StringBuilder AppendInvocationDeclaration(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			@this
				.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append(" ??= new ")
				.AppendInvocationType(methodSymbol, mockedTypeSymbol);

			if (methodSymbol.TryGetGenericTypeNames(mockedTypeSymbol, out var genericTypeNames))
			{
				@this.AppendLine("();");

				@this
					.Indent(indent)
					.Append("var ")
					.AppendParameterName(memberSymbol.MemberName, methodSymbol.MethodKind, suffix: MockGeneratorConst.Suffixes.Invocation)
					.Append(" = (")
					.AppendInvocationType(methodSymbol)
					.Append(')')
					.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
					.Append(".GetOrAdd(")
					.AppendTypesDeclaration(genericTypeNames)
					.Append(", static key => new ")
					.AppendInvocationType(methodSymbol);
			}

			@this.Append('(');
			if (!genericTypeNames.IsDefaultOrEmpty)
				@this.Append('$');

			@this
				.Append('"')
				.Append(mockedTypeSymbol.TypeSymbol.Name)
				.AppendGenericTypes(mockedTypeSymbol.TypeSymbol)
				.Append('.')
				.AppendPropertyName(memberSymbol.Symbol.Name)
				.AppendInvocationMethodGenericParameters(methodSymbol, genericTypeNames);

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

			@this
				.Append('"')
				.TryAppendParameterPrefixes(methodSymbol);

			// For GetOrAdd
			if (!genericTypeNames.IsDefaultOrEmpty)
				@this.Append(')');

			return @this.Append(");");
		}

		private void AppendInvocationMethodGenericParameters(IMethodSymbol methodSymbol, ImmutableArray<string> methodOnlyGenericTypeNames)
		{
			if (methodSymbol.TypeArguments.IsDefaultOrEmpty)
				return;

			if (methodOnlyGenericTypeNames.Length == 1)
			{
				@this.Append("<{key.Name}>");
				return;
			}

			@this.Append('<');

			for (int i = 0, itemCounter = 1; i < methodSymbol.TypeArguments.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				var typeArgument = methodSymbol.TypeArguments[i];
				if (methodOnlyGenericTypeNames.Contains(typeArgument.Name))
					@this.Append("{key.Item").Append(itemCounter++).Append(".Name}");
				else
					@this.AppendType(typeArgument);
			}

			@this.Append('>');
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

		private void TryAppendParameterPrefixes(IMethodSymbol methodSymbol)
		{
			if (methodSymbol.Parameters.Length == 0)
				return;

			foreach (var parameter in methodSymbol.Parameters)
			{
				var refKindString = parameter.RefKind.GetString(pascalCase: false);
				if (string.IsNullOrEmpty(refKindString))
					continue;

				@this
					.Append(", ")
					.AppendPrefixParameter(parameter.Name)
					.Append(": \"")
					.Append(refKindString)
					.Append('"');
			}
		}

		public StringBuilder AppendPrefixField(string name)
		{
			return @this
				.AppendFieldName(MockGeneratorConst.Suffixes.Prefix)
				.AppendPropertyName(name);
		}

		public StringBuilder AppendPrefixParameter(string name)
		{
			return @this
				.AppendParameterName(MockGeneratorConst.Suffixes.Prefix)
				.AppendPropertyName(name);
		}
	}
}
