namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class SpecialCases : IssuesTestsBase
{
	[Fact]
	public async Task StreamMock()
	{
		const string testCode =
			"""
			using System.IO;

			namespace Issues.Tests;

			public interface IInterface
			{
				Stream GetStream();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
				protected partial IMock<Stream> StreamMock { get; }

				protected TestsBase()
				{
					InterfaceMock
						.SetupGetStream()
						.Returns(StreamMock.Object);
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace Issues.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<Issues.Tests.IInterface> InterfaceMock => _interfaceMock;

					// StreamMock
					private readonly StreamMock _streamMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<Stream> StreamMock => _streamMock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						InterfaceMock.VerifyNoOtherCalls();
						StreamMock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock: InterfaceMock,
							streamMock: StreamMock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<Issues.Tests.IInterface> InterfaceMock;
						public readonly IMockSequence<Stream> StreamMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<Issues.Tests.IInterface> interfaceMock, MyNihongo.Mockurai.IMock<Stream> streamMock)
						{
							VerifyIndex = new VerifyIndex();
							InterfaceMock = new MockSequence<Issues.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interfaceMock,
							};
							StreamMock = new MockSequence<Stream>
							{
								VerifyIndex = VerifyIndex,
								Mock = streamMock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							InterfaceMock = ctx.InterfaceMock;
							StreamMock = ctx.StreamMock;
						}
					}
				}
				"""
			),
			(
				"InterfaceMock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InterfaceMock : IMock<Issues.Tests.IInterface>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public InterfaceMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.IInterface Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// GetStream
					private Setup<Stream>? _getStream0;
					private Invocation? _getStream0Invocation;

					public Setup<Stream> SetupGetStream()
					{
						_getStream0 ??= new Setup<Stream>();
						_getStream0.Reset();
						return _getStream0;
					}

					public void VerifyGetStream(in Times times)
					{
						_getStream0Invocation ??= new Invocation("IInterface.GetStream()");
						_getStream0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetStream(long index)
					{
						_getStream0Invocation ??= new Invocation("IInterface.GetStream()");
						return _getStream0Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_getStream0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _getStream0Invocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public Stream GetStream()
						{
							_mock._getStream0Invocation ??= new Invocation("IInterface.GetStream()");
							_mock._getStream0Invocation.Register(_mock._invocationIndex);
							return _mock._getStream0?.Execute(out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> GetStream => _mock._getStream0Invocation?.GetInvocations() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// GetStream
						public ISetup<System.Action, Stream, System.Func<Stream>> SetupGetStream() =>
							((InterfaceMock)@this).SetupGetStream();

						public void VerifyGetStream(in Times times) =>
							((InterfaceMock)@this).VerifyGetStream(times);

						public void VerifyGetStream(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetStream(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// GetStream
						public void GetStream()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetStream(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"StreamMock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class StreamMock : IMock<Stream>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public StreamMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Stream Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					public void VerifyNoOtherCalls()
					{
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield break;
					}

					private sealed class Proxy : Stream
					{
						private readonly StreamMock _mock;

						public Proxy(StreamMock mock)
						{
							_mock = mock;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly StreamMock _mock;

						public InvocationContainer(StreamMock mock)
						{
							_mock = mock;
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Stream> @this)
					{
						public StreamMock.InvocationContainer Invocations => ((StreamMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((StreamMock)@this).VerifyNoOtherCalls();
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Stream> @this)
					{
					}
				}
				"""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
