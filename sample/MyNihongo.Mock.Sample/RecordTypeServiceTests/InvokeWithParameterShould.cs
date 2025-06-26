// namespace MyNihongo.Mock.Sample.RecordTypeServiceTests;
//
// public sealed class InvokeWithParameterShould : RecordTypeServiceTestsBase
// {
// 	[Fact]
// 	public void ExecuteWithoutSetup()
// 	{
// 		var input = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		CreateFixture()
// 			.InvokeWithParameter(input);
// 	}
//
// 	[Fact]
// 	public void ThrowWithSetupSameInstance()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		var input = new RecordParameter1(
// 			Number: 1,
// 			Text: "Some text"
// 		);
//
// 		RecordDependencyServiceMock
// 			.SetupInvokeWithParameter(input)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		var actual = () => CreateFixture()
// 			.InvokeWithParameter(input);
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
// 			.SetupInvokeWithParameter(setupParameter)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		var actual = () => CreateFixture()
// 			.InvokeWithParameter(input);
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public void ExecuteWithSetupAnotherSetup()
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
// 			.SetupInvokeWithParameter(setupParameter)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		CreateFixture()
// 			.InvokeWithParameter(input);
// 	}
// }
