namespace MyNihongo.Mock.Tests;

public abstract class TestsBase
{
	protected static CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier> CreateFixture(string testCode, (string, string)[] expected)
	{
		return new CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier>
		{
			ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
			TestBehaviors = TestBehaviors.SkipGeneratedCodeCheck | TestBehaviors.SkipSuppressionCheck | TestBehaviors.SkipGeneratedSourcesCheck,
			TestCode = testCode,
			TestState =
			{
				AdditionalReferences = { typeof(MockuraiGenerateAttribute).Assembly },
				GeneratedSources =
				{
					(typeof(SourceGenerator), "_Usings.g.cs", "global using MyNihongo.Mock;"),
					expected,
				},
			},
		};
	}
}

file static class SourceFileCollectionEx
{
	public static void Add(this SourceFileCollection @this, (string, string)[] expected)
	{
		foreach (var item in expected)
		{
			@this.Add((typeof(SourceGenerator), item.Item1, item.Item2));
		}
	}
}
