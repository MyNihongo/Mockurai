namespace MyNihongo.Mockurai.Tests.Utils.ClassInheritanceTreeTests;

public sealed class ToArrayShould : ClassInheritanceTreeTestsBase
{
	[Fact]
	public void ReturnTwoRootNodes()
	{
		const string input =
			"""
			public class Class1 {}
			public class Class2 {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class1", "Class2"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnParentChild()
	{
		const string input =
			"""
			public class Class1 : Class2 {}
			public class Class2 {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class2", "Class1"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnParentChildren()
	{
		const string input =
			"""
			public class Class1 : Class2 {}
			public class Class2 {}
			public class Class3 : Class2 {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class2", "Class1", "Class3"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnParentChildChild()
	{
		const string input =
			"""
			public class Class1 : Class3 {}
			public class Class2 {}
			public class Class3 : Class2 {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class2", "Class3", "Class1"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnRootWhenParentMissingFromInput()
	{
		const string input =
			"""
			public class Parent {}
			public class Class1 : Parent {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class1"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnRootDerived()
	{
		const string input =
			"""
			public class Parent
			public class Class1 : Class2 {}
			public class Class2 {}
			public class Class3 : Parent {}
			public class Class4 {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class2", "Class4", "Class3", "Class1"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnEmpty()
	{
		const string input = "";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		Assert.Empty(actual);
	}

	[Fact]
	public void ReturnSingleClass()
	{
		const string input = "public class Class1 {}";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class1"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnDeepChain()
	{
		const string input =
			"""
			public class Class1 : Class2 {}
			public class Class2 : Class3 {}
			public class Class3 : Class4 {}
			public class Class4 {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class4", "Class3", "Class2", "Class1"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnMultipleRootsMultiLevel()
	{
		const string input =
			"""
			public class Class1 {}
			public class Class2 : Class1 {}
			public class Class3 : Class1 {}
			public class Class4 : Class2 {}
			public class Class5 : Class3 {}
			public class Class6 {}
			public class Class7 : Class6 {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class1", "Class6", "Class2", "Class3", "Class7", "Class4", "Class5"];
		Assert.Equivalent(expected, actual, true);
	}

	[Fact]
	public void ReturnMixedDeclarationOrder()
	{
		const string input =
			"""
			public class Class1 : Class3 {}
			public class Class2 : Class3 {}
			public class Class3 {}
			public class Class4 : Class1 {}
			public class Class5 : Class2 {}
			""";

		var actual = CreateFixture(input, TestContext.Current.CancellationToken)
			.ToArray();

		string[] expected = ["Class3", "Class1", "Class2", "Class4", "Class5"];
		Assert.Equivalent(expected, actual, true);
	}
}
