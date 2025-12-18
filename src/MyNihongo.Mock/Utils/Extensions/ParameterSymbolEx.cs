namespace MyNihongo.Mock.Utils;

internal static class ParameterSymbolEx
{
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

		public StringBuilder AppendParameter(IParameterSymbol parameter)
		{
			return @this.Append(parameter);
		}

		public StringBuilder AppendSetupClassName(ImmutableArray<IParameterSymbol> parameters, ITypeSymbol? returnTypeSymbol)
		{
			@this.Append("Setup");

			foreach (var parameter in parameters)
			{
				var refType = parameter.RefKind.GetString();

				@this
					.AppendPropertyName(refType)
					.Append(parameter.Type.Name);
			}

			if (returnTypeSymbol is not null)
			{
				@this
					.Append('<')
					.AppendType(returnTypeSymbol)
					.Append('>');
			}

			return @this;
		}
	}
}
