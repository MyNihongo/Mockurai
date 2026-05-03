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
}
