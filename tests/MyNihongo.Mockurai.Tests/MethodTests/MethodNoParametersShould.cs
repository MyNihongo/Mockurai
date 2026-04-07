namespace MyNihongo.Mockurai.Tests.MethodTests;

public sealed class MethodNoParametersShould : MethodTestsBase
{
	[Fact]
	public async Task GenerateInterface()
	{
		const string method = "void Invoke();";

		const string methods =
			"""
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
			""";

		const string proxy =
			"""
			public void Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				_mock._invoke0?.Invoke();
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke() =>
				((InterfaceMock)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((InterfaceMock)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturn()
	{
		const string method = "decimal Invoke();";

		const string methods =
			"""
			// Invoke
			private Setup<decimal>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<decimal> SetupInvoke()
			{
				_invoke0 ??= new Setup<decimal>();
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
			""";

		const string proxy =
			"""
			public decimal Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvoke() =>
				((InterfaceMock)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((InterfaceMock)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturnGeneric()
	{
		const string method = "TReturn Invoke<T, TReturn>();";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public Setup<TReturn> SetupInvoke<T, TReturn>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (Setup<TReturn>)_invoke0.GetOrAdd((typeof(T), typeof(TReturn)), static _ => new Setup<TReturn>());
				return invoke0;
			}

			public void VerifyInvoke<T, TReturn>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T, TReturn>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public TReturn Invoke<T, TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<TReturn>?)_mock._invoke0?.ValueOrDefault((typeof(T), typeof(TReturn))))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvoke<T, TReturn>() =>
				((InterfaceMock)@this).SetupInvoke<T, TReturn>();

			public void VerifyInvoke<T, TReturn>(in Times times) =>
				((InterfaceMock)@this).VerifyInvoke<T, TReturn>(times);

			public void VerifyInvoke<T, TReturn>(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvoke<T, TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T, TReturn>()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T, TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithGeneric()
	{
		const string method = "void Invoke<T>();";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup SetupInvoke<T>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd(typeof(T), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<T>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke();
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<T>() =>
				((InterfaceMock)@this).SetupInvoke<T>();

			public void VerifyInvoke<T>(in Times times) =>
				((InterfaceMock)@this).VerifyInvoke<T>(times);

			public void VerifyInvoke<T>(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvoke<T>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T>()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithGenerics()
	{
		const string method = "void Invoke<T1, T2>();";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), Setup>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public Setup SetupInvoke<T1, T2>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke();
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<T1, T2>() =>
				((InterfaceMock)@this).SetupInvoke<T1, T2>();

			public void VerifyInvoke<T1, T2>(in Times times) =>
				((InterfaceMock)@this).VerifyInvoke<T1, T2>(times);

			public void VerifyInvoke<T1, T2>(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvoke<T1, T2>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T1, T2>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClass()
	{
		const string method =
			"""
			public virtual void Invoke() {}
			protected virtual void Invoke2() {}
			public void Invoke3() {}
			""";

		const string methods =
			"""
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
				_invoke0Invocation ??= new Invocation("Class.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("Class.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation("Class.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				_mock._invoke0?.Invoke();
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke() =>
				((ClassMock)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassAbstract()
	{
		const string method =
			"""
			public abstract int Invoke();
			protected abstract decimal Invoke2();
			public decimal Invoke3() { return 12m; }
			""";

		const string methods =
			"""
			// Invoke
			private Setup<int>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<int> SetupInvoke()
			{
				_invoke0 ??= new Setup<int>();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation("Class.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("Class.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override int Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation("Class.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}

			protected override decimal Invoke2() {return default!;}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, int, System.Func<int>> SetupInvoke() =>
				((ClassMock)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassReturn()
	{
		const string method =
			"""
			public virtual decimal Invoke() { return 0m; }
			protected virtual decimal Invoke2() { return 0m; }
			public decimal Invoke3() { return 12m; }
			""";

		const string methods =
			"""
			// Invoke
			private Setup<decimal>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<decimal> SetupInvoke()
			{
				_invoke0 ??= new Setup<decimal>();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation("Class.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("Class.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override decimal Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation("Class.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvoke() =>
				((ClassMock)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassWithGeneric()
	{
		const string method =
			"""
			public virtual void Invoke<T>() {}
			protected virtual void Invoke2<T>() {}
			public void Invoke3<T>() {}
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup SetupInvoke<T>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd(typeof(T), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<T>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"Class.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"Class.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke<T>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"Class.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke();
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<T>() =>
				((ClassMock)@this).SetupInvoke<T>();

			public void VerifyInvoke<T>(in Times times) =>
				((ClassMock)@this).VerifyInvoke<T>(times);

			public void VerifyInvoke<T>(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke<T>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T>()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke<T>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassReturnWithGeneric()
	{
		const string method =
			"""
			public virtual TReturn Invoke<T, TReturn>() { return default!; }
			protected virtual TReturn Invoke2<T, TReturn>() { return default!; }
			public TReturn Invoke3<T, TReturn>() { return default!; }
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public Setup<TReturn> SetupInvoke<T, TReturn>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (Setup<TReturn>)_invoke0.GetOrAdd((typeof(T), typeof(TReturn)), static _ => new Setup<TReturn>());
				return invoke0;
			}

			public void VerifyInvoke<T, TReturn>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T, TReturn>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override TReturn Invoke<T, TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<TReturn>?)_mock._invoke0?.ValueOrDefault((typeof(T), typeof(TReturn))))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvoke<T, TReturn>() =>
				((ClassMock)@this).SetupInvoke<T, TReturn>();

			public void VerifyInvoke<T, TReturn>(in Times times) =>
				((ClassMock)@this).VerifyInvoke<T, TReturn>(times);

			public void VerifyInvoke<T, TReturn>(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke<T, TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T, TReturn>()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke<T, TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassWithGenerics()
	{
		const string method =
			"""
			public virtual void Invoke<T1, T2>() {}
			protected virtual void Invoke2<T1, T2>() {}
			public void Invoke3<T1, T2>() {}
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), Setup>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public Setup SetupInvoke<T1, T2>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke();
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<T1, T2>() =>
				((ClassMock)@this).SetupInvoke<T1, T2>();

			public void VerifyInvoke<T1, T2>(in Times times) =>
				((ClassMock)@this).VerifyInvoke<T1, T2>(times);

			public void VerifyInvoke<T1, T2>(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke<T1, T2>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke<T1, T2>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassWithGenericAbstract()
	{
		const string method =
			"""
			public abstract void Invoke<T>();
			protected abstract void Invoke2<T>();
			public void Invoke3<T>() {}
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup SetupInvoke<T>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd(typeof(T), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<T>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"Class.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"Class.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke<T>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"Class.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke();
			}

			protected override void Invoke2<T>() {}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<T>() =>
				((ClassMock)@this).SetupInvoke<T>();

			public void VerifyInvoke<T>(in Times times) =>
				((ClassMock)@this).VerifyInvoke<T>(times);

			public void VerifyInvoke<T>(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke<T>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T>()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke<T>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassReturnWithGenericAbstract()
	{
		const string method =
			"""
			public abstract TReturn Invoke<T, TReturn>();
			protected abstract TReturn Invoke2<T, TReturn>();
			public TReturn Invoke3<T, TReturn>() { return default!; }
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public Setup<TReturn> SetupInvoke<T, TReturn>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (Setup<TReturn>)_invoke0.GetOrAdd((typeof(T), typeof(TReturn)), static _ => new Setup<TReturn>());
				return invoke0;
			}

			public void VerifyInvoke<T, TReturn>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T, TReturn>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override TReturn Invoke<T, TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<TReturn>?)_mock._invoke0?.ValueOrDefault((typeof(T), typeof(TReturn))))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}

			protected override TReturn Invoke2<T, TReturn>() {return default!;}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvoke<T, TReturn>() =>
				((ClassMock)@this).SetupInvoke<T, TReturn>();

			public void VerifyInvoke<T, TReturn>(in Times times) =>
				((ClassMock)@this).VerifyInvoke<T, TReturn>(times);

			public void VerifyInvoke<T, TReturn>(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke<T, TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T, TReturn>()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke<T, TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassWithGenericsAbstract()
	{
		const string method =
			"""
			public abstract void Invoke<T1, T2>();
			protected abstract void Invoke2<T1, T2>();
			public void Invoke3<T1, T2>() {}
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), Setup>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public Setup SetupInvoke<T1, T2>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"Class.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke();
			}

			protected override void Invoke2<T1, T2>() {}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<T1, T2>() =>
				((ClassMock)@this).SetupInvoke<T1, T2>();

			public void VerifyInvoke<T1, T2>(in Times times) =>
				((ClassMock)@this).VerifyInvoke<T1, T2>(times);

			public void VerifyInvoke<T1, T2>(System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke<T1, T2>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke<T1, T2>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
