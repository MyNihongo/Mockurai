namespace MyNihongo.Mock.Utils;

internal static class ParameterSymbolEx
{
	extension(ImmutableArray<IParameterSymbol> @this)
	{
		public bool TryGetInputParameters(out ImmutableArray<IParameterSymbol> parameters)
		{
			var builder = ImmutableArray.CreateBuilder<IParameterSymbol>();

			foreach (var parameter in @this)
			{
				if (parameter.RefKind != RefKind.Out)
					builder.Add(parameter);
			}

			parameters = builder.ToImmutable();
			return builder.Count > 0;
		}
	}

	// TODO: when there is more time try to optimize appending instead of appending strings of ITypeSymbol, IPropertySymbol, etc
	extension(StringBuilder @this)
	{
		public StringBuilder AppendParameters(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false, ImmutableDictionary<IParameterSymbol, string>? parameterTypeOverride = null)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				var typeOverride = parameterTypeOverride?.GetValueOrDefault(parameters[i]);
				@this.AppendParameter(parameters[i], typeOverride);

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

		private StringBuilder AppendParameter(IParameterSymbol parameter, string? typeOverride = null)
		{
			var refKindString = parameter.RefKind.GetString();
			if (!string.IsNullOrEmpty(refKindString))
				@this.Append(refKindString).Append(' ');

			return @this
				.AppendType(parameter.Type, typeOverride)
				.Append(' ')
				.Append(parameter.Name);
		}

		public StringBuilder AppendParameterNames(ImmutableArray<IParameterSymbol> parameters, string? suffix = null, bool appendComma = false)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				@this.Append(parameters[i].Name);

				if (!string.IsNullOrEmpty(suffix))
					@this.Append(suffix);

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		public StringBuilder AppendDiscardParameterNames(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				var refKindString = parameters[i].RefKind.GetString();
				if (!string.IsNullOrEmpty(refKindString))
					@this.Append(refKindString).Append(' ');

				@this.Append('_');

				if (appendComma)
					@this.Append(", ");
			}

			return @this;
		}

		public StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol, bool useOverriddenGenericNames)
		{
			var parameters = methodSymbol.Parameters;
			var returnTypeSymbol = methodSymbol.TryGetReturnType();

			return @this.AppendSetupClassName(parameters, returnTypeSymbol, typeOverrideBuilder: null, useOverriddenGenericNames);
		}

		public StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol, bool useOverriddenGenericNames, out ImmutableDictionary<IParameterSymbol, string> genericTypeOverride)
		{
			var parameters = methodSymbol.Parameters;
			var returnTypeSymbol = methodSymbol.TryGetReturnType();

			var builder = ImmutableDictionary.CreateBuilder<IParameterSymbol, string>(SymbolEqualityComparer.Default);
			@this.AppendSetupClassName(parameters, returnTypeSymbol, builder, useOverriddenGenericNames);

			genericTypeOverride = builder.ToImmutable();
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
			@this
				.Append("Setup")
				.AppendParameterRefKinds(parameters, out var genericParameters);

			if (returnTypeSymbol is not null)
			{
				@this
					.Append('<')
					.AppendGenericSetupInvocationParameters(genericParameters, useOverriddenGenericNames, typeOverrideBuilder, appendGenericSymbols: false, appendTrailingComma: true);

				if (useOverriddenGenericNames)
					@this.Append(MockGeneratorConst.Suffixes.GenericReturnParameter);
				else
					@this.AppendType(returnTypeSymbol);

				@this.Append('>');
			}
			else
			{
				@this.AppendGenericSetupInvocationParameters(genericParameters, useOverriddenGenericNames, typeOverrideBuilder);
			}

			return @this;
		}

		public StringBuilder AppendInvocationClassName(ImmutableArray<IParameterSymbol> parameters, bool useOverriddenGenericNames, out ImmutableDictionary<IParameterSymbol, string> genericTypeOverride)
		{
			var builder = ImmutableDictionary.CreateBuilder<IParameterSymbol, string>(SymbolEqualityComparer.Default);
			@this.AppendInvocationClassName(parameters, useOverriddenGenericNames, builder, appendGenericDeclaration: true);

			genericTypeOverride = builder.ToImmutable();
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
			@this
				.Append("Invocation")
				.AppendParameterRefKinds(parameters, out var genericParameters);

			return appendGenericDeclaration
				? @this.AppendGenericSetupInvocationParameters(genericParameters, useOverriddenGenericNames, typeOverrideBuilder)
				: @this;
		}

		private StringBuilder AppendParameterRefKinds(ImmutableArray<IParameterSymbol> parameters, out ImmutableArray<IParameterSymbol> genericParameters)
		{
			ImmutableArray<IParameterSymbol>.Builder? builder = null;

			foreach (var parameter in parameters)
			{
				if (parameter.Type is ITypeParameterSymbol)
				{
					builder ??= ImmutableArray.CreateBuilder<IParameterSymbol>();
					builder.Add(parameter);

					@this
						.Append(MockGeneratorConst.Suffixes.GenericParameter)
						.Append(builder.Count);
				}
				else
				{
					@this
						.AppendRefKindPrefix(parameter.RefKind)
						.Append(parameter.Type.Name);
				}
			}

			genericParameters = builder?.ToImmutable() ?? ImmutableArray<IParameterSymbol>.Empty;
			return @this;
		}

		private StringBuilder AppendGenericSetupInvocationParameters(
			ImmutableArray<IParameterSymbol> genericParameters,
			bool useOverriddenGenericNames,
			ImmutableDictionary<IParameterSymbol, string>.Builder? typeOverrideBuilder,
			bool appendGenericSymbols = true,
			bool appendTrailingComma = false)
		{
			if (genericParameters.IsDefaultOrEmpty)
				return @this;

			if (appendGenericSymbols)
				@this.Append('<');

			for (var i = 0; i < genericParameters.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				if (useOverriddenGenericNames)
				{
					var typeNameOverride = MockGeneratorConst.Suffixes.GenericParameter + (i + 1);
					@this.Append(typeNameOverride);

					typeOverrideBuilder?[genericParameters[i]] = typeNameOverride;
				}
				else
				{
					@this.AppendType(genericParameters[i].Type);
				}
			}

			if (appendGenericSymbols)
				@this.Append('>');
			else if (appendTrailingComma)
				@this.Append(", ");

			return @this;
		}
	}
}
