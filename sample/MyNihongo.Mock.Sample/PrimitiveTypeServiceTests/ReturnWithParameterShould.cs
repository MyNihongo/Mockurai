namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithParameterShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const string paramCustomerId = "qOp5SNt2LVqzQ9qS",
			expected = "name:,age:32";

		var actual = CreateFixture()
			.ReturnWithParameter(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			nameSetup = "Okayama Issei",
			expected = "name:Okayama Issei,age:32";

		DependencyServiceMock
			.SetupReturnWithParameter(parameter)
			.Returns(nameSetup);

		var actual = CreateFixture()
			.ReturnWithParameter(parameter);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithAnotherSetup()
	{
		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID",
			setupName = "Okayama Issei",
			expected = "name:,age:32";

		DependencyServiceMock
			.SetupReturnWithParameter(setupCustomerId)
			.Returns(setupName);

		var actual = CreateFixture()
			.ReturnWithParameter(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithParameter(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueForThrowsWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID",
			expected = "name:,age:32";

		DependencyServiceMock
			.SetupReturnWithParameter(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithParameter(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(parameter1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify1 = It<string>.Where(x => x.EndsWith('1'));

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(verify1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify = It<string>.Any();

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		var actual = () =>
		{
			var verify = It<string>.Any();
			DependencyServiceMock.VerifyReturnWithParameter(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#ReturnWithParameter(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: "parameter1"
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#ReturnWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
