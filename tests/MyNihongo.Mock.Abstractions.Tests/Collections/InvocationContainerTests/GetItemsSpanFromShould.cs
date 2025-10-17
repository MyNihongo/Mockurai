namespace MyNihongo.Mock.Abstractions.Tests.Collections.InvocationContainerTests;

public sealed class GetItemsSpanFromShould : InvocationContainerTestsBase
{
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	public void ReturnEntireCollectionFromStart(int index)
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 2 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.GetItemsSpanFrom(index)
			.ToArray();

		var expected = new Invocation[]
		{
			new() { Index = 2 },
			new() { Index = 3 },
			new() { Index = 7 },
			new() { Index = 10 },
		};
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void ReturnSliceFromMiddle()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 2 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.GetItemsSpanFrom(index: 4)
			.ToArray();

		var expected = new Invocation[]
		{
			new() { Index = 7 },
			new() { Index = 10 },
		};
		Assert.Equivalent(expected, actual);
	}

	[Theory]
	[InlineData(8)]
	[InlineData(9)]
	[InlineData(10)]
	public void ReturnLastElement(int index)
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 1 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.GetItemsSpanFrom(index)
			.ToArray();

		var expected = new Invocation[]
		{
			new() { Index = 10 },
		};
		Assert.Equivalent(expected, actual);
	}
}
