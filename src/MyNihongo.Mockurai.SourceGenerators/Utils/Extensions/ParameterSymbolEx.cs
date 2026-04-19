namespace MyNihongo.Mockurai.Utils;

internal static class ParameterSymbolEx
{
	extension(IParameterSymbol? @this)
	{
		public ImmutableArray<IParameterSymbol> ToImmutableArray()
		{
			return @this is null
				? ImmutableArray<IParameterSymbol>.Empty
				: [@this];
		}
	}

	extension(ImmutableArray<IParameterSymbol> @this)
	{
		public bool TryGetInputParameters(out ImmutableArray<ParameterSplit.Item> parameters)
		{
			parameters = @this.SplitParameters().InputParameters;
			return parameters.Length > 0;
		}

		public ParameterSplit SplitParameters()
		{
			ImmutableArray<ParameterSplit.Item>.Builder? inputBuilder = null, outputBuilder = null;

			for (var i = 0; i < @this.Length; i++)
			{
				var parameter = @this[i];
				var item = new ParameterSplit.Item(parameter, i);

				if (parameter.RefKind == RefKind.Out)
					(outputBuilder ??= ImmutableArray.CreateBuilder<ParameterSplit.Item>()).Add(item);
				else
					(inputBuilder ??= ImmutableArray.CreateBuilder<ParameterSplit.Item>()).Add(item);
			}

			return new ParameterSplit(
				inputParameters: inputBuilder?.ToImmutable() ?? ImmutableArray<ParameterSplit.Item>.Empty,
				outputParameters: outputBuilder?.ToImmutable() ?? ImmutableArray<ParameterSplit.Item>.Empty
			);
		}

		public string GetSafeVariableName(string variableName)
		{
			var parameterNames = @this
				.Select(static x => x.Name)
				.ToImmutableHashSet();

			for (var i = 0; i < 4; i++)
			{
				if (!parameterNames.Contains(variableName))
					return variableName;

				variableName = '_' + variableName;
			}

			return $"__{Guid.NewGuid():N}__";
		}
	}

	// TODO: when there is more time try to optimize appending instead of appending strings of ITypeSymbol, IPropertySymbol, etc
	extension(StringBuilder @this)
	{
		public StringBuilder AppendParameters(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false, ImmutableDictionary<IParameterSymbol, StringTemplate>? parameterTypeOverride = null, bool appendRefKind = true, Func<StringBuilder, int, StringBuilder>? appendParameterName = null)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				var typeOverride = parameterTypeOverride
					?.GetValueOrDefault(parameters[i])
					.Build();

				@this.AppendParameterWithoutName(parameters[i], typeOverride, appendRefKind);

				if (appendParameterName is not null)
					appendParameterName(@this, i);
				else
					@this.Append(parameters[i].Name);

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		public StringBuilder TryAppendParameter(IParameterSymbol? parameter)
		{
			return parameter is not null
				? @this.AppendParameter(parameter)
				: @this;
		}

		private StringBuilder AppendParameter(IParameterSymbol parameter, string? typeOverride = null, bool appendRefKind = true)
		{
			return @this
				.AppendParameterWithoutName(parameter, typeOverride, appendRefKind)
				.Append(parameter.Name);
		}

		private StringBuilder AppendParameterWithoutName(IParameterSymbol parameter, string? typeOverride = null, bool appendRefKind = true)
		{
			if (appendRefKind)
			{
				var refKindString = parameter.RefKind.GetString();
				if (!string.IsNullOrEmpty(refKindString))
					@this.Append(refKindString).Append(' ');
			}

			return @this
				.AppendType(parameter.Type, typeOverride)
				.Append(' ');
		}

		public StringBuilder AppendParameterNames(ImmutableArray<IParameterSymbol> parameters, string? suffix = null, bool appendComma = false, bool appendRefModifier = false, Func<StringBuilder, IParameterSymbol, int, StringBuilder>? appendParameterName = null)
		{
			return @this.AppendParameterNames(parameters, convert: static x => x, suffix, appendComma, appendRefModifier, appendParameterName);
		}

		public StringBuilder AppendParameterNames(ImmutableArray<ParameterSplit.Item> parameters, string? suffix = null, bool appendComma = false, bool appendRefModifier = false, Func<StringBuilder, ParameterSplit.Item, int, StringBuilder>? appendParameterName = null)
		{
			return @this.AppendParameterNames(parameters, convert: static x => x.Parameter, suffix, appendComma, appendRefModifier, appendParameterName);
		}

		private StringBuilder AppendParameterNames<T>(ImmutableArray<T> parameters, Func<T, IParameterSymbol> convert, string? suffix, bool appendComma, bool appendRefModifier, Func<StringBuilder, T, int, StringBuilder>? appendParameterName)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				var parameter = convert(parameters[i]);
				var refKind = parameter.RefKind;

				if (!appendComma && i > 0)
					@this.Append(", ");

				if (appendRefModifier)
				{
					var refModifier = refKind.GetModifierString();
					if (!string.IsNullOrEmpty(refModifier))
						@this.Append(refModifier).Append(' ');
				}

				if (appendParameterName is not null)
					appendParameterName(@this, parameters[i], i);
				else
					@this.Append(parameter.Name);

				if (appendRefModifier && refKind == RefKind.Out)
					@this.Append('!');

				if (!string.IsNullOrEmpty(suffix))
					@this.Append(suffix);

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		public StringBuilder AppendDiscardParameterNames(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false, Func<StringBuilder, int, StringBuilder>? appendParameterName = null)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				var refKind = parameters[i].RefKind;
				var refKindString = refKind.GetString();
				if (!string.IsNullOrEmpty(refKindString))
					@this.Append(refKindString).Append(' ');

				// It is not possible to discard `out` parameters and their values must be assigned in any case (CS0177)
				if (refKind == RefKind.Out)
				{
					if (appendParameterName is not null)
						appendParameterName(@this, i);
					else
						@this.Append(parameters[i].Name);
				}
				else
				{
					@this.Append('_');
				}

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		public StringBuilder AppendParameterNamesOrDefault(ImmutableArray<IParameterSymbol> parameters)
		{
			foreach (var parameter in parameters)
			{
				@this.Append(", ");

				var parameterName = parameter.RefKind == RefKind.Out
					? "default!"
					: parameter.Name;

				@this.Append(parameterName);
			}

			return @this;
		}

		public StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol, bool useOverriddenGenericNames)
		{
			var parameters = methodSymbol.Parameters;
			var returnTypeSymbol = methodSymbol.TryGetReturnType();

			return @this.AppendSetupClassName(parameters, returnTypeSymbol, typeOverrideBuilder: null, useOverriddenGenericNames);
		}

		public StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol, bool useOverriddenGenericNames, out ImmutableDictionary<IParameterSymbol, StringTemplate> typeOverride)
		{
			var parameters = methodSymbol.Parameters;
			var returnTypeSymbol = methodSymbol.TryGetReturnType();

			var builder = ImmutableDictionary.CreateBuilder<IParameterSymbol, StringTemplate>(SymbolEqualityComparer.Default);
			@this.AppendSetupClassName(parameters, returnTypeSymbol, builder, useOverriddenGenericNames);

			typeOverride = builder.ToImmutable();
			return @this;
		}

		public StringBuilder AppendSetupClassName(ImmutableArray<IParameterSymbol> parameters, ITypeSymbol? returnTypeSymbol, bool useOverriddenGenericNames = false)
		{
			return @this.AppendSetupClassName(parameters, returnTypeSymbol, typeOverrideBuilder: null, useOverriddenGenericNames);
		}

		private StringBuilder AppendSetupClassName(
			ImmutableArray<IParameterSymbol> parameters,
			ITypeSymbol? returnTypeSymbol,
			ImmutableDictionary<IParameterSymbol, StringTemplate>.Builder? typeOverrideBuilder,
			bool useOverriddenGenericNames)
		{
			const bool appendRefKind = true;

			@this
				.Append("Setup")
				.AppendParameterRefKinds(parameters, appendRefKind, out var parametersWithGenericTypes, out var genericTypeMapping);

			TryAppendPropertyTypeOverride(typeOverrideBuilder, parametersWithGenericTypes, genericTypeMapping);

			if (returnTypeSymbol is not null)
			{
				@this
					.Append('<')
					.AppendGenericSetupInvocationParameters(genericTypeMapping, useOverriddenGenericNames, appendGenericSymbols: false, appendTrailingComma: true);

				if (useOverriddenGenericNames)
					@this.Append(MockGeneratorConst.Suffixes.GenericReturnParameter);
				else
					@this.AppendType(returnTypeSymbol);

				@this.Append('>');
			}
			else
			{
				@this.AppendGenericSetupInvocationParameters(genericTypeMapping, useOverriddenGenericNames);
			}

			return @this;
		}

		public StringBuilder AppendInvocationClassName(ImmutableArray<IParameterSymbol> parameters, bool useOverriddenGenericNames, out ImmutableDictionary<IParameterSymbol, StringTemplate> typeOverride)
		{
			var builder = ImmutableDictionary.CreateBuilder<IParameterSymbol, StringTemplate>(SymbolEqualityComparer.Default);
			@this.AppendInvocationClassName(parameters, useOverriddenGenericNames, builder, appendGenericDeclaration: true);

			typeOverride = builder.ToImmutable();
			return @this;
		}

		public StringBuilder AppendInvocationClassName(ImmutableArray<IParameterSymbol> parameters, bool useOverriddenGenericNames = false, bool appendGenericDeclaration = true)
		{
			return @this.AppendInvocationClassName(parameters, useOverriddenGenericNames, typeOverrideBuilder: null, appendGenericDeclaration);
		}

		private StringBuilder AppendInvocationClassName(
			ImmutableArray<IParameterSymbol> parameters,
			bool useOverriddenGenericNames,
			ImmutableDictionary<IParameterSymbol, StringTemplate>.Builder? typeOverrideBuilder,
			bool appendGenericDeclaration)
		{
			const bool appendRefKind = false;

			@this
				.Append("Invocation")
				.AppendParameterRefKinds(parameters, appendRefKind, out var parametersWithGenericTypes, out var genericTypeMapping);

			TryAppendPropertyTypeOverride(typeOverrideBuilder, parametersWithGenericTypes, genericTypeMapping);

			return appendGenericDeclaration
				? @this.AppendGenericSetupInvocationParameters(genericTypeMapping, useOverriddenGenericNames)
				: @this;
		}

		private void AppendParameterRefKinds(ImmutableArray<IParameterSymbol> parameters, bool appendRefKind, out ImmutableArray<IParameterSymbol> parametersWithGenericTypes, out ImmutableDictionary<ITypeSymbol, GenericTypeData> genericTypeMapping)
		{
			ImmutableHashSet<IParameterSymbol>.Builder? parameterBuilder = null;
			ImmutableDictionary<ITypeSymbol, GenericTypeData>.Builder? genericTypeMappingBuilder = null;
			var typeQueue = new Queue<ITypeSymbol>();

			foreach (var parameter in parameters)
			{
				if (appendRefKind)
					@this.AppendRefKindPrefix(parameter.RefKind);

				typeQueue.Enqueue(parameter.Type);

				while (typeQueue.Count > 0)
				{
					var typeSymbol = typeQueue.Dequeue();

					if (typeSymbol is ITypeParameterSymbol)
					{
						TryAddGenericTypeParameter(@this, ref genericTypeMappingBuilder, typeSymbol);
						TryAddParameter(ref parameterBuilder, parameter);
						continue;
					}

					if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
					{
						@this
							.Append("Array")
							.Append(arrayTypeSymbol.Rank);

						typeQueue.Enqueue(arrayTypeSymbol.ElementType);
						continue;
					}

					@this.Append(typeSymbol.Name);

					if (typeSymbol.NullableAnnotation == NullableAnnotation.Annotated)
						@this.Append("Nullable");

					if (typeSymbol is not INamedTypeSymbol namedTypeSymbol)
						continue;

					foreach (var typeArgument in namedTypeSymbol.TypeArguments)
						typeQueue.Enqueue(typeArgument);
				}
			}

			parametersWithGenericTypes = parameterBuilder?.ToImmutableArray() ?? ImmutableArray<IParameterSymbol>.Empty;
			genericTypeMapping = genericTypeMappingBuilder?.ToImmutable() ?? ImmutableDictionary<ITypeSymbol, GenericTypeData>.Empty;
			return;

			static void TryAddGenericTypeParameter(StringBuilder stringBuilder, ref ImmutableDictionary<ITypeSymbol, GenericTypeData>.Builder? builder, ITypeSymbol type)
			{
				builder ??= ImmutableDictionary.CreateBuilder<ITypeSymbol, GenericTypeData>(SymbolEqualityComparer.Default);
				if (!builder.TryGetValue(type, out var genericParameter))
				{
					var sort = builder.Count + 1;
					var name = MockGeneratorConst.Suffixes.GenericParameter + sort;
					genericParameter = builder[type] = new GenericTypeData(name, sort);
				}

				stringBuilder.Append(genericParameter.Name);
			}

			static void TryAddParameter(ref ImmutableHashSet<IParameterSymbol>.Builder? builder, IParameterSymbol parameter)
			{
				builder ??= ImmutableHashSet.CreateBuilder<IParameterSymbol>(SymbolEqualityComparer.Default);
				builder.Add(parameter);
			}
		}

		private StringBuilder AppendGenericSetupInvocationParameters(
			ImmutableDictionary<ITypeSymbol, GenericTypeData> genericParameters,
			bool useOverriddenGenericNames,
			bool appendGenericSymbols = true,
			bool appendTrailingComma = false)
		{
			if (genericParameters.IsEmpty)
				return @this;

			if (appendGenericSymbols)
				@this.Append('<');

			var i = 0;
			foreach (var genericParameter in genericParameters.OrderBy(static x => x.Value.Sort))
			{
				if (i > 0)
					@this.Append(", ");

				if (useOverriddenGenericNames)
					@this.Append(genericParameter.Value.Name);
				else
					@this.AppendType(genericParameter.Key);

				i++;
			}

			if (appendGenericSymbols)
				@this.Append('>');
			else if (appendTrailingComma)
				@this.Append(", ");

			return @this;
		}

		public StringBuilder AppendParameterTuple(ImmutableArray<IParameterSymbol> parameters, ImmutableDictionary<IParameterSymbol, StringTemplate>? parameterTypeOverride = null)
		{
			return @this
				.Append('(')
				.AppendParameters(parameters, appendRefKind: false, parameterTypeOverride: parameterTypeOverride)
				.Append(')');
		}

		public StringBuilder AppendParameterVariableTupleNames(ImmutableArray<IParameterSymbol> parameters)
		{
			@this.Append('(');

			for (var i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				@this.AppendParameterVariableName(parameters[i], i);
			}

			return @this.Append(')');
		}
	}

	private static void TryAppendPropertyTypeOverride(
		ImmutableDictionary<IParameterSymbol, StringTemplate>.Builder? typeOverrideBuilder,
		ImmutableArray<IParameterSymbol> parametersWithGenericTypes,
		ImmutableDictionary<ITypeSymbol, GenericTypeData> genericTypeMapping)
	{
		if (typeOverrideBuilder is null || parametersWithGenericTypes.IsDefaultOrEmpty)
			return;

		StringBuilder? stringBuilder = null;
		Stack<(ITypeSymbol, int, bool)>? typeStack = null;
		List<object>? parameterList = null;

		foreach (var parameter in parametersWithGenericTypes)
		{
			if (parameter.Type is ITypeParameterSymbol)
			{
				if (genericTypeMapping.TryGetValue(parameter.Type, out var genericParameterName))
					typeOverrideBuilder.Add(parameter, genericParameterName.Name);

				continue;
			}

			stringBuilder ??= new StringBuilder();
			stringBuilder.Clear();

			typeStack ??= new Stack<(ITypeSymbol, int, bool)>();
			typeStack.Push((parameter.Type, 0, false));

			parameterList ??= [];
			parameterList.Clear();

			while (typeStack.Count > 0)
			{
				var (typeSymbol, index, isVisited) = typeStack.Pop();
				if (isVisited)
				{
					if (typeSymbol is INamedTypeSymbol { TypeArguments.IsDefaultOrEmpty: false })
						stringBuilder.Append('>');
					else if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
						stringBuilder.AppendArrayBrackets(arrayTypeSymbol);
				}
				else
				{
					if (typeSymbol is ITypeParameterSymbol)
					{
						if (genericTypeMapping.TryGetValue(typeSymbol, out var genericParameterData))
						{
							stringBuilder.AppendFormatParameter(parameterList.Count);
							parameterList.Add(genericParameterData.Name);
						}
					}
					else if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
					{
						if (index > 0)
							stringBuilder.Append(", ");

						if (!namedTypeSymbol.TypeArguments.IsDefaultOrEmpty)
						{
							typeStack.Push((typeSymbol, index, true));

							stringBuilder
								.AppendTypeNamespaceAndName(typeSymbol)
								.Append('<');

							for (var i = namedTypeSymbol.TypeArguments.Length - 1; i >= 0; i--)
							{
								typeStack.Push((namedTypeSymbol.TypeArguments[i], i, false));
							}
						}
						else
						{
							stringBuilder.AppendType(typeSymbol);
						}
					}
					else if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
					{
						if (genericTypeMapping.TryGetValue(arrayTypeSymbol.ElementType, out var genericParameterData))
						{
							stringBuilder.AppendFormatParameter(parameterList.Count).AppendArrayBrackets(arrayTypeSymbol);
							parameterList.Add(genericParameterData.Name);
						}
						else
						{
							typeStack.Push((typeSymbol, index, true));
							typeStack.Push((arrayTypeSymbol.ElementType, 0, false));
						}
					}
				}
			}

			typeOverrideBuilder.Add(parameter, new StringTemplate(stringBuilder.ToString(), parameterList.ToArray()));
		}
	}

	private readonly struct GenericTypeData(string name, int sort) : IEquatable<GenericTypeData>
	{
		public readonly string Name = name;
		public readonly int Sort = sort;

		public bool Equals(GenericTypeData other)
		{
			return Name == other.Name;
		}

		public override bool Equals(object? obj) =>
			obj is GenericTypeData other && Equals(other);

		public override int GetHashCode() =>
			Name.GetHashCode();
	}
}
