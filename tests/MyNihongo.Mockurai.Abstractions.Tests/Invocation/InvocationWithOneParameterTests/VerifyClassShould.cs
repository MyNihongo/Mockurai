namespace MyNihongo.Mockurai.Abstractions.Tests.Invocation.InvocationWithOneParameterTests;

public sealed class VerifyClassShould : InvocationWithOneParameterTestsBase
{
	[Fact]
	public void VerifyEquivalent()
	{
		const int number = 1;
		const string name = "Okayama Issei";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<ClassParameter1>();
		fixture.Register(index, new ClassParameter1
		{
			Number = number,
			Text = name,
		});
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 1,
			Text = name + '2',
		});

		var verify = It<ClassParameter1>.Equivalent(new ClassParameter1
		{
			Number = number,
			Text = name,
		});
		const int expected = 1;
		fixture.Verify(verify.ValueSetup, Times.Exactly(expected));
	}

	[Fact]
	public void ThrowVerifyNotEquivalent()
	{
		const int number = 1;
		const string name = "Okayama Issei";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<ClassParameter1>();
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 12345,
			Text = name + " another name",
		});
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 1,
			Text = name + '2',
		});

		var actual = () =>
		{
			var verify = It<ClassParameter1>.Equivalent(new ClassParameter1
			{
				Number = number,
				Text = name,
			});
			fixture.Verify(verify.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod({"Text":"Okayama Issei","Number":1}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod({"Text":"Okayama Issei another name","Number":12346})
			  - Text:
			    expected: "Okayama Issei"
			    actual: "Okayama Issei another name"
			  - Number:
			    expected: 1
			    actual: 12346
			- 2: MyClass.MyMethod({"Text":"Okayama Issei2","Number":2})
			  - Text:
			    expected: "Okayama Issei"
			    actual: "Okayama Issei2"
			  - Number:
			    expected: 1
			    actual: 2
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentOneMatched()
	{
		const int number = 1;
		const string name = "Okayama Issei";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<ClassParameter1>();
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 12345,
			Text = name,
		});
		fixture.Register(index, new ClassParameter1
		{
			Number = number,
			Text = name,
		});
		fixture.Register(index, new ClassParameter1
		{
			Number = number,
			Text = name + '2',
		});

		var actual = () =>
		{
			var verify = It<ClassParameter1>.Equivalent(new ClassParameter1
			{
				Number = number,
				Text = name,
			});
			const int expected = 2;
			fixture.Verify(verify.ValueSetup, Times.Exactly(expected));
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod({"Text":"Okayama Issei","Number":1}) to be called 2 times, but instead it was called 1 time.
			Performed invocations:
			- 1: MyClass.MyMethod({"Text":"Okayama Issei","Number":12346})
			  - Number:
			    expected: 1
			    actual: 12346
			- 2: MyClass.MyMethod({"Text":"Okayama Issei","Number":1})
			- 3: MyClass.MyMethod({"Text":"Okayama Issei2","Number":1})
			  - Text:
			    expected: "Okayama Issei"
			    actual: "Okayama Issei2"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyEquivalentIndex()
	{
		const int number = 1;
		const string name = "Okayama Issei";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<ClassParameter1>();
		fixture.Register(index, new ClassParameter1
		{
			Number = number,
			Text = name,
		});
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 1,
			Text = name + '2',
		});

		var verify = It<ClassParameter1>.Equivalent(new ClassParameter1
		{
			Number = number,
			Text = name,
		});
		const long expected = 1L;
		fixture.Verify(verify.ValueSetup, expected);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentIndex()
	{
		const int number = 1;
		const string name = "Okayama Issei";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<ClassParameter1>();
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 12345,
			Text = name + " another name",
		});
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 1,
			Text = name + '2',
		});

		var actual = () =>
		{
			var verify = It<ClassParameter1>.Equivalent(new ClassParameter1
			{
				Number = number,
				Text = name,
			});
			const long expected = 1L;
			fixture.Verify(verify.ValueSetup, expected);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod({"Text":"Okayama Issei","Number":1}) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod({"Text":"Okayama Issei another name","Number":12346})
			  - Text:
			    expected: "Okayama Issei"
			    actual: "Okayama Issei another name"
			  - Number:
			    expected: 1
			    actual: 12346
			- 2: MyClass.MyMethod({"Text":"Okayama Issei2","Number":2})
			  - Text:
			    expected: "Okayama Issei"
			    actual: "Okayama Issei2"
			  - Number:
			    expected: 1
			    actual: 2
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentOneMatchedIndex()
	{
		const int number = 1;
		const string name = "Okayama Issei";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<ClassParameter1>();
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 12345,
			Text = name,
		});
		fixture.Register(index, new ClassParameter1
		{
			Number = number,
			Text = name,
		});
		fixture.Register(index, new ClassParameter1
		{
			Number = number,
			Text = name + '2',
		});

		var actual = () =>
		{
			var verify = It<ClassParameter1>.Equivalent(new ClassParameter1
			{
				Number = number,
				Text = name,
			});
			const long expected = 3L;
			fixture.Verify(verify.ValueSetup, expected);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod({"Text":"Okayama Issei","Number":1}) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod({"Text":"Okayama Issei","Number":12346})
			- 2: MyClass.MyMethod({"Text":"Okayama Issei","Number":1})
			- 3: MyClass.MyMethod({"Text":"Okayama Issei2","Number":1})
			  - Text:
			    expected: "Okayama Issei"
			    actual: "Okayama Issei2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithJapaneseCharacters()
	{
		const int number = 1;
		const string name = "岡山壱成";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<ClassParameter1>();
		fixture.Register(index, new ClassParameter1
		{
			Number = number + 12345,
			Text = name,
		});

		var actual = () =>
		{
			var verify = It<ClassParameter1>.Value(new ClassParameter1
			{
				Number = number,
				Text = "岡山一成",
			});
			fixture.Verify(verify.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod({"Text":"岡山一成","Number":1}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod({"Text":"岡山壱成","Number":12346})
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithJapaneseCharactersEquivalent()
	{
		const int number = 1;
		const string name = "岡山壱成";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<ClassParameter1>();
		fixture.Register(index, new ClassParameter1
		{
			Number = number,
			Text = name,
		});

		var actual = () =>
		{
			var verify = It<ClassParameter1>.Equivalent(new ClassParameter1
			{
				Number = number,
				Text = "岡山一成",
			});
			fixture.Verify(verify.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod({"Text":"岡山一成","Number":1}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod({"Text":"岡山壱成","Number":1})
			  - Text:
			    expected: "岡山一成"
			    actual: "岡山壱成"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
