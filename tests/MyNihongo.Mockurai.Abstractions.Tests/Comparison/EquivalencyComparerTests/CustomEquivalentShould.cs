using System.Text.Json;

namespace MyNihongo.Mockurai.Abstractions.Tests.Comparison.EquivalencyComparerTests;

public sealed class CustomEquivalentShould
{
	[Fact]
	public void BeEmptyForEqualObjects()
	{
		var input1 = new { prop = "aaa" }.ToJsonElement();
		var input2 = new { prop = "aaa" }.ToJsonElement();

		It<JsonElement>.Equivalent(input1, new JsonElementComparer())
			.ValueSetup.Check(input2, out var actual);

		Assert.NotNull(actual);
		Assert.Empty(actual.Entries);
	}
}

file sealed class JsonElementComparer : IEquivalencyComparer<JsonElement>
{
	public ComparisonResult Equivalent(JsonElement x, JsonElement y)
	{
		var result = new ComparisonResult();
		throw new NotImplementedException();
	}
}

file static class Extensions
{
	public static JsonElement ToJsonElement<T>(this T @this)
	{
		return JsonSerializer.SerializeToElement(@this);
	}
}
