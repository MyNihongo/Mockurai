// namespace MyNihongo.Mock.Sample.StructTypeServiceTests;
//
// public sealed class ReturnWithMultipleParametersShould : StructTypeServiceTestsBase
// {
// 	[Fact]
// 	public void ThrowWithoutSetup()
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
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input1, input2);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public void ReturnValueWithSetup()
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
// 		StructDependencyServiceMock
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Returns(new StructReturn(
// 				age: 12,
// 				name: "Okayama Issei",
// 				dateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		var actual = CreateFixture()
// 			.ReturnWithMultipleParameters(input1, input2);
//
// 		const double expected = 15d;
// 		Assert.Equal(expected, actual);
// 	}
//
// 	[Fact]
// 	public void ThrowWithInvalidSequence1()
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
// 		StructDependencyServiceMock
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Returns(new StructReturn(
// 				age: 12,
// 				name: "Okayama Issei",
// 				dateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input1, input1);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowWithInvalidSequence2()
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
// 		StructDependencyServiceMock
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Returns(new StructReturn(
// 				age: 12,
// 				name: "Okayama Issei",
// 				dateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input1);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowWithInvalidSequence3()
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
// 		StructDependencyServiceMock
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Returns(new StructReturn(
// 				age: 12,
// 				name: "Okayama Issei",
// 				dateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input2);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public void ReturnValueWithDifferentInstancesInvalidSequence1()
// 	{
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Returns(new StructReturn(
// 				age: 12,
// 				name: "Okayama Issei",
// 				dateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		var actual = CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input1);
//
// 		const double expected = 15d;
// 		Assert.Equal(expected, actual);
// 	}
//
// 	[Fact]
// 	public void ReturnValueWithDifferentInstancesInvalidSequence2()
// 	{
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Returns(new StructReturn(
// 				age: 12,
// 				name: "Okayama Issei",
// 				dateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		var actual = CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input1);
//
// 		const double expected = 15d;
// 		Assert.Equal(expected, actual);
// 	}
//
// 	[Fact]
// 	public void ReturnValueWithDifferentInstancesInvalidSequence3()
// 	{
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Returns(new StructReturn(
// 				age: 12,
// 				name: "Okayama Issei",
// 				dateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		var actual = CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input1);
//
// 		const double expected = 15d;
// 		Assert.Equal(expected, actual);
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input1, input2);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowForThrowsWithInvalidSequence1()
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input1, input1);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowForThrowsWithInvalidSequence2()
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input1);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowForThrowsWithInvalidSequence3()
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input2);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up", exception.Message);
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input1);
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input1);
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
// 			.SetupReturnWithMultipleParameters(input1, input2)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithMultipleParameters(input2, input1);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
// }
