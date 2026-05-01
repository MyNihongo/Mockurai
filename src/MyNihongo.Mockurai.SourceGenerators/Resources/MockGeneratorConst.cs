namespace MyNihongo.Mockurai.Resources;

internal static class MockGeneratorConst
{
	public const string GenerateAttributeName = "MockuraiGenerate";
	public const string BehaviourAttributeName = "MockuraiBehavior";
	public const string BehaviourAttribute = $"{BehaviourAttributeName}Attribute";
	public const string BeforeVerifyNoOtherCallsAttributeName = "MockuraiBeforeVerifyNoOtherCalls";
	public const string BeforeVerifyNoOtherCallsAttribute = $"{BeforeVerifyNoOtherCallsAttributeName}Attribute";
	public const string SkipVerifyNoOtherCallsPropertyName = "SkipVerifyNoOtherCalls";
	public const string Namespace = "MyNihongo.Mockurai";

	public static class Variables
	{
		public const string ReturnValue = "returnValue";
		public const string Parameter = "parameter";
	}

	public static class Suffixes
	{
		public const string Indexer = "Indexer";
		public const string Invocation = "Invocation";
		public const string ValueSetup = ".ValueSetup";
		public const string Prefix = "prefix";
		public const string GenericParameter = "T", GenericReturnParameter = "TReturns";
		public const string MockVariableCall = "_mock.";
		public const string DefaultAssign = " = default!;";
	}
}
