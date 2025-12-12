namespace MyNihongo.Mock.Tests;

public abstract class TestsBase
{
	protected static CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier> CreateFixture(string testCode, GeneratedSources expected)
	{
		return new CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier>
		{
			ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
			TestCode = testCode,
			TestState =
			{
				AdditionalReferences = { typeof(MockuraiGenerateAttribute).Assembly },
				GeneratedSources =
				{
					expected,
					(typeof(SourceGenerator), "_Usings.g.cs", "global using MyNihongo.Mock;"),
				},
			},
		};
	}
}

file static class SourceFileCollectionEx
{
	public static void Add(this SourceFileCollection @this, GeneratedSources expected)
	{
		foreach (var item in expected)
		{
			@this.Add((typeof(SourceGenerator), item.FileName, item.SourceCode));
		}
	}
}
