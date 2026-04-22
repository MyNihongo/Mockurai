namespace MyNihongo.Mockurai.Sample.ClassTypeServiceTests;

public sealed class InvokeWithOneParameterShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithJapaneseCharacters()
	{
		const int number = 1;
		const string name = "岡山壱成";

		var fixture = CreateFixture();
		fixture.InvokeWithOneParameter(new ClassParameter1
		{
			Number = number,
			Text = name,
		});

		var actual = () =>
		{
			var verify = ItIn<ClassParameter1>.Value(new ClassParameter1
			{
				Number = number,
				Text = "岡山一成",
			});
			ClassDependencyServiceMock.VerifyInvokeWithOneParameter(verify, Times.Once);
		};

		const string expectedMessage =
			"""
			Expected IClassDependencyService.InvokeWithOneParameter(in {"Text":"岡山一成","Number":1}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: IClassDependencyService.InvokeWithOneParameter(in {"Text":"岡山壱成","Number":1})
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithJapaneseCharactersEquivalent()
	{
		const int number = 1;
		const string name = "岡山壱成";

		var fixture = CreateFixture();
		fixture.InvokeWithOneParameter(new ClassParameter1
		{
			Number = number,
			Text = name,
		});

		var actual = () =>
		{
			var verify = ItIn<ClassParameter1>.Equivalent(new ClassParameter1
			{
				Number = number,
				Text = "岡山一成",
			});
			ClassDependencyServiceMock.VerifyInvokeWithOneParameter(verify, Times.Once);
		};

		const string expectedMessage =
			"""
			Expected IClassDependencyService.InvokeWithOneParameter(in {"Text":"岡山一成","Number":1}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: IClassDependencyService.InvokeWithOneParameter(in {"Text":"岡山壱成","Number":1})
			  - Text:
			    expected: 岡山一成
			    actual: 岡山壱成
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
