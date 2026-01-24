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
		public StringBuilder AppendParameters(ImmutableArray<IParameterSymbol> parameters)
		{
			for (var i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
					@this.Append(", ");

				@this.AppendParameter(parameters[i]);
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

		public StringBuilder AppendSetupClassName(IMethodSymbol methodSymbol, Action<StringBuilder, ITypeSymbol>? returnTypeOverride = null)
		{
			var parameters = methodSymbol.Parameters;
			var returnTypeSymbol = methodSymbol.TryGetReturnType();

			return @this.AppendSetupClassName(parameters, returnTypeSymbol, returnTypeOverride);
		}

		public StringBuilder AppendSetupClassName(ImmutableArray<IParameterSymbol> parameters, ITypeSymbol? returnTypeSymbol, Action<StringBuilder, ITypeSymbol>? returnTypeOverride = null)
		{
			@this
				.Append("Setup")
				.AppendParameterRefKinds(parameters);

			if (returnTypeSymbol is not null)
			{
				@this.Append('<');

				if (returnTypeOverride is not null)
					returnTypeOverride(@this, returnTypeSymbol);
				else
					@this.AppendType(returnTypeSymbol);

				@this.Append('>');
			}

			return @this;
		}

		public StringBuilder AppendInvocationClassName(ImmutableArray<IParameterSymbol> parameters)
		{
			return @this
				.Append("Invocation")
				.AppendParameterRefKinds(parameters);
		}

		private StringBuilder AppendParameterRefKinds(ImmutableArray<IParameterSymbol> parameters)
		{
			foreach (var parameter in parameters)
			{
				@this
					.AppendRefKind(parameter.RefKind)
					.Append(parameter.Type.Name);
			}

			return @this;
		}
	}
}
