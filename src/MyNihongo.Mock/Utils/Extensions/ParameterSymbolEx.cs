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
		public StringBuilder AppendParameters(ImmutableArray<IParameterSymbol> parameters, bool appendComma = false)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (!appendComma && i > 0)
					@this.Append(", ");

				@this.AppendParameter(parameters[i]);

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

		private StringBuilder AppendParameter(IParameterSymbol parameter)
		{
			return @this.Append(parameter);
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

		public StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol, bool useOverriddenGenericNames = false)
		{
			var parameters = methodSymbol.Parameters;
			var returnTypeSymbol = methodSymbol.TryGetReturnType();

			return @this.AppendSetupClassName(parameters, returnTypeSymbol, useOverriddenGenericNames);
		}

		public StringBuilder AppendSetupClassName(ImmutableArray<IParameterSymbol> parameters, ITypeSymbol? returnTypeSymbol, bool useOverriddenGenericNames = false)
		{
			@this
				.Append("Setup")
				.AppendParameterRefKinds(parameters, out var genericParameters);

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

		public StringBuilder AppendInvocationClassName(ImmutableArray<IParameterSymbol> parameters, bool useOverriddenGenericNames = false)
		{
			return @this
				.Append("Invocation")
				.AppendParameterRefKinds(parameters, out var genericParameters)
				.AppendGenericSetupInvocationParameters(genericParameters, useOverriddenGenericNames);
		}

		private StringBuilder AppendParameterRefKinds(ImmutableArray<IParameterSymbol> parameters, out ImmutableArray<ITypeParameterSymbol> genericParameters)
		{
			ImmutableArray<ITypeParameterSymbol>.Builder? builder = null;

			foreach (var parameter in parameters)
			{
				if (parameter.Type is ITypeParameterSymbol typeParameterSymbol)
				{
					builder ??= ImmutableArray.CreateBuilder<ITypeParameterSymbol>();
					builder.Add(typeParameterSymbol);

					@this
						.Append(MockGeneratorConst.Suffixes.GenericParameter)
						.Append(builder.Count);
				}
				else
				{
					@this
						.AppendRefKind(parameter.RefKind)
						.Append(parameter.Type.Name);
				}
			}

			genericParameters = builder?.ToImmutable() ?? ImmutableArray<ITypeParameterSymbol>.Empty;
			return @this;
		}

		private StringBuilder AppendGenericSetupInvocationParameters(ImmutableArray<ITypeParameterSymbol> genericParameters, bool useOverriddenGenericNames, bool appendGenericSymbols = true, bool appendTrailingComma = false)
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
					@this
						.Append(MockGeneratorConst.Suffixes.GenericParameter)
						.Append(i + 1);
				}
				else
				{
					@this.AppendType(genericParameters[i]);
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
