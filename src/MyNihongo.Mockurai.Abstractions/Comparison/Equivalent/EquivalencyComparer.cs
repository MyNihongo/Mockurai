using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;

namespace MyNihongo.Mockurai;

/// <summary>
/// Compares two object graphs by structural equivalence, recursively walking public properties, public fields,
/// and enumerable elements, and reporting any differences as a <see cref="ComparisonResult"/>.
/// </summary>
public class EquivalencyComparer
{
	private readonly PropertyInfo[]? _properties;
	private readonly FieldInfo[]? _fields;
	private readonly ConcurrentDictionary<Type, EquivalencyComparer> _nestedComparers = new();
	private readonly Type _type;
	private readonly bool _isEnumerable, _isComparedByEquivalency;

	/// <summary>
	/// Initializes a new instance of the <see cref="EquivalencyComparer"/> class for the specified type,
	/// caching its public properties and fields used for equivalency comparison.
	/// </summary>
	/// <param name="type">The runtime type whose values will be compared by this instance.</param>
	protected EquivalencyComparer(in Type type)
	{
		_type = type;

		_isComparedByEquivalency = ComparedByEquivalency(type);
		if (!_isComparedByEquivalency)
			return;

		_isEnumerable = type.IsAssignableTo(typeof(IEnumerable));
		if (_isEnumerable)
			return;

		const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

		_properties = type.GetProperties(flags);
		_fields = type.GetFields(flags);
	}

	/// <summary>
	/// Compares two values for structural equivalence, accumulating any differences into <paramref name="result"/>.
	/// </summary>
	/// <param name="x">The expected value.</param>
	/// <param name="y">The actual value.</param>
	/// <param name="result">The result instance that collects detected differences.</param>
	/// <param name="path">The dotted member path of the current node in the object graph, or <see langword="null"/> for the root.</param>
	/// <returns>The same <paramref name="result"/> instance, populated with any differences found.</returns>
	protected ComparisonResult Equivalent(in object? x, in object? y, in ComparisonResult result, in string? path)
	{
		if (_properties is not null)
		{
			foreach (var property in _properties)
			{
				var propertyPath = !string.IsNullOrEmpty(path)
					? $"{path}.{property.Name}"
					: property.Name;

				var xValue = property.GetValue(x);
				var yValue = property.GetValue(y);

				if (xValue is null)
				{
					if (yValue is not null)
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
		}

		if (_fields is not null)
		{
			foreach (var field in _fields)
			{
				var propertyPath = !string.IsNullOrEmpty(path)
					? $"{path}.{field.Name}"
					: field.Name;

				var xValue = field.GetValue(x);
				var yValue = field.GetValue(y);

				if (xValue is null)
				{
					if (yValue is not null)
						result.Add(propertyPath, "null", yValue.ToString());
				}
				else if (yValue is null)
				{
					result.Add(propertyPath, xValue.ToString(), "null");
				}
				else if (ComparedByEquivalency(field.FieldType))
				{
					var equalityComparer = _nestedComparers.GetOrAdd(field.FieldType, static x => new EquivalencyComparer(x));
					equalityComparer.Equivalent(xValue, yValue, result, propertyPath);
				}
				else
				{
					if (!xValue.Equals(yValue))
						result.Add(propertyPath, xValue.ToString(), yValue.ToString());
				}
			}
		}

		if (_isEnumerable)
		{
			var propertyPath = GetPropertyPathOrRoot(path);

			if (x is null)
			{
				if (y is not null)
					result.Add(propertyPath, "null", y.ToString());
			}
			else if (y is null)
			{
				result.Add(propertyPath, x.ToString(), "null");
			}
			else
			{
				int xCount = 0, yCount = 0;
				IEnumerable xEnumerable = (IEnumerable)x, yEnumerable = (IEnumerable)y;
				var elementType = _type.TryGetElementType();

				IEnumerator xEnumerator = xEnumerable.GetEnumerator(), yEnumerator = yEnumerable.GetEnumerator();

				try
				{
					while (xEnumerator.MoveNext())
					{
						xCount++;

						var xValue = xEnumerator.Current;
						elementType ??= xValue?.GetType();

						if (!yEnumerator.MoveNext())
						{
							result.Add(propertyPath, $"collection with at least {xCount} elements", $"collection with {yCount} elements");
							goto Exit;
						}

						yCount++;

						var yValue = yEnumerator.Current;
						elementType ??= yValue?.GetType();

						if (elementType is null)
						{
							if (xValue is not null || yValue is not null)
								throw new InvalidOperationException($"Unable to determine a type of the collection, type=`{_type}`");

							continue;
						}

						var equalityComparer = _nestedComparers.GetOrAdd(elementType, static x => new EquivalencyComparer(x));
						equalityComparer.Equivalent(xValue, yValue, result, $"{propertyPath}[{xCount - 1}]");
					}

					if (yEnumerator.MoveNext())
					{
						yCount++;
						result.Add(propertyPath, $"collection with {xCount} elements", $"collection with at least {yCount} elements");
					}

					Exit: ;
				}
				finally
				{
					if (xEnumerator is IDisposable xDisposable)
						xDisposable.Dispose();
					if (yEnumerator is IDisposable yDisposable)
						yDisposable.Dispose();
				}
			}
		}

		if (!_isComparedByEquivalency)
		{
			var propertyPath = GetPropertyPathOrRoot(path);

			if (x is null)
			{
				if (y is not null)
					result.Add(propertyPath, "null", y.ToString());
			}
			else if (y is null)
			{
				result.Add(propertyPath, x.ToString(), "null");
			}
			else
			{
				if (!x.Equals(y))
					result.Add(propertyPath, x.ToString(), y.ToString());
			}
		}

		return result;
	}

	private static bool ComparedByEquivalency(in Type type)
	{
		return (type.IsClass || type.IsInterface) && type != typeof(string);
	}

	private static string GetPropertyPathOrRoot(string? path)
	{
		return string.IsNullOrEmpty(path)
			? ComparisonResult.RootPath
			: path;
	}
}

/// <summary>
/// Strongly-typed <see cref="EquivalencyComparer"/> that compares two instances of <typeparamref name="T"/> for structural equivalence.
/// </summary>
/// <typeparam name="T">The type of the values being compared.</typeparam>
public sealed class EquivalencyComparer<T> : EquivalencyComparer, IEquivalencyComparer<T>
{
	/// <summary>
	/// Gets the shared default instance of the comparer for type <typeparamref name="T"/>.
	/// </summary>
	public static readonly EquivalencyComparer<T> Default = new();

	private EquivalencyComparer()
		: base(typeof(T))
	{
	}

	/// <summary>
	/// Compares two values of type <typeparamref name="T"/> for structural equivalence.
	/// </summary>
	/// <param name="x">The expected value.</param>
	/// <param name="y">The actual value.</param>
	/// <returns>A <see cref="ComparisonResult"/> describing any detected differences.</returns>
	public ComparisonResult Equivalent(T? x, T? y)
	{
		var result = new ComparisonResult();
		return Equivalent(x, y, result, path: null);
	}
}

file static class TypeEx
{
	public static Type? TryGetElementType(this Type @this)
	{
		if (@this.IsArray)
			return @this.GetElementType();

		if (@this.IsGenericType && @this.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			return @this.GetGenericArguments()[0];

		foreach (var @interface in @this.GetInterfaces())
		{
			if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				return @interface.GetGenericArguments()[0];
		}

		return null;
	}
}
