namespace MyNihongo.Mockurai.Abstractions.Tests.Comparison.EquivalencyComparerTests;

public sealed class EquivalentLookupShould
{
	[Fact]
	public void BeEmptyIfEquivalent()
	{
		const int number1 = 1, number2 = 2;
		const string text1 = "text1", text2 = "text2";

		ITestLookup<int, string> input1 = new TestLookup<int, string>
		{
			{ number1, text1 },
			{ number2, text2 },
		};
		var input2 = new TestLookup<int, string>
		{
			{ number1, text1 },
			{ number2, text2 },
		};

		var actual = EquivalencyComparer<ITestLookup<int, string>>.Default.Equivalent(input1, input2);
		Assert.Empty(actual.Entries);
	}

	[Fact]
	public void ReturnResultIfNotEquivalent()
	{
		const int number1 = 1, number2 = 2;
		const string text1 = "text1", text2 = "text2";

		ITestLookup<int, string> input1 = new TestLookup<int, string>
		{
			{ number1, text1 + "a" },
			{ number2, text2 },
		};
		var input2 = new TestLookup<int, string>
		{
			{ number1, text1 },
			{ number2 + 1, text2 },
		};

		var actual = EquivalencyComparer<ITestLookup<int, string>>.Default.Equivalent(input1, input2);

		var expected = new ComparisonResult.Entry[]
		{
			new("this[1].Key", "2", "3"),
			new("this[0].Value[0]", "\"text1a\"", "\"text1\""),
		};
		Assert.Equivalent(expected, actual.Entries, true);
	}

	private interface ITestLookup<TKey, TItem> : IEnumerable<KeyValuePair<TKey, HashSet<TItem>>>
	{
		int Count { get; }
	}

	private sealed class TestLookup<TKey, TItem> : ITestLookup<TKey, TItem>
		where TKey : notnull
	{
		private readonly Dictionary<TKey, HashSet<TItem>> _container = new();

		public int Count => _container.Count;

		public void Add(TKey key, TItem item)
		{
			ref var hashSet = ref CollectionsMarshal.GetValueRefOrAddDefault(_container, key, out _);
			hashSet ??= [];

			hashSet.Add(item);
		}

		public IEnumerator<KeyValuePair<TKey, HashSet<TItem>>> GetEnumerator() =>
			((IEnumerable<KeyValuePair<TKey, HashSet<TItem>>>)_container).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();
	}
}
