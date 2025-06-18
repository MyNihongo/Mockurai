namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public sealed class InvokeWithMultipleParametersShould : ClassTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		CreateFixture()
			.InvokeWithMultipleParameters(input1, input2);
	}

	[Fact]
	public void ThrowCustomException()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithMultipleParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithMultipleParameters(input1, input2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnNullThrowsWithInvalidSequence1()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithMultipleParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(input1, input1);
	}

	[Fact]
	public void ReturnNullThrowsWithInvalidSequence2()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithMultipleParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(input2, input1);
	}

	[Fact]
	public void ReturnNullThrowsWithInvalidSequence3()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 2,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithMultipleParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(input2, input2);
	}

	[Fact]
	public void ReturnNullThrowsWithDifferentInstancesInvalidSequence1()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithMultipleParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(input2, input1);
	}

	[Fact]
	public void ReturnNullThrowsWithDifferentInstancesInvalidSequence2()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithMultipleParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(input1, input1);
	}

	[Fact]
	public void ReturnNullThrowsWithDifferentInstancesInvalidSequence3()
	{
		const string errorMessage = nameof(errorMessage);

		var input1 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		var input2 = new ClassParameter1
		{
			Number = 1,
			Text = "Some text",
		};

		ClassDependencyServiceMock
			.SetupInvokeWithMultipleParameters(input1, input2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithMultipleParameters(input2, input1);
	}
}
