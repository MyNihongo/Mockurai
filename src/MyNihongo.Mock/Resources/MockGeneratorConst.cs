namespace MyNihongo.Mock.Resources;

internal static class MockGeneratorConst
{
	public const string GenerateAttributeName = "MockuraiGenerate";
	public const string BehavriorAttribute = "MockuraiBehaviorAttribute";
	public const string SkipVerifyNoOtherCallsPropertyName = "SkipVerifyNoOtherCalls";
	public const string Namespace = "MyNihongo.Mock"; // TODO change to Mockurai

	public static class Suffixes
	{
		public const string Invocation = "Invocation";
		public const string ValueSetup = ".ValueSetup";
		public const string Prefix = "prefix";
		public const string GenericParameter = "T", GenericReturnParameter = "TReturns";
		public const string MockVariableCall = "_mock.";
	}
}
