namespace MyNihongo.Mock.Tests.PropertyTests;

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

		const string verifyNoOtherCalls = "_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0GetInvocation;";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

		const string verifyNoOtherCalls = "_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0SetInvocation;";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

		const string verifyNoOtherCalls = "_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _property0SetInvocation;";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
			private Setup<MyNihongo.Mock.Tests.Record>? _property0Get;
			private Invocation? _property0GetInvocation;
			private SetupWithParameter<MyNihongo.Mock.Tests.Record>? _property0Set;
			private Invocation<MyNihongo.Mock.Tests.Record>? _property0SetInvocation;

			public Setup<MyNihongo.Mock.Tests.Record> SetupGetProperty()
			{
				_property0Get ??= new Setup<MyNihongo.Mock.Tests.Record>();
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

			public SetupWithParameter<MyNihongo.Mock.Tests.Record> SetupSetProperty(in It<MyNihongo.Mock.Tests.Record> value)
			{
				_property0Set ??= new SetupWithParameter<MyNihongo.Mock.Tests.Record>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<MyNihongo.Mock.Tests.Record> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<MyNihongo.Mock.Tests.Record>("Class.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<MyNihongo.Mock.Tests.Record> value, long index)
			{
				_property0SetInvocation ??= new Invocation<MyNihongo.Mock.Tests.Record>("Class.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override MyNihongo.Mock.Tests.Record Property
			{
				get
				{
					_mock._property0GetInvocation ??= new Invocation("Class.Property.get");
					_mock._property0GetInvocation.Register(_mock._invocationIndex);
					return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
				}
				set
				{
					_mock._property0SetInvocation ??= new Invocation<MyNihongo.Mock.Tests.Record>("Class.Property.set = {0}");
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

		var testCode = CreateClassTestCode(property, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
