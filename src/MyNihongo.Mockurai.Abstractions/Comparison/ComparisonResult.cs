namespace MyNihongo.Mockurai;

/// <summary>
/// Represents the outcome of a structural comparison, accumulating any differences found between two object graphs.
/// </summary>
public sealed class ComparisonResult
{
	/// <summary>
	/// The path used to identify the root of the compared object graph.
	/// </summary>
	public const string RootPath = "this";
	private readonly List<Entry> _entries = [];

	/// <summary>
	/// Gets the collection of recorded differences between the compared values.
	/// </summary>
	public IReadOnlyList<Entry> Entries => _entries;

	/// <summary>
	/// Records a difference at the given member path.
	/// </summary>
	/// <param name="path">The dotted member path where the difference was detected.</param>
	/// <param name="expectedValue">The expected value, or <see langword="null"/> if absent.</param>
	/// <param name="actualValue">The actual value, or <see langword="null"/> if absent.</param>
	public void Add(in string path, in string? expectedValue, in string? actualValue)
	{
		var entry = new Entry(path, expectedValue, actualValue);
		_entries.Add(entry);
	}

	/// <summary>
	/// Implicitly converts the result to <see langword="true"/> when no differences have been recorded.
	/// </summary>
	/// <param name="this">The result instance to evaluate.</param>
	public static implicit operator bool(ComparisonResult? @this)
	{
		return @this is not null && @this._entries.Count == 0;
	}

	/// <summary>
	/// A single recorded difference between two compared values.
	/// </summary>
	/// <param name="path">The dotted member path where the difference was detected.</param>
	/// <param name="expectedValue">The expected value, or <see langword="null"/> if absent.</param>
	/// <param name="actualValue">The actual value, or <see langword="null"/> if absent.</param>
	public sealed class Entry(in string path, in string? expectedValue, in string? actualValue)
	{
		/// <summary>
		/// The dotted member path where the difference was detected.
		/// </summary>
		public readonly string Path = path;

		/// <summary>
		/// The expected value, or an empty string if it was <see langword="null"/>.
		/// </summary>
		public readonly string ExpectedValue = expectedValue ?? string.Empty;

		/// <summary>
		/// The actual value, or an empty string if it was <see langword="null"/>.
		/// </summary>
		public readonly string ActualValue = actualValue ?? string.Empty;
	}
}
