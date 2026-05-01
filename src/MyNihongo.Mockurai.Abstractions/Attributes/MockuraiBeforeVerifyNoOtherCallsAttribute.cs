namespace MyNihongo.Mockurai;

/// <summary>
/// Marks a method to be invoked by the generated mock immediately before <c>VerifyNoOtherCalls</c> runs,
/// allowing custom setup or verification logic to execute as part of the call.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class MockuraiBeforeVerifyNoOtherCallsAttribute : Attribute;
