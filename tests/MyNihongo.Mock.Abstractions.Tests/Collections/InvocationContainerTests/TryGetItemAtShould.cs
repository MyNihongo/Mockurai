namespace MyNihongo.Mock.Abstractions.Tests.Collections.InvocationContainerTests;

public sealed class TryGetItemAtShould : InvocationContainerTestsBase
{
	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	public void ReturnNextElementStart(int index)
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 2 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.TryGetItemAt(index);

		var expected = new Invocation { Index = 2 };
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void ReturnNextElementMiddle()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 1 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.TryGetItemAt(index: 4);

		var expected = new Invocation { Index = 7 };
		Assert.Equivalent(expected, actual);
	}

	[Theory]
	[InlineData(8)]
	[InlineData(9)]
	[InlineData(10)]
	public void ReturnNextElementEnd(int index)
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 1 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.TryGetItemAt(index);

		var expected = new Invocation { Index = 10 };
		Assert.Equivalent(expected, actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnNullIfNotFound(int index)
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 1 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.TryGetItemAt(index);
		Assert.Null(actual);
	}
}
