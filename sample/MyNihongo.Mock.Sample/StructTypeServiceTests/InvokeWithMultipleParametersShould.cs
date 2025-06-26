// namespace MyNihongo.Mock.Sample.StructTypeServiceTests;
//
// public sealed class InvokeWithMultipleParametersShould : StructTypeServiceTestsBase
// {
// 	[Fact]
// 	public void ExecuteWithoutSetup()
// 	{
// 		var input1 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		var input2 = new StructParameter1
// 		{
// 			Number = 2,
// 			Text = "Some text",
// 		};
//
// 		CreateFixture()
// 			.InvokeWithMultipleParameters(input1, input2);
// 	}
//
// 	[Fact]
// 	public void ThrowWithSetup()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var input1 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		var input2 = new StructParameter1
// 		{
// 			Number = 2,
// 			Text = "Some text",
// 		};
//
// 		StructDependencyServiceMock
// 			.SetupInvokeWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		var actual = () => CreateFixture()
// 			.InvokeWithMultipleParameters(input1, input2);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public void ExecuteWithInvalidSequence1()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var input1 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		var input2 = new StructParameter1
// 		{
// 			Number = 2,
// 			Text = "Some text",
// 		};
//
// 		StructDependencyServiceMock
// 			.SetupInvokeWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		CreateFixture()
// 			.InvokeWithMultipleParameters(input1, input1);
// 	}
//
// 	[Fact]
// 	public void ExecuteWithInvalidSequence2()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var input1 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		var input2 = new StructParameter1
// 		{
// 			Number = 2,
// 			Text = "Some text",
// 		};
//
// 		StructDependencyServiceMock
// 			.SetupInvokeWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		CreateFixture()
// 			.InvokeWithMultipleParameters(input2, input1);
// 	}
//
// 	[Fact]
// 	public void ExecuteWithInvalidSequence3()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var input1 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		var input2 = new StructParameter1
// 		{
// 			Number = 2,
// 			Text = "Some text",
// 		};
//
// 		StructDependencyServiceMock
// 			.SetupInvokeWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		CreateFixture()
// 			.InvokeWithMultipleParameters(input2, input2);
// 	}
//
// 	[Fact]
// 	public void ThrowWithDifferentInstancesInvalidSequence1()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var input1 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		var input2 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		StructDependencyServiceMock
// 			.SetupInvokeWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		var actual = () => CreateFixture()
// 			.InvokeWithMultipleParameters(input2, input1);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowWithDifferentInstancesInvalidSequence2()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var input1 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		var input2 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		StructDependencyServiceMock
// 			.SetupInvokeWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		var actual = () => CreateFixture()
// 			.InvokeWithMultipleParameters(input1, input1);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowWithDifferentInstancesInvalidSequence3()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var input1 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		var input2 = new StructParameter1
// 		{
// 			Number = 1,
// 			Text = "Some text",
// 		};
//
// 		StructDependencyServiceMock
// 			.SetupInvokeWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		var actual = () => CreateFixture()
// 			.InvokeWithMultipleParameters(input2, input1);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
// }
