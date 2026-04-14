namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class NestedClasses : TestsBase
{
	[Fact]
	public async Task GenerateWithNestedDelegate()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

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
			new("ChangesHandler", [("T1", true)], 2, @namespace: "MyNihongo.Mockurai.Tests.Container"),
		];

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// ContainerMock
					private readonly ContainerMock _containerMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.Container> ContainerMock => _containerMock;

					protected void VerifyNoOtherCalls()
					{
						ContainerMock.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							containerMock: ContainerMock
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.Container> ContainerMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.Container> containerMock)
						{
							ContainerMock = new MockSequence<MyNihongo.Mockurai.Tests.Container>
							{
								VerifyIndex = _verifyIndex,
								Mock = containerMock,
							};
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

				public sealed class ContainerMock : IMock<MyNihongo.Mockurai.Tests.Container>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public ContainerMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public MyNihongo.Mockurai.Tests.Container Object => _proxy ??= new Proxy(this);

					// Execute
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _execute0;
					private InvocationDictionary? _execute0Invocation;

					public SetupStringChangesHandlerT1<T> SetupExecute<T>(in It<string> processorName, in It<MyNihongo.Mockurai.Tests.Container.ChangesHandler<T>> onChangesDelegate)
					{
						_execute0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var execute0 = (SetupStringChangesHandlerT1<T>)_execute0.GetOrAdd(typeof(T), static _ => new SetupStringChangesHandlerT1<T>());
						execute0.SetupParameters(processorName.ValueSetup, onChangesDelegate.ValueSetup);
						return execute0;
					}

					public void VerifyExecute<T>(in It<string> processorName, in It<MyNihongo.Mockurai.Tests.Container.ChangesHandler<T>> onChangesDelegate, in Times times)
					{
						_execute0Invocation ??= new InvocationDictionary();
						var execute0Invocation = (InvocationStringChangesHandlerT1<T>)_execute0Invocation.GetOrAdd(typeof(T), static key => new InvocationStringChangesHandlerT1<T>($"Container.Execute<{key.Name}>({{0}}, {{1}})"));
						execute0Invocation.Verify(processorName.ValueSetup, onChangesDelegate.ValueSetup, times, _invocationProviders);
					}

					public long VerifyExecute<T>(in It<string> processorName, in It<MyNihongo.Mockurai.Tests.Container.ChangesHandler<T>> onChangesDelegate, long index)
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

					private sealed class Proxy : MyNihongo.Mockurai.Tests.Container
					{
						private readonly ContainerMock _mock;

						public Proxy(ContainerMock mock)
						{
							_mock = mock;
						}

						public override void Execute<T>(string processorName, MyNihongo.Mockurai.Tests.Container.ChangesHandler<T> onChangesDelegate)
						{
							_mock._execute0Invocation ??= new InvocationDictionary();
							var execute0Invocation = (InvocationStringChangesHandlerT1<T>)_mock._execute0Invocation.GetOrAdd(typeof(T), static key => new InvocationStringChangesHandlerT1<T>($"Container.Execute<{key.Name}>({{0}}, {{1}})"));
							execute0Invocation.Register(_mock._invocationIndex, processorName, onChangesDelegate);
							((SetupStringChangesHandlerT1<T>?)_mock._execute0?.ValueOrDefault(typeof(T)))?.Invoke(processorName, onChangesDelegate);
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Mockurai.Tests.Container> @this)
					{
						public void VerifyNoOtherCalls() =>
							((ContainerMock)@this).VerifyNoOtherCalls();

						// Execute
						public ISetup<SetupStringChangesHandlerT1<T>.CallbackDelegate> SetupExecute<T>(in It<string> processorName = default, in It<MyNihongo.Mockurai.Tests.Container.ChangesHandler<T>> onChangesDelegate = default) =>
							((ContainerMock)@this).SetupExecute<T>(processorName, onChangesDelegate);

						public void VerifyExecute<T>(in It<string> processorName, in It<MyNihongo.Mockurai.Tests.Container.ChangesHandler<T>> onChangesDelegate, in Times times) =>
							((ContainerMock)@this).VerifyExecute<T>(processorName, onChangesDelegate, times);

						public void VerifyExecute<T>(in It<string> processorName, in It<MyNihongo.Mockurai.Tests.Container.ChangesHandler<T>> onChangesDelegate, System.Func<Times> times) =>
							((ContainerMock)@this).VerifyExecute<T>(processorName, onChangesDelegate, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mockurai.Tests.Container> @this)
					{
						// Execute
						public void Execute<T>(in It<string> processorName, in It<MyNihongo.Mockurai.Tests.Container.ChangesHandler<T>> onChangesDelegate)
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
		await ctx.RunAsync();
	}
}
