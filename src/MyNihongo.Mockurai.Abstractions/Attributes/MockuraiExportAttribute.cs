namespace MyNihongo.Mockurai;

/// <summary>
/// Exports a public mock implementation of <see cref="Type"/> from the current assembly.
/// Intended for libraries that build extensions on top of Mockurai, so that the same mock implementation is shared with the consuming assemblies.
/// Consuming assemblies reuse the exported mock instead of generating their own one.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class MockuraiExportAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MockuraiExportAttribute"/> class.
	/// </summary>
	/// <param name="type">The type which mock implementation is exported.</param>
	public MockuraiExportAttribute(Type type)
	{
		Type = type;
	}

	/// <summary>
	/// Gets the type which mock implementation is exported.
	/// </summary>
	public Type Type { get; }
}
