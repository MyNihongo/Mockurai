using System.Collections.Concurrent;
using System.Reflection;

namespace MyNihongo.Mock;

public class EquivalencyComparer
{
	private readonly PropertyInfo[] _properties;
	private readonly FieldInfo[] _fields;
	private readonly ConcurrentDictionary<Type, EquivalencyComparer> _nestedComparers = new();

	protected EquivalencyComparer(in Type type)
	{
		// TODO: for IEnumerable:

		const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

		_properties = type.GetProperties(flags);
		_fields = type.GetFields(flags);
	}

	protected EquivalencyComparerResult Equivalent(in object? x, in object? y, in EquivalencyComparerResult result, in string? path)
	{
		foreach (var property in _properties)
		{
			var propertyPath = !string.IsNullOrEmpty(path)
				? $"{path}.{property.Name}"
				: property.Name;

			var xValue = property.GetValue(x);
			var yValue = property.GetValue(y);

			if (xValue == null)
			{
				if (yValue != null)
					result.Add(propertyPath, "null", yValue.ToString());
			}
			else if (ComparedByEquivalency(property.PropertyType))
			{
				var equalityComparer = _nestedComparers.GetOrAdd(property.PropertyType, static x => new EquivalencyComparer(x));
				equalityComparer.Equivalent(xValue, yValue, result, propertyPath);
			}
			else
			{
				if (!xValue.Equals(yValue))
					result.Add(propertyPath, xValue.ToString(), yValue?.ToString());
			}
		}

		// TODO: fields
		return result;
	}

	private static bool ComparedByEquivalency(in Type type)
	{
		return type.IsClass && type != typeof(string);
	}
}

public sealed class EquivalencyComparer<T> : EquivalencyComparer
{
	public static readonly EquivalencyComparer<T> Default = new();

	private EquivalencyComparer()
		: base(typeof(T))
	{
	}

	public EquivalencyComparerResult Equivalent(T? x, T? y)
	{
		var result = new EquivalencyComparerResult();
		return Equivalent(x, y, result, path: null);
	}
}
