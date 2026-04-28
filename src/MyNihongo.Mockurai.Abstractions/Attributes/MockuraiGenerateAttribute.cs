namespace MyNihongo.Mockurai;

/// <summary>
/// Marks a class as a target for Mockurai source generation, producing mock implementations for its members.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class MockuraiGenerateAttribute : Attribute;
