namespace MyNihongo.Mockurai.Tests.PropertyTests;

public sealed class PropertyShould : PropertyTestsBase
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
				_property0GetInvocation ??= new Invocation("IInterface.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation("IInterface.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
			}
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action, int, System.Func<int>> SetupGetProperty() =>
				((InterfaceMock)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((InterfaceMock)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyGetProperty(times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0GetInvocation;";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				set
				{
					_mock._property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
				((InterfaceMock)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<int> value, in Times times) =>
				((InterfaceMock)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
				((InterfaceMock)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void SetProperty(in It<int> value)
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0SetInvocation;";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				init
				{
					_mock._property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
					_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
					_mock._property0Set?.Invoke(value);
				}
			}
			""";

		const string extensions =
			"""
			// Property
			public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
				((InterfaceMock)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<int> value, in Times times) =>
				((InterfaceMock)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
				((InterfaceMock)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void SetProperty(in It<int> value)
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0SetInvocation;";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
				_property0GetInvocation ??= new Invocation("IInterface.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface.Property.get");
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
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation("IInterface.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				set
				{
					_mock._property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
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
				((InterfaceMock)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((InterfaceMock)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyGetProperty(times());

			public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
				((InterfaceMock)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<int> value, in Times times) =>
				((InterfaceMock)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
				((InterfaceMock)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void SetProperty(in It<int> value)
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
				_property0GetInvocation ??= new Invocation("IInterface.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface.Property.get");
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
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public int Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation("IInterface.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				init
				{
					_mock._property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
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
				((InterfaceMock)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((InterfaceMock)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyGetProperty(times());

			public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
				((InterfaceMock)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<int> value, in Times times) =>
				((InterfaceMock)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
				((InterfaceMock)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void SetProperty(in It<int> value)
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassPropertyGetSetWithRecord()
	{
		const string property = "public abstract Record Property { get; set; }";

		const string methods =
			"""
			// Property
			private Setup<MyNihongo.Example.Tests.Record>? _property0Get;
			private Invocation? _property0GetInvocation;
			private SetupWithParameter<MyNihongo.Example.Tests.Record>? _property0Set;
			private Invocation<MyNihongo.Example.Tests.Record>? _property0SetInvocation;

			public Setup<MyNihongo.Example.Tests.Record> SetupGetProperty()
			{
				_property0Get ??= new Setup<MyNihongo.Example.Tests.Record>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation("Class.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("Class.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}

			public SetupWithParameter<MyNihongo.Example.Tests.Record> SetupSetProperty(in It<MyNihongo.Example.Tests.Record> value)
			{
				_property0Set ??= new SetupWithParameter<MyNihongo.Example.Tests.Record>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<MyNihongo.Example.Tests.Record> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<MyNihongo.Example.Tests.Record>("Class.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<MyNihongo.Example.Tests.Record> value, long index)
			{
				_property0SetInvocation ??= new Invocation<MyNihongo.Example.Tests.Record>("Class.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override MyNihongo.Example.Tests.Record Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation("Class.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				set
				{
					_mock._property0SetInvocation ??= new Invocation<MyNihongo.Example.Tests.Record>("Class.Property.set = {0}");
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
			public ISetup<System.Action, MyNihongo.Example.Tests.Record, System.Func<MyNihongo.Example.Tests.Record>> SetupGetProperty() =>
				((ClassMock)@this).SetupGetProperty();

			public void VerifyGetProperty(in Times times) =>
				((ClassMock)@this).VerifyGetProperty(times);

			public void VerifyGetProperty(System.Func<Times> times) =>
				((ClassMock)@this).VerifyGetProperty(times());

			public ISetup<System.Action<MyNihongo.Example.Tests.Record>> SetupSetProperty(in It<MyNihongo.Example.Tests.Record> value = default) =>
				((ClassMock)@this).SetupSetProperty(value);

			public void VerifySetProperty(in It<MyNihongo.Example.Tests.Record> value, in Times times) =>
				((ClassMock)@this).VerifySetProperty(value, times);

			public void VerifySetProperty(in It<MyNihongo.Example.Tests.Record> value, System.Func<Times> times) =>
				((ClassMock)@this).VerifySetProperty(value, times());
			""";

		const string extensionsSequence =
			"""
			// Property
			public void GetProperty()
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void SetProperty(in It<MyNihongo.Example.Tests.Record> value)
			{
				var nextIndex = ((ClassMock)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateClassTestCode(property, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
