using System.Runtime.CompilerServices;

namespace MyNihongo.Mockurai.Utils;

internal static class MethodSymbolUtils
{
	public static ImmutableArray<IParameterSymbol> CombineParameters(IMethodSymbol? methodSymbol1, IMethodSymbol? methodSymbol2, out bool hasDefaultParameters)
	{
		ImmutableDictionary<string, OrderedParameter>.Builder? builder = null;
		hasDefaultParameters = false;
		int parameterOrder = int.MinValue, defaultParameterOrder = 0;

		if (methodSymbol1 is not null)
		{
			builder = ImmutableDictionary.CreateBuilder<string, OrderedParameter>();

			foreach (var parameter in methodSymbol1.Parameters)
			{
				var order = parameter.GetParameterOrder(ref parameterOrder, ref defaultParameterOrder);
				builder.Add(parameter.Name, new OrderedParameter(parameter, order));
				hasDefaultParameters = hasDefaultParameters || parameter.HasExplicitDefaultValue;
			}
		}

		if (methodSymbol2 is not null)
		{
			builder ??= ImmutableDictionary.CreateBuilder<string, OrderedParameter>();

			foreach (var parameter in methodSymbol2.Parameters)
			{
				var order = parameter.GetParameterOrder(ref parameterOrder, ref defaultParameterOrder);
				builder[parameter.Name] = new OrderedParameter(parameter, order);
			}
		}

		if (builder is null)
			return ImmutableArray<IParameterSymbol>.Empty;

		return
		[
			..builder.Values
				.OrderBy(static x => x.Order)
				.Select(static x => x.ParameterSymbol),
		];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int GetParameterOrder(this IParameterSymbol parameter, ref int parameterOrder, ref int defaultParameterOrder)
	{
		return parameter.HasExplicitDefaultValue
			? defaultParameterOrder++
			: parameterOrder++;
	}
}

file readonly struct OrderedParameter(IParameterSymbol parameterSymbol, int order) : IEquatable<OrderedParameter>
{
	public readonly IParameterSymbol ParameterSymbol = parameterSymbol;
	public readonly int Order = order;

	public bool Equals(OrderedParameter other)
	{
		return SymbolEqualityComparer.Default.Equals(ParameterSymbol, other.ParameterSymbol);
	}

	public override bool Equals(object? obj) =>
		obj is OrderedParameter other && Equals(other);

	public override int GetHashCode() =>
		SymbolEqualityComparer.Default.GetHashCode(ParameterSymbol);
}
