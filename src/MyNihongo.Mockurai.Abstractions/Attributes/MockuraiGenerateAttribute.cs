namespace MyNihongo.Mockurai;

/// <summary>
/// Marks a class as a target for Mockurai source generation, producing mock implementations for its members.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class MockuraiGenerateAttribute : Attribute
{
	/// <summary>
	/// Gets or sets the name of the method to invoke before <c>VerifyNoOtherCalls()</c>.
	/// </summary>
	/// <remarks>
	/// The referenced method may declare any parameters, but must return <c>void</c>.
	/// </remarks>
	public string? BeforeVerifyNoOtherCalls { get; set; }
}
