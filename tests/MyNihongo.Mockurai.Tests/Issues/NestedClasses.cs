namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class NestedClasses : TestsBase
{
	[Fact]
	public async Task GenerateWithNestedDelegate()
	{
		const string testCode =
			"""
			using System.Threading.Tasks;

			namespace Issues.Tests;

			public abstract class Container
			{
				public delegate Task ChangesHandler<T>(System.Collections.Generic.IReadOnlyCollection<T> changes, System.Threading.CancellationToken cancellationToken);

				public abstract void Execute<T>(string processorName, ChangesHandler<T> onChangesDelegate);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Container> ContainerMock { get; }
			}
			""";

		TypeModel[] types =
		[
			new("String", 1),
			new("ChangesHandler", [("T1", true)], 2, @namespace: "Issues.Tests.Container"),
		];

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace Issues.Tests;

				public partial class TestsBase
				{
					// ContainerMock
					private readonly ContainerMock _containerMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<Issues.Tests.Container> ContainerMock => _containerMock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						ContainerMock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							containerMock: ContainerMock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<Issues.Tests.Container> ContainerMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<Issues.Tests.Container> containerMock)
						{
							VerifyIndex = new VerifyIndex();
							ContainerMock = new MockSequence<Issues.Tests.Container>
							{
								VerifyIndex = VerifyIndex,
								Mock = containerMock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							ContainerMock = ctx.ContainerMock;
						}
					}
				}
				"""
			),
			(
				"ContainerMock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class ContainerMock : IMock<Issues.Tests.Container>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public ContainerMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.Container Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// Execute
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _execute0;
					private InvocationDictionary? _execute0Invocation;

					public SetupStringChangesHandlerT1<T> SetupExecute<T>(in It<string> processorName, in It<Issues.Tests.Container.ChangesHandler<T>> onChangesDelegate)
					{
						_execute0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var execute0 = (SetupStringChangesHandlerT1<T>)_execute0.GetOrAdd(typeof(T), static _ => new SetupStringChangesHandlerT1<T>());
						execute0.SetupParameters(processorName.ValueSetup, onChangesDelegate.ValueSetup);
						return execute0;
					}

					public void VerifyExecute<T>(in It<string> processorName, in It<Issues.Tests.Container.ChangesHandler<T>> onChangesDelegate, in Times times)
					{
						_execute0Invocation ??= new InvocationDictionary();
						var execute0Invocation = (InvocationStringChangesHandlerT1<T>)_execute0Invocation.GetOrAdd(typeof(T), static key => new InvocationStringChangesHandlerT1<T>($"Container.Execute<{key.Name}>({{0}}, {{1}})"));
						execute0Invocation.Verify(processorName.ValueSetup, onChangesDelegate.ValueSetup, times, _invocationProviders);
					}

					public long VerifyExecute<T>(in It<string> processorName, in It<Issues.Tests.Container.ChangesHandler<T>> onChangesDelegate, long index)
					{
						_execute0Invocation ??= new InvocationDictionary();
						var execute0Invocation = (InvocationStringChangesHandlerT1<T>)_execute0Invocation.GetOrAdd(typeof(T), static key => new InvocationStringChangesHandlerT1<T>($"Container.Execute<{key.Name}>({{0}}, {{1}})"));
						return execute0Invocation.Verify(processorName.ValueSetup, onChangesDelegate.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_execute0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _execute0Invocation;
					}

					private sealed class Proxy : Issues.Tests.Container
					{
						private readonly ContainerMock _mock;

						public Proxy(ContainerMock mock)
						{
							_mock = mock;
						}

						public override void Execute<T>(string processorName, Issues.Tests.Container.ChangesHandler<T> onChangesDelegate)
						{
							_mock._execute0Invocation ??= new InvocationDictionary();
							var execute0Invocation = (InvocationStringChangesHandlerT1<T>)_mock._execute0Invocation.GetOrAdd(typeof(T), static key => new InvocationStringChangesHandlerT1<T>($"Container.Execute<{key.Name}>({{0}}, {{1}})"));
							execute0Invocation.Register(_mock._invocationIndex, processorName, onChangesDelegate);
							((SetupStringChangesHandlerT1<T>?)_mock._execute0?.ValueOrDefault(typeof(T)))?.Invoke(processorName, onChangesDelegate);
						}
					}

					public sealed class InvocationContainer
					{
						private readonly ContainerMock _mock;

						public InvocationContainer(ContainerMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(string processorName, Issues.Tests.Container.ChangesHandler<T> onChangesDelegate)>> Execute<T>()
						{
							_mock._execute0Invocation ??= new InvocationDictionary();
							var execute0Invocation = (InvocationStringChangesHandlerT1<T>)_mock._execute0Invocation.GetOrAdd(typeof(T), static key => new InvocationStringChangesHandlerT1<T>($"Container.Execute<{key.Name}>({{0}}, {{1}})"));
							return execute0Invocation.GetInvocationsWithArguments() ?? [];
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.Container> @this)
					{
						public ContainerMock.InvocationContainer Invocations => ((ContainerMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((ContainerMock)@this).VerifyNoOtherCalls();

						// Execute
						public ISetup<SetupStringChangesHandlerT1<T>.CallbackDelegate> SetupExecute<T>(in It<string> processorName = default, in It<Issues.Tests.Container.ChangesHandler<T>> onChangesDelegate = default) =>
							((ContainerMock)@this).SetupExecute<T>(processorName, onChangesDelegate);

						public void VerifyExecute<T>(in It<string> processorName, in It<Issues.Tests.Container.ChangesHandler<T>> onChangesDelegate, in Times times) =>
							((ContainerMock)@this).VerifyExecute<T>(processorName, onChangesDelegate, times);

						public void VerifyExecute<T>(in It<string> processorName, in It<Issues.Tests.Container.ChangesHandler<T>> onChangesDelegate, System.Func<Times> times) =>
							((ContainerMock)@this).VerifyExecute<T>(processorName, onChangesDelegate, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.Container> @this)
					{
						// Execute
						public void Execute<T>(in It<string> processorName, in It<Issues.Tests.Container.ChangesHandler<T>> onChangesDelegate)
						{
							var nextIndex = ((ContainerMock)@this.Mock).VerifyExecute<T>(processorName, onChangesDelegate, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			CreateSetupCode(types),
			CreateInvocationCode(types),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
