namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class Indexer : IssuesTestsBase
{
	[Fact]
	public async Task GenerateIndexer()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				string? this[string key] { get; set; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("String", 1),
			new("String", 2, isNullable: true),
		};

		GeneratedSources generatedSources =
		[
			TestsBase,
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

					// this[]
					private SetupWithParameter<string, string?>? _indexer0Get;
					private Invocation<string>? _indexer0GetInvocation;
					private SetupStringStringNullable? _indexer0Set;
					private InvocationStringStringNullable? _indexer0SetInvocation;

					public SetupWithParameter<string, string?> SetupGetIndexer(in It<string> key)
					{
						_indexer0Get ??= new SetupWithParameter<string, string?>();
						_indexer0Get.SetupParameter(key.ValueSetup);
						return _indexer0Get;
					}

					public void VerifyGetIndexer(in It<string> key, in Times times)
					{
						_indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
						_indexer0GetInvocation.Verify(key.ValueSetup, times, _invocationProviders);
					}

					public long VerifyGetIndexer(in It<string> key, long index)
					{
						_indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
						return _indexer0GetInvocation.Verify(key.ValueSetup, index, _invocationProviders);
					}

					public SetupStringStringNullable SetupSetIndexer(in It<string> key, in It<string?> value)
					{
						_indexer0Set ??= new SetupStringStringNullable();
						_indexer0Set.SetupParameters(key.ValueSetup, value.ValueSetup);
						return _indexer0Set;
					}

					public void VerifySetIndexer(in It<string> key, in It<string?> value, in Times times)
					{
						_indexer0SetInvocation ??= new InvocationStringStringNullable("IInterface.This[{0}].set = {1}");
						_indexer0SetInvocation.Verify(key.ValueSetup, value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetIndexer(in It<string> key, in It<string?> value, long index)
					{
						_indexer0SetInvocation ??= new InvocationStringStringNullable("IInterface.This[{0}].set = {1}");
						return _indexer0SetInvocation.Verify(key.ValueSetup, value.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_indexer0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_indexer0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _indexer0GetInvocation;
						yield return _indexer0SetInvocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public string? this[string key]
						{
							get
							{
								_mock._indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
								_mock._indexer0GetInvocation.Register(_mock._invocationIndex, key);
								return _mock._indexer0Get?.Execute(key, out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._indexer0SetInvocation ??= new InvocationStringStringNullable("IInterface.This[{0}].set = {1}");
								_mock._indexer0SetInvocation.Register(_mock._invocationIndex, key, value);
								_mock._indexer0Set?.Invoke(key, value);
							}
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// this[]
						public ISetup<System.Action<string>, string?, System.Func<string, string?>> SetupGetIndexer(in It<string> key = default) =>
							((InterfaceMock)@this).SetupGetIndexer(key);

						public void VerifyGetIndexer(in It<string> key, in Times times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, times);

						public void VerifyGetIndexer(in It<string> key, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, times());

						public ISetup<SetupStringStringNullable.CallbackDelegate> SetupSetIndexer(in It<string> key = default, in It<string?> value = default) =>
							((InterfaceMock)@this).SetupSetIndexer(key, value);

						public void VerifySetIndexer(in It<string> key, in It<string?> value, in Times times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, value, times);

						public void VerifySetIndexer(in It<string> key, in It<string?> value, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, value, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// this[]
						public void GetIndexer(in It<string> key)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetIndexer(key, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetIndexer(in It<string> key, in It<string?> value)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifySetIndexer(key, value, @this.VerifyIndex);
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

	[Fact]
	public async Task GenerateMultipleIndexers()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				string? this[string key] { get; set; }
				string this[int key] { get; set; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types1 = new TypeModel[]
		{
			new("String", 1),
			new("String", 2, isNullable: true),
		};

		string[] types2 = ["Int32", "String"];

		GeneratedSources generatedSources =
		[
			TestsBase,
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

					// this[]
					private SetupWithParameter<string, string?>? _indexer0Get;
					private Invocation<string>? _indexer0GetInvocation;
					private SetupStringStringNullable? _indexer0Set;
					private InvocationStringStringNullable? _indexer0SetInvocation;

					public SetupWithParameter<string, string?> SetupGetIndexer(in It<string> key)
					{
						_indexer0Get ??= new SetupWithParameter<string, string?>();
						_indexer0Get.SetupParameter(key.ValueSetup);
						return _indexer0Get;
					}

					public void VerifyGetIndexer(in It<string> key, in Times times)
					{
						_indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
						_indexer0GetInvocation.Verify(key.ValueSetup, times, _invocationProviders);
					}

					public long VerifyGetIndexer(in It<string> key, long index)
					{
						_indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
						return _indexer0GetInvocation.Verify(key.ValueSetup, index, _invocationProviders);
					}

					public SetupStringStringNullable SetupSetIndexer(in It<string> key, in It<string?> value)
					{
						_indexer0Set ??= new SetupStringStringNullable();
						_indexer0Set.SetupParameters(key.ValueSetup, value.ValueSetup);
						return _indexer0Set;
					}

					public void VerifySetIndexer(in It<string> key, in It<string?> value, in Times times)
					{
						_indexer0SetInvocation ??= new InvocationStringStringNullable("IInterface.This[{0}].set = {1}");
						_indexer0SetInvocation.Verify(key.ValueSetup, value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetIndexer(in It<string> key, in It<string?> value, long index)
					{
						_indexer0SetInvocation ??= new InvocationStringStringNullable("IInterface.This[{0}].set = {1}");
						return _indexer0SetInvocation.Verify(key.ValueSetup, value.ValueSetup, index, _invocationProviders);
					}

					// this[]
					private SetupWithParameter<int, string>? _indexer1Get;
					private Invocation<int>? _indexer1GetInvocation;
					private SetupInt32String? _indexer1Set;
					private InvocationInt32String? _indexer1SetInvocation;

					public SetupWithParameter<int, string> SetupGetIndexer(in It<int> key)
					{
						_indexer1Get ??= new SetupWithParameter<int, string>();
						_indexer1Get.SetupParameter(key.ValueSetup);
						return _indexer1Get;
					}

					public void VerifyGetIndexer(in It<int> key, in Times times)
					{
						_indexer1GetInvocation ??= new Invocation<int>("IInterface.This[{0}].get");
						_indexer1GetInvocation.Verify(key.ValueSetup, times, _invocationProviders);
					}

					public long VerifyGetIndexer(in It<int> key, long index)
					{
						_indexer1GetInvocation ??= new Invocation<int>("IInterface.This[{0}].get");
						return _indexer1GetInvocation.Verify(key.ValueSetup, index, _invocationProviders);
					}

					public SetupInt32String SetupSetIndexer(in It<int> key, in It<string> value)
					{
						_indexer1Set ??= new SetupInt32String();
						_indexer1Set.SetupParameters(key.ValueSetup, value.ValueSetup);
						return _indexer1Set;
					}

					public void VerifySetIndexer(in It<int> key, in It<string> value, in Times times)
					{
						_indexer1SetInvocation ??= new InvocationInt32String("IInterface.This[{0}].set = {1}");
						_indexer1SetInvocation.Verify(key.ValueSetup, value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetIndexer(in It<int> key, in It<string> value, long index)
					{
						_indexer1SetInvocation ??= new InvocationInt32String("IInterface.This[{0}].set = {1}");
						return _indexer1SetInvocation.Verify(key.ValueSetup, value.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_indexer0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_indexer0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_indexer1GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_indexer1SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _indexer0GetInvocation;
						yield return _indexer0SetInvocation;
						yield return _indexer1GetInvocation;
						yield return _indexer1SetInvocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public string? this[string key]
						{
							get
							{
								_mock._indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
								_mock._indexer0GetInvocation.Register(_mock._invocationIndex, key);
								return _mock._indexer0Get?.Execute(key, out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._indexer0SetInvocation ??= new InvocationStringStringNullable("IInterface.This[{0}].set = {1}");
								_mock._indexer0SetInvocation.Register(_mock._invocationIndex, key, value);
								_mock._indexer0Set?.Invoke(key, value);
							}
						}

						public string this[int key]
						{
							get
							{
								_mock._indexer1GetInvocation ??= new Invocation<int>("IInterface.This[{0}].get");
								_mock._indexer1GetInvocation.Register(_mock._invocationIndex, key);
								return _mock._indexer1Get?.Execute(key, out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._indexer1SetInvocation ??= new InvocationInt32String("IInterface.This[{0}].set = {1}");
								_mock._indexer1SetInvocation.Register(_mock._invocationIndex, key, value);
								_mock._indexer1Set?.Invoke(key, value);
							}
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// this[]
						public ISetup<System.Action<string>, string?, System.Func<string, string?>> SetupGetIndexer(in It<string> key = default) =>
							((InterfaceMock)@this).SetupGetIndexer(key);

						public void VerifyGetIndexer(in It<string> key, in Times times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, times);

						public void VerifyGetIndexer(in It<string> key, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, times());

						public ISetup<SetupStringStringNullable.CallbackDelegate> SetupSetIndexer(in It<string> key = default, in It<string?> value = default) =>
							((InterfaceMock)@this).SetupSetIndexer(key, value);

						public void VerifySetIndexer(in It<string> key, in It<string?> value, in Times times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, value, times);

						public void VerifySetIndexer(in It<string> key, in It<string?> value, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, value, times());

						// this[]
						public ISetup<System.Action<int>, string, System.Func<int, string>> SetupGetIndexer(in It<int> key = default) =>
							((InterfaceMock)@this).SetupGetIndexer(key);

						public void VerifyGetIndexer(in It<int> key, in Times times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, times);

						public void VerifyGetIndexer(in It<int> key, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, times());

						public ISetup<SetupInt32String.CallbackDelegate> SetupSetIndexer(in It<int> key = default, in It<string> value = default) =>
							((InterfaceMock)@this).SetupSetIndexer(key, value);

						public void VerifySetIndexer(in It<int> key, in It<string> value, in Times times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, value, times);

						public void VerifySetIndexer(in It<int> key, in It<string> value, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, value, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// this[]
						public void GetIndexer(in It<string> key)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetIndexer(key, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetIndexer(in It<string> key, in It<string?> value)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifySetIndexer(key, value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// this[]
						public void GetIndexer(in It<int> key)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetIndexer(key, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetIndexer(in It<int> key, in It<string> value)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifySetIndexer(key, value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			CreateSetupCode(types1),
			CreateInvocationCode(types1),
			CreateSetupCode(types2),
			CreateInvocationCode(types2),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateIndexerWithMultipleParams()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				string? this[string key, int param1, bool param2] { get; set; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var typesGet = new TypeModel[]
		{
			new("String", 1),
			new("Int32", 2),
			new("Boolean", 3),
		};

		TypeModel[] typesSet =
		[
			..typesGet,
			new("String", 4, isNullable: true),
		];

		GeneratedSources generatedSources =
		[
			TestsBase,
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

					// this[]
					private SetupStringInt32Boolean<string?>? _indexer0Get;
					private InvocationStringInt32Boolean? _indexer0GetInvocation;
					private SetupStringInt32BooleanStringNullable? _indexer0Set;
					private InvocationStringInt32BooleanStringNullable? _indexer0SetInvocation;

					public SetupStringInt32Boolean<string?> SetupGetIndexer(in It<string> key, in It<int> param1, in It<bool> param2)
					{
						_indexer0Get ??= new SetupStringInt32Boolean<string?>();
						_indexer0Get.SetupParameters(key.ValueSetup, param1.ValueSetup, param2.ValueSetup);
						return _indexer0Get;
					}

					public void VerifyGetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, in Times times)
					{
						_indexer0GetInvocation ??= new InvocationStringInt32Boolean("IInterface.This[{0}, {1}, {2}].get");
						_indexer0GetInvocation.Verify(key.ValueSetup, param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyGetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, long index)
					{
						_indexer0GetInvocation ??= new InvocationStringInt32Boolean("IInterface.This[{0}, {1}, {2}].get");
						return _indexer0GetInvocation.Verify(key.ValueSetup, param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					public SetupStringInt32BooleanStringNullable SetupSetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, in It<string?> value)
					{
						_indexer0Set ??= new SetupStringInt32BooleanStringNullable();
						_indexer0Set.SetupParameters(key.ValueSetup, param1.ValueSetup, param2.ValueSetup, value.ValueSetup);
						return _indexer0Set;
					}

					public void VerifySetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, in It<string?> value, in Times times)
					{
						_indexer0SetInvocation ??= new InvocationStringInt32BooleanStringNullable("IInterface.This[{0}, {1}, {2}].set = {3}");
						_indexer0SetInvocation.Verify(key.ValueSetup, param1.ValueSetup, param2.ValueSetup, value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, in It<string?> value, long index)
					{
						_indexer0SetInvocation ??= new InvocationStringInt32BooleanStringNullable("IInterface.This[{0}, {1}, {2}].set = {3}");
						return _indexer0SetInvocation.Verify(key.ValueSetup, param1.ValueSetup, param2.ValueSetup, value.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_indexer0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_indexer0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _indexer0GetInvocation;
						yield return _indexer0SetInvocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public string? this[string key, int param1, bool param2]
						{
							get
							{
								_mock._indexer0GetInvocation ??= new InvocationStringInt32Boolean("IInterface.This[{0}, {1}, {2}].get");
								_mock._indexer0GetInvocation.Register(_mock._invocationIndex, key, param1, param2);
								return _mock._indexer0Get?.Execute(key, param1, param2, out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._indexer0SetInvocation ??= new InvocationStringInt32BooleanStringNullable("IInterface.This[{0}, {1}, {2}].set = {3}");
								_mock._indexer0SetInvocation.Register(_mock._invocationIndex, key, param1, param2, value);
								_mock._indexer0Set?.Invoke(key, param1, param2, value);
							}
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// this[]
						public ISetup<SetupStringInt32Boolean<string?>.CallbackDelegate, string?, SetupStringInt32Boolean<string?>.ReturnsCallbackDelegate> SetupGetIndexer(in It<string> key = default, in It<int> param1 = default, in It<bool> param2 = default) =>
							((InterfaceMock)@this).SetupGetIndexer(key, param1, param2);

						public void VerifyGetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, in Times times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, param1, param2, times);

						public void VerifyGetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, param1, param2, times());

						public ISetup<SetupStringInt32BooleanStringNullable.CallbackDelegate> SetupSetIndexer(in It<string> key = default, in It<int> param1 = default, in It<bool> param2 = default, in It<string?> value = default) =>
							((InterfaceMock)@this).SetupSetIndexer(key, param1, param2, value);

						public void VerifySetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, in It<string?> value, in Times times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, param1, param2, value, times);

						public void VerifySetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, in It<string?> value, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, param1, param2, value, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// this[]
						public void GetIndexer(in It<string> key, in It<int> param1, in It<bool> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetIndexer(key, param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetIndexer(in It<string> key, in It<int> param1, in It<bool> param2, in It<string?> value)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifySetIndexer(key, param1, param2, value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			CreateSetupReturnsCode(typesGet),
			CreateInvocationCode(typesGet),
			CreateSetupCode(typesSet),
			CreateInvocationCode(typesSet),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
