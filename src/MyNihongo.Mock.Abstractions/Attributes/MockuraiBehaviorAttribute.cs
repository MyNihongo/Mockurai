namespace MyNihongo.Mock;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class MockuraiBehaviorAttribute : Attribute
{
	public bool SkipVerifyNoOtherCalls { get; set; }
}
