namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class BaseClassesTest : TestsBase
{
	private static readonly GeneratedSource InterfaceMockSource =
	(
		"InterfaceMock.g.cs",
		"""
		#nullable enable
		namespace MyNihongo.Mockurai;

		public sealed class InterfaceMock : IMock<MyNihongo.BaseClasses.Tests.IInterface>
		{
			private readonly InvocationIndex.Counter _invocationIndex;
			private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			private Proxy? _proxy;

			public InterfaceMock(InvocationIndex.Counter invocationIndex)
			{
				_invocationIndex = invocationIndex;
				_invocationProviders = GetInvocations;
			}

			public MyNihongo.BaseClasses.Tests.IInterface Object => _proxy ??= new Proxy(this);

			public InvocationContainer Invocations => field ??= new InvocationContainer(this);

			// Invoke
			private Setup? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup SetupInvoke()
			{
				_invoke0 ??= new Setup();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}

			public void VerifyNoOtherCalls()
			{
				_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
			}

			private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			{
				yield return _invoke0Invocation;
			}

			private sealed class Proxy : MyNihongo.BaseClasses.Tests.IInterface
			{
				private readonly InterfaceMock _mock;

				public Proxy(InterfaceMock mock)
				{
					_mock = mock;
				}

				public void Invoke()
				{
					_mock._invoke0Invocation ??= new Invocation("IInterface.Invoke()");
					_mock._invoke0Invocation.Register(_mock._invocationIndex);
					_mock._invoke0?.Invoke();
				}
			}

			public sealed class InvocationContainer
			{
				private readonly InterfaceMock _mock;

				public InvocationContainer(InterfaceMock mock)
				{
					_mock = mock;
				}

				public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];
			}
		}

		public static partial class MockExtensions
		{
			extension(IMock<MyNihongo.BaseClasses.Tests.IInterface> @this)
			{
				public InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

				public void VerifyNoOtherCalls() =>
					((InterfaceMock)@this).VerifyNoOtherCalls();

				// Invoke
				public ISetup<System.Action> SetupInvoke() =>
					((InterfaceMock)@this).SetupInvoke();

				public void VerifyInvoke(in Times times) =>
					((InterfaceMock)@this).VerifyInvoke(times);

				public void VerifyInvoke(System.Func<Times> times) =>
					((InterfaceMock)@this).VerifyInvoke(times());
			}
		}

		public static partial class MockSequenceExtensions
		{
			extension(IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> @this)
			{
				// Invoke
				public void Invoke()
				{
					var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
					@this.VerifyIndex.Set(nextIndex);
				}
			}
		}
		"""
	);

	[Theory]
	[InlineData("MockuraiGenerate")]
	[InlineData("MyNihongo.Mockurai.MockuraiGenerate")]
	public async Task InvokeBaseClassMethods(string attribute)
	{
		var testCode =
			$$"""
			  namespace MyNihongo.BaseClasses.Tests;

			  public interface IInterface
			  {
			  	void Invoke();
			  }

			  [{{attribute}}]
			  public abstract partial class TestsBase
			  {
			  	protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }
			  }

			  [{{attribute}}]
			  public abstract partial class TestsDerivedBase : TestsBase
			  {
			  	protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }
			  }
			  """;

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					protected override void VerifyNoOtherCalls()
					{
						base.VerifyNoOtherCalls();
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task AddVerifyNoOtherCallsWithoutParams()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls()
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					protected override void VerifyNoOtherCalls()
					{
						this.BeforeVerifyNoOtherCalls();
						base.VerifyNoOtherCalls();
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task OverrideVerifyNoOtherCallsWithoutParams()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls()
				{
				}
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls()
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						this.BeforeVerifyNoOtherCalls();
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					protected override void VerifyNoOtherCalls()
					{
						this.BeforeVerifyNoOtherCalls();
						base.VerifyNoOtherCalls();
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task AddVerifyNoOtherCallsWithParams()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg)
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected virtual void VerifyNoOtherCalls(int arg)
					{
						this.BeforeVerifyNoOtherCalls(arg);
						base.VerifyNoOtherCalls();
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task AddVerifyNoOtherCallsWithDefaultParams()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg = 0)
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected virtual void VerifyNoOtherCalls(int arg = 0)
					{
						this.BeforeVerifyNoOtherCalls(arg);
						base.VerifyNoOtherCalls();
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task OverloadVerifyNoOtherCallsWithDefaultParams()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg2 = 0)
				{
				}
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg1 = 0)
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls(int arg2 = 0)
					{
						this.BeforeVerifyNoOtherCalls(arg2);
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected virtual void VerifyNoOtherCalls(int arg1 = 0, int arg2 = 0)
					{
						this.BeforeVerifyNoOtherCalls(arg1);
						base.VerifyNoOtherCalls(arg2);
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task OverloadVerifyNoOtherCallsWithSameDefaultParams()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg = 0)
				{
				}
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg = 0)
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls(int arg = 0)
					{
						this.BeforeVerifyNoOtherCalls(arg);
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					protected override void VerifyNoOtherCalls(int arg = 0)
					{
						this.BeforeVerifyNoOtherCalls(arg);
						base.VerifyNoOtherCalls(arg);
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task OverrideVerifyNoOtherCallsWithSameParam()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg)
				{
				}
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg)
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls(int arg)
					{
						this.BeforeVerifyNoOtherCalls(arg);
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					protected override void VerifyNoOtherCalls(int arg)
					{
						this.BeforeVerifyNoOtherCalls(arg);
						base.VerifyNoOtherCalls(arg);
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task OverrideVerifyNoOtherCallsWithDifferentParams()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg1)
				{
				}
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg2)
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls(int arg1)
					{
						this.BeforeVerifyNoOtherCalls(arg1);
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected virtual void VerifyNoOtherCalls(int arg2, int arg1)
					{
						this.BeforeVerifyNoOtherCalls(arg2);
						base.VerifyNoOtherCalls(arg1);
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task OverrideVerifyNoOtherCallsWithDifferentDefaultParams()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg1)
				{
				}
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }

				[MockuraiBeforeVerifyNoOtherCalls]
				private void BeforeVerifyNoOtherCalls(int arg2 = 0)
				{
				}
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls(int arg1)
					{
						this.BeforeVerifyNoOtherCalls(arg1);
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected virtual void VerifyNoOtherCalls(int arg1, int arg2 = 0)
					{
						this.BeforeVerifyNoOtherCalls(arg2);
						base.VerifyNoOtherCalls(arg1);
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateOverridesCorrectlyWhenOrderIsWrong()
	{
		const string testCode =
			"""
			namespace MyNihongo.BaseClasses.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerived1Base : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerived2Base : TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface2Mock { get; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial MyNihongo.Mockurai.IMock<IInterface> Interface1Mock { get; }
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock => _interface1Mock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						Interface1Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface1Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerived1Base.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerived1Base
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					protected override void VerifyNoOtherCalls()
					{
						base.VerifyNoOtherCalls();
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			(
				"TestsDerived2Base.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.BaseClasses.Tests;

				public partial class TestsDerived2Base
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock => _interface2Mock;

					protected override void VerifyNoOtherCalls()
					{
						base.VerifyNoOtherCalls();
						Interface2Mock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(2)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						base.VerifyInSequence(ctx =>
						{
							var thisCtx = new VerifySequenceContext(
								ctx: ctx,
								interface2Mock: Interface2Mock
							);

							verify(thisCtx);
						});
					}

					protected new class VerifySequenceContext : MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext
					{
						public readonly IMockSequence<MyNihongo.BaseClasses.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(MyNihongo.BaseClasses.Tests.TestsBase.VerifySequenceContext ctx, MyNihongo.Mockurai.IMock<MyNihongo.BaseClasses.Tests.IInterface> interface2Mock)
							: base(ctx)
						{
							Interface2Mock = new MockSequence<MyNihongo.BaseClasses.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
							: base(ctx)
						{
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			InterfaceMockSource,
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
