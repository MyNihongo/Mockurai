using System.Text.Json;

namespace MyNihongo.Mockurai.Abstractions.Tests.Comparison.EquivalencyComparerTests;

public sealed class EquivalentJsonElementShould
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

	[Fact]
	public void BeEmptyForEqualArrays()
	{
		var input1 = new[] { new { prop = "aaa" } }.ToJsonElement();
		var input2 = new[] { new { prop = "aaa" } }.ToJsonElement();

		It<JsonElement>.Equivalent(input1, new JsonElementComparer())
			.ValueSetup.Check(input2, out var actual);

		Assert.NotNull(actual);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void ReturnResultIfValueNotSame()
	{
		var input1 = new { prop = "aaa1" }.ToJsonElement();
		var input2 = new { prop = "aaa2" }.ToJsonElement();

		It<JsonElement>.Equivalent(input1, new JsonElementComparer())
			.ValueSetup.Check(input2, out var actual);

		var expected = new ComparisonResult.Entry[]
		{
			new("prop", "aaa1", "aaa2"),
		};

		Assert.NotNull(actual);
		Assert.Equivalent(expected, actual.Entries);
	}

	[Fact]
	public void ReturnResultIfPropertyNotSame()
	{
		var input1 = new { prop1 = "aaa" }.ToJsonElement();
		var input2 = new { prop2 = "aaa" }.ToJsonElement();

		It<JsonElement>.Equivalent(input1, new JsonElementComparer())
			.ValueSetup.Check(input2, out var actual);

		var expected = new ComparisonResult.Entry[]
		{
			new("prop1", "aaa", "not found"),
			new("prop2", "not found", "aaa"),
		};

		Assert.NotNull(actual);
		Assert.Equivalent(expected, actual.Entries);
	}

	[Fact]
	public void ReturnResultIfObjectNotSame()
	{
		var input1 = new { prop = "aaa" }.ToJsonElement();
		var input2 = new[] { new { prop = "aaa" } }.ToJsonElement();

		It<JsonElement>.Equivalent(input1, new JsonElementComparer())
			.ValueSetup.Check(input2, out var actual);

		var expected = new ComparisonResult.Entry[]
		{
			new("this", """{"prop":"aaa"}""", """[{"prop":"aaa"}]"""),
		};

		Assert.NotNull(actual);
		Assert.Equivalent(expected, actual.Entries);
	}
}

file sealed class JsonElementComparer : IEquivalencyComparer<JsonElement>
{
	public ComparisonResult Equivalent(JsonElement x, JsonElement y)
	{
		var result = new ComparisonResult();
		return Equivalent(x, y, result, path: null);
	}

	private static ComparisonResult Equivalent(in JsonElement x, in JsonElement y, in ComparisonResult result, in string? path)
	{
		if (x.ValueKind != y.ValueKind)
		{
			var thisPath = GetPropertyPathOrRoot(path);
			result.Add(thisPath, x.ToString(), y.ToString());
			return result;
		}

		switch (x.ValueKind)
		{
			case JsonValueKind.Object:
				CompareObjects(x, y, result, path);
				break;
			case JsonValueKind.Array:
				CompareArrays(x, y, result, path);
				break;
			default:
				var xString = x.ToString();
				var yString = y.ToString();
				if (!string.Equals(xString, yString, StringComparison.Ordinal))
				{
					var thisPath = GetPropertyPathOrRoot(path);
					result.Add(thisPath, xString, yString);
				}

				break;
		}

		return result;
	}

	private static void CompareObjects(in JsonElement x, in JsonElement y, ComparisonResult result, string? path)
	{
		var yProps = y.EnumerateObject().ToDictionary(static x => x.Name, static x => x.Value);
		var xNames = new HashSet<string>();

		foreach (var xProp in x.EnumerateObject())
		{
			xNames.Add(xProp.Name);
			var propPath = BuildPath(path, xProp.Name);

			if (yProps.TryGetValue(xProp.Name, out var yValue))
				Equivalent(xProp.Value, yValue, result, propPath);
			else
				result.Add(propPath, xProp.Value.ToString(), "not found");
		}

		foreach (var yProp in yProps.Where(p => !xNames.Contains(p.Key)))
			result.Add(BuildPath(path, yProp.Key), "not found", yProp.Value.ToString());
	}

	private static void CompareArrays(in JsonElement x, in JsonElement y, ComparisonResult result, string? path)
	{
		var xItems = x.EnumerateArray().ToArray();
		var yItems = y.EnumerateArray().ToArray();

		var len = Math.Max(xItems.Length, yItems.Length);

		for (var i = 0; i < len; i++)
		{
			var indexPath = BuildPath(path, i);

			if (i >= xItems.Length)
				result.Add(indexPath, null, yItems[i].ToString());
			else if (i >= yItems.Length)
				result.Add(indexPath, xItems[i].ToString(), null);
			else
				Equivalent(xItems[i], yItems[i], result, indexPath);
		}
	}

	private static string BuildPath(string? parent, string property)
	{
		return !string.IsNullOrEmpty(parent)
			? $"{parent}.{property}"
			: property;
	}

	private static string BuildPath(string? parent, int index)
	{
		var thisPath = GetPropertyPathOrRoot(parent);
		return $"{thisPath}[{index}]";
	}

	private static string GetPropertyPathOrRoot(string? path)
	{
		return string.IsNullOrEmpty(path)
			? ComparisonResult.RootPath
			: path;
	}
}

file static class Extensions
{
	public static JsonElement ToJsonElement<T>(this T @this)
	{
		return JsonSerializer.SerializeToElement(@this);
	}
}
