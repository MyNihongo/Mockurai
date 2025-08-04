// ReSharper disable AccessToDisposedClosure

namespace MyNihongo.Mock.Abstractions.Tests.Invocation.InvocationWithOneParameterTests;

public sealed class VerifyClassDisposableShould : InvocationWithOneParameterTestsBase
{
	[Fact]
	public void VerifyDisposableParameter()
	{
		var index = new InvocationIndex.Counter();
		const string name = nameof(name);

		var fixture = CreateFixture<ClassParameterDisposable>();

		using (var parameter = new ClassParameterDisposable(name))
			fixture.Register(index, parameter);

		using var expected = new ClassParameterDisposable(name);
		fixture.Verify(It<ClassParameterDisposable>.Equivalent(expected), Times.Once());
	}

	[Fact]
	public void ThrowDisposed()
	{
		var index = new InvocationIndex.Counter();
		const string name = nameof(name);

		var fixture = CreateFixture<ClassParameterDisposable>();

		using (var parameter = new ClassParameterDisposable(name))
			fixture.Register(index, parameter);

		using var expected = new ClassParameterDisposable(name);
		var actual = () => fixture.Verify(expected, Times.Once());

		Assert.Throws<ObjectDisposedException>(actual);
	}
}
