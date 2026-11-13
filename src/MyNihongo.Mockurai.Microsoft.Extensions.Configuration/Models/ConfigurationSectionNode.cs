using Microsoft.Extensions.Primitives;

namespace MyNihongo.Mockurai;

internal sealed class ConfigurationSectionNode : IConfigurationSection
{
	// Configuration keys are case-insensitive
	private readonly Dictionary<string, ConfigurationSectionNode> _childMap = new(StringComparer.OrdinalIgnoreCase);
	private readonly List<ConfigurationSectionNode> _children = [];

	private ConfigurationSectionNode(string key, string path)
	{
		Key = key;
		Path = path;
	}

	public string Key { get; }

	public string Path { get; }

	public string? Value { get; set; }

	public string? this[string key]
	{
		get => Find(key)?.Value;
		set => GetOrAdd(key).Value = value;
	}

	public static ConfigurationSectionNode CreateRoot() =>
		new(string.Empty, string.Empty);

	public IConfigurationSection GetSection(string key)
	{
		if (Find(key) is { } node)
			return node;

		// Real configuration never returns null, but an empty section instead
		var path = CombinePath(key);
		return new ConfigurationSectionNode(ConfigurationPath.GetSectionKey(path), path);
	}

	public IEnumerable<IConfigurationSection> GetChildren() =>
		_children.ToArray();

	public IChangeToken GetReloadToken() =>
		NullChangeToken.Instance;

	public ConfigurationSectionNode GetOrAddChild(string key)
	{
		if (_childMap.TryGetValue(key, out var child))
			return child;

		child = new ConfigurationSectionNode(key, CombinePath(key));
		_childMap.Add(key, child);
		_children.Add(child);

		return child;
	}

	private ConfigurationSectionNode? Find(string path)
	{
		var node = this;
		foreach (var key in path.Split(ConfigurationPath.KeyDelimiter))
			if (!node._childMap.TryGetValue(key, out node))
				return null;

		return node;
	}

	private ConfigurationSectionNode GetOrAdd(string path)
	{
		var node = this;
		foreach (var key in path.Split(ConfigurationPath.KeyDelimiter))
			node = node.GetOrAddChild(key);

		return node;
	}

	private string CombinePath(string key) =>
		string.IsNullOrEmpty(Path)
			? key
			: ConfigurationPath.Combine(Path, key);
}
