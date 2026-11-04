using System.Collections.Concurrent;
using System.Reflection;

namespace MyNihongo.Mockurai;

internal sealed class SectionInfoProvider
{
	private readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyMap = new();

	public IReadOnlyList<SectionInfo> Resolve(Type type, object obj)
	{
		if (obj is IDictionary<string, object> dictionary)
		{
			var sectionInfos = new SectionInfo[dictionary.Count];

			var i = 0;
			foreach (var keyValue in dictionary)
				sectionInfos[i++] = new SectionInfo(keyValue.Key, keyValue.Value, keyValue.Value.GetType());

			return sectionInfos;
		}
		else
		{
			var props = _propertyMap
				.GetOrAdd(type, static x => x.GetProperties());

			var sectionInfos = new SectionInfo[props.Length];

			for (var i = 0; i < props.Length; i++)
				sectionInfos[i] = new SectionInfo(props[i].Name, props[i].GetValue(obj), props[i].PropertyType);

			return sectionInfos;
		}
	}
}
