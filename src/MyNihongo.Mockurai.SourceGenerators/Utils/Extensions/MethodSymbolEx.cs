namespace MyNihongo.Mockurai.Utils;

internal static class MethodSymbolEx
{
	private const bool DefaultAppendVerifyPrefix = true;

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
			return @this.GetReturnTypeMetadata().ReturnType;
		}

		public ReturnTypeMetadata GetReturnTypeMetadata()
		{
			if (@this.ReturnsVoid)
				return default;

			var returnType = @this.ReturnType;
			if (returnType.Name is "Task" or "ValueTask" && returnType.ContainingNamespace.ToString() == "System.Threading.Tasks" && returnType is INamedTypeSymbol returnTypeNamedSymbol)
			{
				var name = $"{returnType.ContainingNamespace}.{returnType.Name}";

				returnType = !returnTypeNamedSymbol.TypeArguments.IsDefaultOrEmpty
					? returnTypeNamedSymbol.TypeArguments[0]
					: null;

				return new ReturnTypeMetadata(returnType, name);
			}

			return new ReturnTypeMetadata(returnType, staticInitializer: null);
		}
	}

	extension(StringBuilder @this)
	{
		private StringBuilder AppendInterface(string interfaceName, ITypeSymbol? returnTypeSymbol)
		{
			@this
				.Append(interfaceName)
				.Append("<System.Action");

			if (returnTypeSymbol is not null)
			{
				@this
					.Append(", ")
					.AppendType(returnTypeSymbol)
					.Append(", System.Func<")
					.AppendType(returnTypeSymbol)
					.Append(">");
			}

			return @this.Append('>');
		}

		private StringBuilder AppendInterface(string interfaceName, IParameterSymbol parameterSymbol, ITypeSymbol? returnTypeSymbol)
		{
			@this
				.Append(interfaceName)
				.Append("<System.Action")
				.AppendRefKindPrefix(parameterSymbol.RefKind)
				.Append('<')
				.AppendType(parameterSymbol.Type)
				.Append('>');

			if (returnTypeSymbol is not null)
			{
				@this
					.Append(", ")
					.AppendType(returnTypeSymbol)
					.Append(", System.Func")
					.AppendRefKindPrefix(parameterSymbol.RefKind)
					.Append('<')
					.AppendType(parameterSymbol.Type)
					.Append(", ")
					.AppendType(returnTypeSymbol)
					.Append(">");
			}

			return @this.Append(">");
		}

		public StringBuilder AppendInterface(string interfaceName, IMethodSymbol methodSymbol, ITypeSymbol? returnTypeSymbol, bool useOverriddenGenericNames)
		{
			@this
				.Append(interfaceName)
				.Append('<')
				.AppendSetupClassName(methodSymbol, useOverriddenGenericNames)
				.Append(".CallbackDelegate");

			if (returnTypeSymbol is not null)
			{
				@this.Append(", ");

				if (useOverriddenGenericNames)
					@this.Append(MockGeneratorConst.Suffixes.GenericReturnParameter);
				else
					@this.AppendType(returnTypeSymbol);

				@this
					.Append(", ")
					.AppendSetupClassName(methodSymbol, useOverriddenGenericNames)
					.Append(".ReturnsCallbackDelegate");
			}

			return @this.Append('>');
		}

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
				.Indent(indent).AppendSetupMethodDeclaration(methodSymbol, useDefaults: false, returnType: SetupMethodReturnType.Class).AppendLine()
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
					.Append('.')
					.AppendSetupParametersMethodName(parameters)
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

		public StringBuilder AppendSetupParametersMethodName(ImmutableArray<IParameterSymbol> parameters)
		{
			@this.Append("SetupParameter");

			if (parameters.Length > 1)
				@this.Append('s');

			return @this;
		}

		public void AppendVerifyMethods(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			var areGenericTypesEmpty = !methodSymbol.TryGetGenericTypeCount(mockedTypeSymbol, out _);

			// Verify times
			@this
				.Indent(indent).AppendVerifyTimesMethodDeclaration(methodSymbol).AppendLine()
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
				.Append("public long ")
				.AppendVerifyMethodName(methodSymbol)
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

		public void AppendSetupVerifyExtensionMethods(IMethodSymbol methodSymbol, string castName, int indent, bool prependNewLines = false)
		{
			if (prependNewLines)
			{
				@this
					.AppendLine()
					.AppendLine();
			}

			@this.AppendSetupExtensionMethods(methodSymbol, castName, useDefaults: true, indent);
			@this.AppendLine().AppendLine();
			@this.AppendVerifyExtensionMethods(methodSymbol, castName, indent);
		}

		private void AppendSetupExtensionMethods(IMethodSymbol methodSymbol, string castName, bool useDefaults, int indent)
		{
			@this
				.Indent(indent)
				.AppendSetupMethodDeclaration(methodSymbol, useDefaults, returnType: SetupMethodReturnType.Interface)
				.AppendLine(" =>");

			@this
				.Indent(indent + 1)
				.AppendCastCall(castName)
				.AppendSetupMethodName(methodSymbol)
				.Append('(')
				.AppendParameterNames(methodSymbol.Parameters)
				.Append(");");
		}

		public void AppendVerifyExtensionMethods(IMethodSymbol methodSymbol, string castName, int indent, bool prependNewLines = false)
		{
			if (prependNewLines)
			{
				@this
					.AppendLine()
					.AppendLine();
			}

			// Verify times (object)
			@this
				.Indent(indent)
				.AppendVerifyTimesMethodDeclaration(methodSymbol)
				.AppendLine(" =>");

			@this
				.Indent(indent + 1)
				.AppendCastCall(castName)
				.AppendVerifyMethodName(methodSymbol)
				.Append('(')
				.AppendParameterNames(methodSymbol.Parameters, appendComma: true)
				.AppendLine("times);")
				.AppendLine();

			// Verify times (func)
			@this
				.Indent(indent)
				.AppendVerifyTimesMethodDeclaration(methodSymbol, timesType: "System.Func<Times>")
				.AppendLine(" =>");

			@this
				.Indent(indent + 1)
				.AppendCastCall(castName)
				.AppendVerifyMethodName(methodSymbol)
				.Append('(')
				.AppendParameterNames(methodSymbol.Parameters, appendComma: true)
				.Append("times());");
		}

		public void AppendVerifySequenceExtensionMethods(IMethodSymbol methodSymbol, string castName, int indent, bool prependNewLines = false)
		{
			if (prependNewLines)
			{
				@this
					.AppendLine()
					.AppendLine();
			}

			@this
				.Indent(indent)
				.Append("public void ")
				.AppendVerifyMethodName(methodSymbol, appendVerifyPrefix: false)
				.Append('(')
				.AppendItParameters(methodSymbol.Parameters)
				.AppendLine(")");

			@this
				.Indent(indent++)
				.AppendLine("{");

			@this
				.Indent(indent)
				.Append("var nextIndex = ")
				.AppendCastCall(castName, thisParameterName: "@this.Mock")
				.AppendVerifyMethodName(methodSymbol)
				.Append('(')
				.AppendParameterNames(methodSymbol.Parameters, appendComma: true)
				.AppendLine("@this.VerifyIndex);");

			@this
				.Indent(indent).AppendLine("@this.VerifyIndex.Set(nextIndex);")
				.Indent(--indent).Append('}');
		}

		private StringBuilder AppendSetupMethodDeclaration(IMethodSymbol methodSymbol, bool useDefaults, SetupMethodReturnType returnType)
		{
			return @this
				.Append("public ")
				.AppendSetupMethodReturnType(methodSymbol, returnType)
				.Append(' ')
				.AppendSetupMethodName(methodSymbol)
				.Append('(')
				.AppendItParameters(methodSymbol.Parameters, useDefaults: useDefaults)
				.Append(')');
		}

		private StringBuilder AppendSetupMethodReturnType(IMethodSymbol methodSymbol, SetupMethodReturnType returnType)
		{
			return returnType switch
			{
				SetupMethodReturnType.Class => @this.AppendSetupType(methodSymbol),
				SetupMethodReturnType.Interface => @this.AppendSetupInterface(methodSymbol),
				_ => @this,
			};
		}

		private StringBuilder AppendSetupInterface(IMethodSymbol methodSymbol)
		{
			const string interfaceName = "ISetup";
			var returnType = methodSymbol.TryGetReturnType();

			return methodSymbol.Parameters.Length switch
			{
				0 => @this.AppendInterface(interfaceName, returnType),
				1 => @this.AppendInterface(interfaceName, methodSymbol.Parameters[0], returnType),
				_ => @this.AppendInterface(interfaceName, methodSymbol, returnType, useOverriddenGenericNames: false),
			};
		}

		private StringBuilder AppendSetupMethodName(IMethodSymbol methodSymbol)
		{
			return @this
				.Append("Setup")
				.AppendMethodName(methodSymbol)
				.AppendGenericTypes(methodSymbol.TypeArguments);
		}

		private StringBuilder AppendVerifyTimesMethodDeclaration(IMethodSymbol methodSymbol, string timesType = "in Times")
		{
			return @this
				.Append("public void ")
				.AppendVerifyMethodName(methodSymbol)
				.Append('(')
				.AppendItParameters(methodSymbol.Parameters, appendComma: true)
				.Append(timesType)
				.Append(" times)");
		}

		private StringBuilder AppendVerifyMethodName(IMethodSymbol methodSymbol, bool appendVerifyPrefix = DefaultAppendVerifyPrefix)
		{
			if (appendVerifyPrefix)
				@this.Append("Verify");

			return @this
				.AppendMethodName(methodSymbol)
				.AppendGenericTypes(methodSymbol.TypeArguments);
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

		public StringBuilder AppendSetupType(IMethodSymbol methodSymbol)
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
				.AppendRefKindPrefix(parameterSymbol.RefKind)
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

		public StringBuilder AppendTypesDeclaration(ImmutableArray<string> typeNames)
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
			var methodName = method.AssociatedSymbol?.GetSymbolName() ?? method.Name;

			return @this
				.AppendMethodKind(method.MethodKind)
				.AppendPropertyName(methodName);
		}

		private StringBuilder AppendItParameters(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false, ImmutableDictionary<IParameterSymbol, string>? parameterTypeOverride = null, bool useDefaults = false)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				var typeOverride = parameterTypeOverride?.GetValueOrDefault(parameters[i]);

				if (!appendComma && i > 0)
					@this.Append(", ");

				@this
					.Append("in It")
					.AppendRefKindPrefix(parameters[i].RefKind)
					.Append('<')
					.AppendType(parameters[i].Type, typeOverride)
					.Append("> ")
					.AppendParameterName(parameters[i].Name);

				if (useDefaults)
					@this.Append(" = default");

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		public StringBuilder AppendItSetupParameters(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false, bool isNullable = false, ImmutableDictionary<IParameterSymbol, string>? parameterTypeOverride = null)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				var typeOverride = parameterTypeOverride?.GetValueOrDefault(parameters[i]);

				@this
					.Append("in ")
					.AppendItSetupType(parameters[i].Type, isNullable, typeOverride)
					.Append(' ')
					.AppendParameterName(parameters[i].Name);

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		public StringBuilder AppendInvocationDeclaration(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, int indent)
		{
			return @this.AppendInvocationDeclaration(methodSymbol, mockedTypeSymbol, memberSymbol, fieldPrefix: null, indent, out _);
		}

		public StringBuilder AppendInvocationDeclaration(IMethodSymbol methodSymbol, MockedTypeSymbol mockedTypeSymbol, MockedMemberSymbol memberSymbol, string? fieldPrefix, int indent, out ImmutableArray<string> genericTypeNames)
		{
			@this
				.AppendIfNotNullOrEmpty(fieldPrefix)
				.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
				.Append(" ??= new ")
				.AppendInvocationType(methodSymbol, mockedTypeSymbol);

			if (methodSymbol.TryGetGenericTypeNames(mockedTypeSymbol, out genericTypeNames))
			{
				@this.AppendLine("();");

				@this
					.Indent(indent)
					.Append("var ")
					.AppendParameterName(memberSymbol.MemberName, methodSymbol.MethodKind, suffix: MockGeneratorConst.Suffixes.Invocation)
					.Append(" = (")
					.AppendInvocationType(methodSymbol)
					.Append(')')
					.AppendIfNotNullOrEmpty(fieldPrefix)
					.AppendInvocationFieldName(memberSymbol.MemberName, methodSymbol.MethodKind)
					.Append(".GetOrAdd(")
					.AppendTypesDeclaration(genericTypeNames)
					.Append(", static key => new ")
					.AppendInvocationType(methodSymbol);
			}

			@this.Append('(');

			var isStringInterpolated = !genericTypeNames.IsDefaultOrEmpty || (mockedTypeSymbol.TypeSymbol as INamedTypeSymbol)?.TypeArguments.Length > 0;
			if (isStringInterpolated)
				@this.Append('$');

			@this
				.Append('"')
				.Append(mockedTypeSymbol.TypeSymbol.Name)
				.AppendGenericTypes(mockedTypeSymbol.TypeSymbol, appendTypeOfName: true)
				.Append('.')
				.AppendInvocationDeclarationMethodName(memberSymbol.Symbol, out var parameterPlaceholderIndex)
				.AppendInvocationMethodGenericParameters(methodSymbol, genericTypeNames);

			switch (methodSymbol.MethodKind)
			{
				case MethodKind.PropertyGet:
					@this.Append(".get");
					break;
				case MethodKind.PropertySet:
					@this.Append(".set = ").AppendStringFormat(index: parameterPlaceholderIndex, isStringInterpolated);
					break;
				case MethodKind.EventAdd:
					@this.Append(".add += ").AppendStringFormat(index: 0, isStringInterpolated);
					break;
				case MethodKind.EventRemove:
					@this.Append(".remove -= ").AppendStringFormat(index: 0, isStringInterpolated);
					break;
				default:
					@this.AppendParameterPlaceholders(methodSymbol, isStringInterpolated);
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

		private StringBuilder AppendInvocationDeclarationMethodName(ISymbol symbol, out int parameterPlaceholderIndex)
		{
			if (symbol.TryGetIndexerProperty(out var indexerPropertySymbol))
			{
				@this.Append("This[");

				for (parameterPlaceholderIndex = 0; parameterPlaceholderIndex < indexerPropertySymbol.Parameters.Length; parameterPlaceholderIndex++)
				{
					if (parameterPlaceholderIndex > 0)
						@this.Append(", ");

					@this
						.Append('{')
						.Append(parameterPlaceholderIndex)
						.Append('}');
				}

				return @this.Append(']');
			}

			parameterPlaceholderIndex = 0;
			return @this.AppendPropertyName(symbol.Name);
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

		private void AppendParameterPlaceholders(IMethodSymbol methodSymbol, bool isStringInterpolated)
		{
			@this.Append('(');

			for (var i = 0; i < methodSymbol.Parameters.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				@this.AppendStringFormat(i, isStringInterpolated);
			}

			@this.Append(')');
		}

		private void AppendStringFormat(int index, bool isStringInterpolated)
		{
			if (isStringInterpolated)
				@this.Append("{{");
			else
				@this.Append('{');

			@this.Append(index);

			if (isStringInterpolated)
				@this.Append("}}");
			else
				@this.Append('}');
		}

		private void TryAppendParameterPrefixes(IMethodSymbol methodSymbol)
		{
			if (methodSymbol.Parameters.Length == 0)
				return;

			var customPrefixName = methodSymbol.Parameters.Length >= 2;
			foreach (var parameter in methodSymbol.Parameters)
			{
				var refKindString = parameter.RefKind.GetString(pascalCase: false);
				if (string.IsNullOrEmpty(refKindString))
					continue;

				@this.Append(", ");

				if (customPrefixName)
					@this.AppendPrefixParameter();
				else
					@this.Append(MockGeneratorConst.Suffixes.Prefix);

				@this
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

		public StringBuilder AppendPrefixParameter()
		{
			return @this
				.AppendParameterName(MockGeneratorConst.Suffixes.Prefix)
				.AppendPropertyName(MockGeneratorConst.Variables.Parameter);
		}
	}

	private enum SetupMethodReturnType
	{
		Class,
		Interface,
	}
}
