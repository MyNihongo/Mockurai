namespace MyNihongo.Mockurai.Sample.ClassTypeServiceTests;

public sealed class InvokeWithSeveralParametersShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ThrowWithJapaneseCharacters()
	{
		const int number = 1;
		const string name = "岡山壱成";

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters(
			parameter1: new ClassParameter1
			{
				Number = number,
				Text = name,
			},
			parameter2: new ClassParameter1
			{
				Number = number,
				Text = name,
			}
		);

		var actual = () =>
		{
			var verify = ItIn<ClassParameter1>.Value(new ClassParameter1
			{
				Number = number,
				Text = "岡山一成",
			});
			ClassDependencyServiceMock.VerifyInvokeWithSeveralParameters(verify, verify, Times.Once);
		};

		const string expectedMessage =
			"""
			Expected IClassDependencyService.InvokeWithSeveralParameters(in {"Text":"岡山一成","Number":1}, in {"Text":"岡山一成","Number":1}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: IClassDependencyService.InvokeWithSeveralParameters(in {"Text":"岡山壱成","Number":1}, in {"Text":"岡山壱成","Number":1})
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
		fixture.InvokeWithSeveralParameters(
			parameter1: new ClassParameter1
			{
				Number = number,
				Text = name,
			},
			parameter2: new ClassParameter1
			{
				Number = number,
				Text = name,
			}
		);

		var actual = () =>
		{
			var verify = ItIn<ClassParameter1>.Equivalent(new ClassParameter1
			{
				Number = number,
				Text = "岡山一成",
			});
			ClassDependencyServiceMock.VerifyInvokeWithSeveralParameters(verify, verify, Times.Once);
		};

		const string expectedMessage =
			"""
			Expected IClassDependencyService.InvokeWithSeveralParameters(in {"Text":"岡山一成","Number":1}, in {"Text":"岡山一成","Number":1}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: IClassDependencyService.InvokeWithSeveralParameters(in {"Text":"岡山壱成","Number":1}, in {"Text":"岡山壱成","Number":1})
			  - parameter1:
			    - Text:
			      expected: 岡山一成
			      actual: 岡山壱成
			  - parameter2:
			    - Text:
			      expected: 岡山一成
			      actual: 岡山壱成
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
