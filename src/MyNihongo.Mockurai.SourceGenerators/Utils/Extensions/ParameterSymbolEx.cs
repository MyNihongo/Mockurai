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
		public StringBuilder AppendParameters(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false, ImmutableDictionary<IParameterSymbol, string>? parameterTypeOverride = null, bool appendRefKind = true, Func<StringBuilder, int, StringBuilder>? appendParameterName = null)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				var typeOverride = parameterTypeOverride?.GetValueOrDefault(parameters[i]);
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

		public StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol, bool useOverriddenGenericNames, out ImmutableDictionary<IParameterSymbol, string> typeOverride)
		{
			var parameters = methodSymbol.Parameters;
			var returnTypeSymbol = methodSymbol.TryGetReturnType();

			var builder = ImmutableDictionary.CreateBuilder<IParameterSymbol, string>(SymbolEqualityComparer.Default);
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
			ImmutableDictionary<IParameterSymbol, string>.Builder? typeOverrideBuilder,
			bool useOverriddenGenericNames)
		{
			const bool appendRefKind = true;

			@this
				.Append("Setup")
				.AppendParameterRefKinds(parameters, appendRefKind, out var parametersWithGenericTypes, out var genericParameters);

			TODO create overrides

			if (returnTypeSymbol is not null)
			{
				@this
					.Append('<')
					.AppendGenericSetupInvocationParameters(genericParameters, useOverriddenGenericNames, appendGenericSymbols: false, appendTrailingComma: true);

				if (useOverriddenGenericNames)
					@this.Append(MockGeneratorConst.Suffixes.GenericReturnParameter);
				else
					@this.AppendType(returnTypeSymbol);

				@this.Append('>');
			}
			else
			{
				@this.AppendGenericSetupInvocationParameters(genericParameters, useOverriddenGenericNames);
			}

			return @this;
		}

		public StringBuilder AppendInvocationClassName(ImmutableArray<IParameterSymbol> parameters, bool useOverriddenGenericNames, out ImmutableDictionary<IParameterSymbol, string> typeOverride)
		{
			var builder = ImmutableDictionary.CreateBuilder<IParameterSymbol, string>(SymbolEqualityComparer.Default);
			@this.AppendInvocationClassName(parameters, useOverriddenGenericNames, appendGenericDeclaration: true);

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
			ImmutableDictionary<IParameterSymbol, string>.Builder? typeOverrideBuilder,
			bool appendGenericDeclaration)
		{
			const bool appendRefKind = false;

			@this
				.Append("Invocation")
				.AppendParameterRefKinds(parameters, appendRefKind, out var parametersWithGenericTypes, out var genericParameters);

			TODO create overrides

			return appendGenericDeclaration
				? @this.AppendGenericSetupInvocationParameters(genericParameters, useOverriddenGenericNames)
				: @this;
		}

		private void AppendParameterRefKinds(ImmutableArray<IParameterSymbol> parameters, bool appendRefKind, out ImmutableArray<IParameterSymbol> parametersWithGenericTypes, out ImmutableDictionary<ITypeSymbol, string> genericTypes)
		{
			ImmutableHashSet<IParameterSymbol>.Builder? parameterBuilder = null;
			ImmutableDictionary<ITypeSymbol, string>.Builder? genericParameterBuilder = null;
			var genericParameterProcessQueue = new Queue<ITypeSymbol>();

			foreach (var parameter in parameters)
			{
				if (appendRefKind)
					@this.AppendRefKindPrefix(parameter.RefKind);

				if (parameter.Type is ITypeParameterSymbol)
				{
					TryAddGenericTypeParameter(@this, ref genericParameterBuilder, parameter.Type);
					TryAddParameter(ref parameterBuilder, parameter);
				}
				else
				{
					@this.Append(parameter.Type.Name);
					genericParameterProcessQueue.Enqueue(parameter.Type);
				}

				// Here we need to handle nested generic types like IList<T>, IList<ICollection<T>>, T[], etc.
				while (genericParameterProcessQueue.Count > 0)
				{
					if (genericParameterProcessQueue.Dequeue() is not INamedTypeSymbol { TypeArguments.IsDefaultOrEmpty: false } typeSymbol)
						continue;

					foreach (var typeArgument in typeSymbol.TypeArguments)
					{
						if (typeArgument is ITypeParameterSymbol)
						{
							TryAddGenericTypeParameter(@this, ref genericParameterBuilder, typeArgument);
							TryAddParameter(ref parameterBuilder, parameter);
						}

						genericParameterProcessQueue.Enqueue(typeArgument);
					}
				}
			}

			parametersWithGenericTypes = parameterBuilder?.ToImmutableArray() ?? ImmutableArray<IParameterSymbol>.Empty;
			genericTypes = genericParameterBuilder?.ToImmutable() ?? ImmutableDictionary<ITypeSymbol, string>.Empty;
			return;

			static void TryAddGenericTypeParameter(StringBuilder stringBuilder, ref ImmutableDictionary<ITypeSymbol, string>.Builder? builder, ITypeSymbol type)
			{
				builder ??= ImmutableDictionary.CreateBuilder<ITypeSymbol, string>(SymbolEqualityComparer.Default);
				if (!builder.ContainsKey(type))
				{
					var genericParameter = MockGeneratorConst.Suffixes.GenericParameter + (builder.Count + 1);
					stringBuilder.Append(genericParameter);

					builder[type] = genericParameter;
				}
			}

			static void TryAddParameter(ref ImmutableHashSet<IParameterSymbol>.Builder? builder, IParameterSymbol parameter)
			{
				builder ??= ImmutableHashSet.CreateBuilder<IParameterSymbol>(SymbolEqualityComparer.Default);
				builder.Add(parameter);
			}
		}

		private StringBuilder AppendGenericSetupInvocationParameters(
			ImmutableDictionary<ITypeSymbol, string> genericParameters,
			bool useOverriddenGenericNames,
			bool appendGenericSymbols = true,
			bool appendTrailingComma = false)
		{
			if (genericParameters.IsEmpty)
				return @this;

			if (appendGenericSymbols)
				@this.Append('<');

			var i = 0;
			foreach (var genericParameter in genericParameters)
			{
				if (i > 0)
					@this.Append(", ");

				if (useOverriddenGenericNames)
					@this.Append(genericParameter.Value);
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
	}
}
