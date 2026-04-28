namespace MyNihongo.Mockurai;

/// <summary>
/// Configures the behavior of the generated mock for the decorated property or field.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class MockuraiBehaviorAttribute : Attribute
{
	/// <summary>
	/// Gets or sets a value indicating whether the generated mock should skip verifying that no other calls were made.
	/// </summary>
	public bool SkipVerifyNoOtherCalls { get; set; }
}
