using System.Collections.Frozen;
using System.Reflection;

namespace MyNihongo.Mock;

public sealed class EquivalencyComparer<T>
{
	public static readonly EquivalencyComparer<T> Default = new();
	private readonly FrozenDictionary<string, PropertyInfo> _properties;
	private readonly FrozenDictionary<string, FieldInfo> _fields;

	private EquivalencyComparer()
	{
		_properties = typeof(T)
			.GetProperties(BindingFlags.Instance | BindingFlags.Public)
			.ToFrozenDictionary(static x => x.Name);

		_fields = typeof(T)
			.GetFields(BindingFlags.Instance | BindingFlags.Public)
			.ToFrozenDictionary(static x => x.Name);
	}

	public bool Equivalent(T? x, T? y)
	{
		return false;
	}
}
