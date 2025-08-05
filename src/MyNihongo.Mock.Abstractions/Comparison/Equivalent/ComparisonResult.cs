namespace MyNihongo.Mock;

public sealed class ComparisonResult
{
	private readonly List<Entry> _entries = [];

	public IReadOnlyList<Entry> Entries => _entries;

	public void Add(in string path, in string? expectedValue, in string? actualValue)
	{
		var entry = new Entry(path, expectedValue, actualValue);
		_entries.Add(entry);
	}

	public static implicit operator bool(ComparisonResult @this)
	{
		return @this._entries.Count == 0;
	}

	public sealed class Entry(in string path, in string? expectedValue, in string? actualValue)
	{
		public readonly string Path = path, ExpectedValue = expectedValue ?? string.Empty, ActualValue = actualValue ?? string.Empty;
	}
}
