// namespace MyNihongo.Mock.Sample.RecordTypeServiceTests;
//
// public sealed class ReturnWithOneParameterShould : RecordTypeServiceTestsBase
// {
// 	[Fact]
// 	public void ThrowWithoutSetup()
// 	{
// 		var input = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithOneParameter(input);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IRecordDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public void ReturnValueWithSetupSameInstance()
// 	{
// 		var setupParameter = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		RecordDependencyServiceMock
// 			.SetupReturnWithOneParameter(setupParameter)
// 			.Returns(new RecordReturn(
// 				Name: "Okayama Issei",
// 				Age: 12,
// 				DateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		var actual = CreateFixture()
// 			.ReturnWithOneParameter(setupParameter);
//
// 		const string expected = "name:Okayama Issei,age:12";
// 		Assert.Equal(expected, actual);
// 	}
//
// 	[Fact]
// 	public void ReturnValueWithSetupAnotherInstance()
// 	{
// 		var setupParameter = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		var input = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		RecordDependencyServiceMock
// 			.SetupReturnWithOneParameter(setupParameter)
// 			.Returns(new RecordReturn(
// 				Age: 12,
// 				Name: "Okayama Issei",
// 				DateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		var actual = CreateFixture()
// 			.ReturnWithOneParameter(input);
//
// 		const string expected = "name:Okayama Issei,age:12";
// 		Assert.Equal(expected, actual);
// 	}
//
// 	[Fact]
// 	public void ThrowWithSetupAnotherSetup()
// 	{
// 		var setupParameter = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		var input = new RecordParameter1(
// 			Number: 12345678,
// 			Text: "Another text"
// 		);
//
// 		RecordDependencyServiceMock
// 			.SetupReturnWithOneParameter(setupParameter)
// 			.Returns(new RecordReturn(
// 				Age: 12,
// 				Name: "Okayama Issei",
// 				DateOfBirth: new DateOnly(2025, 6, 16)
// 			));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithOneParameter(input);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IRecordDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowWithSetupSameInstance()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var setupParameter = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		RecordDependencyServiceMock
// 			.SetupReturnWithOneParameter(setupParameter)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithOneParameter(setupParameter);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowWithSetupAnotherInstance()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var setupParameter = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		var input = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		RecordDependencyServiceMock
// 			.SetupReturnWithOneParameter(setupParameter)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithOneParameter(input);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public void ThrowForThrowsWithSetupAnotherSetup()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var setupParameter = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		var input = new RecordParameter1(
// 			Number: 12345678,
// 			Text: "Another text"
// 		);
//
// 		RecordDependencyServiceMock
// 			.SetupReturnWithOneParameter(setupParameter)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Action actual = () => CreateFixture()
// 			.ReturnWithOneParameter(input);
//
// 		var exception = Assert.Throws<NullReferenceException>(actual);
// 		Assert.Equal("IRecordDependencyService#ReturnWithParameter() method has not been set up", exception.Message);
// 	}
// }
