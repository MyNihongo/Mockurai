using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;

namespace MyNihongo.Mockurai;

internal static class ConfigurationTreeBuilder
{
	private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertyMap = new();

	public static ConfigurationSectionNode Build(object? configurationObject)
	{
		var root = ConfigurationSectionNode.CreateRoot();

		if (configurationObject is null)
			return root;

		var type = configurationObject.GetType();
		if (type.IsScalar() || configurationObject is IEnumerable && !IsDictionary(configurationObject))
			throw new InvalidOperationException($"`{type.FullName}` must not be a scalar type or a non-dictionary enumerable");

		AddChildren(root, configurationObject);
		return root;
	}

	private static void AddValue(ConfigurationSectionNode node, object? value)
	{
		if (value is null)
			return;

		if (value.GetType().IsScalar())
			node.Value = value.SerializeValue();
		else
			AddChildren(node, value);
	}

	private static void AddChildren(ConfigurationSectionNode node, object value)
	{
		switch (value)
		{
			case IDictionary dictionary:
			{
				foreach (DictionaryEntry entry in dictionary)
				{
					var key = entry.Key.SerializeValue()!;
					AddValue(node.GetOrAddChild(key), entry.Value);
				}

				break;
			}
			// e.g. ExpandoObject, which does not implement the non-generic IDictionary
			case IEnumerable<KeyValuePair<string, object?>> keyValuePairs:
			{
				foreach (var (key, item) in keyValuePairs)
					AddValue(node.GetOrAddChild(key), item);

				break;
			}
			case IEnumerable enumerable:
			{
				var i = 0;
				foreach (var item in enumerable)
				{
					var key = i++.ToString(CultureInfo.InvariantCulture);
					AddValue(node.GetOrAddChild(key), item);
				}

				break;
			}
			default:
			{
				var props = PropertyMap.GetOrAdd(value.GetType(), static type => type
					.GetProperties(BindingFlags.Public | BindingFlags.Instance)
					.Where(static x => x.CanRead && x.GetIndexParameters().Length == 0)
					.ToArray());

				foreach (var prop in props)
					AddValue(node.GetOrAddChild(prop.Name), prop.GetValue(value));

				break;
			}
		}
	}

	private static bool IsDictionary(object value) =>
		value is IDictionary or IEnumerable<KeyValuePair<string, object?>>;
}
