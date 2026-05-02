namespace MyNihongo.Mockurai.Utils;

internal static class MethodSymbolUtils
{
	public static ImmutableArray<IParameterSymbol> CombineParameters(IMethodSymbol? methodSymbol1, IMethodSymbol? methodSymbol2)
	{
		ImmutableSortedDictionary<OrderedParameterName, IParameterSymbol>.Builder? builder = null;
		var order = int.MinValue;

		if (methodSymbol1 is not null)
		{
			builder = ImmutableSortedDictionary.CreateBuilder<OrderedParameterName, IParameterSymbol>();

			foreach (var parameter in methodSymbol1.Parameters)
			{
				var key = new OrderedParameterName(parameter.Name, order++);
				builder[key] = parameter;
			}
		}

		if (methodSymbol2 is not null)
		{
			builder ??= ImmutableSortedDictionary.CreateBuilder<OrderedParameterName, IParameterSymbol>();

			foreach (var parameter in methodSymbol2.Parameters)
			{
				var key = new OrderedParameterName(parameter.Name, order++);
				builder[key] = parameter;
			}
		}

		return builder?.Values.ToImmutableArray() ?? ImmutableArray<IParameterSymbol>.Empty;
	}
}

file readonly struct OrderedParameterName(string name, int order) : IEquatable<OrderedParameterName>, IComparable<OrderedParameterName>
{
	public readonly string Name = name;
	public readonly int Order = order;

	public bool Equals(OrderedParameterName other) =>
		Name == other.Name;

	public override bool Equals(object? obj) =>
		obj is OrderedParameterName other && Equals(other);

	public override int GetHashCode() =>
		Name.GetHashCode();

	public int CompareTo(OrderedParameterName other) =>
		Order.CompareTo(other.Order);
}
