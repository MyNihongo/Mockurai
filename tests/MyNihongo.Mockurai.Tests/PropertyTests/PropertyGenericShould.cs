namespace MyNihongo.Mockurai.Tests.PropertyTests;

public sealed class PropertyGenericShould : PropertyGenericTestsBase
{
	[Fact]
	public async Task GenerateInterfacePropertyGet()
	{
		const string property = "int Property { get; }";

		const string methods =
			"""
			// Property
			private Setup<int>? _property0Get;
			private Invocation? _property0GetInvocation;

			public Setup<int> SetupGetProperty()
			{
				_property0Get ??= new Setup<int>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
			}
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action, int, System.Func<int>> SetupGetProperty() =>
				((InterfaceMock<T>)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0GetInvocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> PropertyGet => _mock._property0GetInvocation?.GetInvocations() ?? [];";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfacePropertySet()
	{
		const string property = "int Property { set; }";

		const string methods =
			"""
			// Property
			private SetupWithParameter<int>? _property0Set;
			private Invocation<int>? _property0SetInvocation;

			public SetupWithParameter<int> SetupSetProperty(in It<int> value)
			{
				_property0Set ??= new SetupWithParameter<int>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<int> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				set
				{
					_mock._property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
				((InterfaceMock<T>)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<int> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void SetProperty(in It<int> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0SetInvocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfacePropertyInit()
	{
		const string property = "int Property { init; }";

		const string methods =
			"""
			// Property
			private SetupWithParameter<int>? _property0Set;
			private Invocation<int>? _property0SetInvocation;

			public SetupWithParameter<int> SetupSetProperty(in It<int> value)
			{
				_property0Set ??= new SetupWithParameter<int>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<int> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				init
				{
					_mock._property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
				((InterfaceMock<T>)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<int> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void SetProperty(in It<int> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0SetInvocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfacePropertyGetSet()
	{
		const string property = "int Property { get; set; }";

		const string methods =
			"""
			// Property
			private Setup<int>? _property0Get;
			private Invocation? _property0GetInvocation;
			private SetupWithParameter<int>? _property0Set;
			private Invocation<int>? _property0SetInvocation;

			public Setup<int> SetupGetProperty()
			{
				_property0Get ??= new Setup<int>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}

			public SetupWithParameter<int> SetupSetProperty(in It<int> value)
			{
				_property0Set ??= new SetupWithParameter<int>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<int> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				set
				{
					_mock._property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _property0GetInvocation;
			yield return _property0SetInvocation;
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action, int, System.Func<int>> SetupGetProperty() =>
				((InterfaceMock<T>)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times());

			public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
				((InterfaceMock<T>)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<int> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void SetProperty(in It<int> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> PropertyGet => _mock._property0GetInvocation?.GetInvocations() ?? [];

			public System.Collections.Generic.IEnumerable<IInvocation<int>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];
			""";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfacePropertyGetInit()
	{
		const string property = "int Property { get; init; }";

		const string methods =
			"""
			// Property
			private Setup<int>? _property0Get;
			private Invocation? _property0GetInvocation;
			private SetupWithParameter<int>? _property0Set;
			private Invocation<int>? _property0SetInvocation;

			public Setup<int> SetupGetProperty()
			{
				_property0Get ??= new Setup<int>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}

			public SetupWithParameter<int> SetupSetProperty(in It<int> value)
			{
				_property0Set ??= new SetupWithParameter<int>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<int> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				init
				{
					_mock._property0SetInvocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _property0GetInvocation;
			yield return _property0SetInvocation;
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action, int, System.Func<int>> SetupGetProperty() =>
				((InterfaceMock<T>)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times());

			public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
				((InterfaceMock<T>)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<int> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void SetProperty(in It<int> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> PropertyGet => _mock._property0GetInvocation?.GetInvocations() ?? [];

			public System.Collections.Generic.IEnumerable<IInvocation<int>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];
			""";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceGenericProperty()
	{
		const string property = "T Property { get; init; }";

		const string methods =
			"""
			// Property
			private Setup<T>? _property0Get;
			private Invocation? _property0GetInvocation;
			private SetupWithParameter<T>? _property0Set;
			private Invocation<T>? _property0SetInvocation;

			public Setup<T> SetupGetProperty()
			{
				_property0Get ??= new Setup<T>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}

			public SetupWithParameter<T> SetupSetProperty(in It<T> value)
			{
				_property0Set ??= new SetupWithParameter<T>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<T> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<T> value, long index)
			{
				_property0SetInvocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				init
				{
					_mock._property0SetInvocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _property0GetInvocation;
			yield return _property0SetInvocation;
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action, T, System.Func<T>> SetupGetProperty() =>
				((InterfaceMock<T>)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times());

			public ISetup<System.Action<T>> SetupSetProperty(in It<T> value = default) =>
				((InterfaceMock<T>)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void SetProperty(in It<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> PropertyGet => _mock._property0GetInvocation?.GetInvocations() ?? [];

			public System.Collections.Generic.IEnumerable<IInvocation<T>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];
			""";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceGenericNullableProperty()
	{
		const string property = "T? Property { get; set; }";

		const string methods =
			"""
			// Property
			private Setup<T?>? _property0Get;
			private Invocation? _property0GetInvocation;
			private SetupWithParameter<T?>? _property0Set;
			private Invocation<T?>? _property0SetInvocation;

			public Setup<T?> SetupGetProperty()
			{
				_property0Get ??= new Setup<T?>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}

			public SetupWithParameter<T?> SetupSetProperty(in It<T?> value)
			{
				_property0Set ??= new SetupWithParameter<T?>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<T?> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<T?>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<T?> value, long index)
			{
				_property0SetInvocation ??= new Invocation<T?>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T? Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				set
				{
					_mock._property0SetInvocation ??= new Invocation<T?>($"IInterface<{typeof(T).Name}>.Property.set = {{0}}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _property0GetInvocation;
			yield return _property0SetInvocation;
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action, T?, System.Func<T?>> SetupGetProperty() =>
				((InterfaceMock<T>)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyGetProperty(times());

			public ISetup<System.Action<T?>> SetupSetProperty(in It<T?> value = default) =>
				((InterfaceMock<T>)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<T?> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<T?> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void SetProperty(in It<T?> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> PropertyGet => _mock._property0GetInvocation?.GetInvocations() ?? [];

			public System.Collections.Generic.IEnumerable<IInvocation<T?>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];
			""";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassPropertyGetSetWithRecord()
	{
		const string property = "public abstract Record<T?> Property { get; set; }";

		const string methods =
			"""
			// Property
			private Setup<MyNihongo.Example.Tests.Record<T?>>? _property0Get;
			private Invocation? _property0GetInvocation;
			private SetupWithParameter<MyNihongo.Example.Tests.Record<T?>>? _property0Set;
			private Invocation<MyNihongo.Example.Tests.Record<T?>>? _property0SetInvocation;

			public Setup<MyNihongo.Example.Tests.Record<T?>> SetupGetProperty()
			{
				_property0Get ??= new Setup<MyNihongo.Example.Tests.Record<T?>>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation($"Class<{typeof(T).Name}>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation($"Class<{typeof(T).Name}>.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}

			public SetupWithParameter<MyNihongo.Example.Tests.Record<T?>> SetupSetProperty(in It<MyNihongo.Example.Tests.Record<T?>> value)
			{
				_property0Set ??= new SetupWithParameter<MyNihongo.Example.Tests.Record<T?>>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<MyNihongo.Example.Tests.Record<T?>> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<MyNihongo.Example.Tests.Record<T?>>($"Class<{typeof(T).Name}>.Property.set = {{0}}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<MyNihongo.Example.Tests.Record<T?>> value, long index)
			{
				_property0SetInvocation ??= new Invocation<MyNihongo.Example.Tests.Record<T?>>($"Class<{typeof(T).Name}>.Property.set = {{0}}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override MyNihongo.Example.Tests.Record<T?> Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation($"Class<{typeof(T).Name}>.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				set
				{
					_mock._property0SetInvocation ??= new Invocation<MyNihongo.Example.Tests.Record<T?>>($"Class<{typeof(T).Name}>.Property.set = {{0}}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _property0GetInvocation;
			yield return _property0SetInvocation;
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action, MyNihongo.Example.Tests.Record<T?>, System.Func<MyNihongo.Example.Tests.Record<T?>>> SetupGetProperty() =>
				((ClassMock<T>)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((ClassMock<T>)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyGetProperty(times());

			public ISetup<System.Action<MyNihongo.Example.Tests.Record<T?>>> SetupSetProperty(in It<MyNihongo.Example.Tests.Record<T?>> value = default) =>
				((ClassMock<T>)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<MyNihongo.Example.Tests.Record<T?>> value, in Times times) =>
				((ClassMock<T>)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<MyNihongo.Example.Tests.Record<T?>> value, System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void SetProperty(in It<MyNihongo.Example.Tests.Record<T?>> value)
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> PropertyGet => _mock._property0GetInvocation?.GetInvocations() ?? [];

			public System.Collections.Generic.IEnumerable<IInvocation<MyNihongo.Example.Tests.Record<T?>>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];
			""";

		var testCode = CreateClassTestCode(property, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
