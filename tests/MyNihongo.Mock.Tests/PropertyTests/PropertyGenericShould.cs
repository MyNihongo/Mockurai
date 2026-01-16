namespace MyNihongo.Mock.Tests.PropertyTests;

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
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = $"public {property}";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

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
				_property0SetInvocation ??= new Invocation<int>("IInterface<T>.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface<T>.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy = "public int Property { get; set; }";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

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
				_property0SetInvocation ??= new Invocation<int>("IInterface<T>.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface<T>.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy = "public int Property { get; init; }";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

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
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
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
				_property0SetInvocation ??= new Invocation<int>("IInterface<T>.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface<T>.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy = "public int Property { get; set; }";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

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
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
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
				_property0SetInvocation ??= new Invocation<int>("IInterface<T>.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface<T>.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy = "public int Property { get; init; }";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
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
				_property0SetInvocation ??= new Invocation<T>("IInterface<T>.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<T> value, long index)
			{
				_property0SetInvocation ??= new Invocation<T>("IInterface<T>.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy = "public T Property { get; init; }";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface<T>.Property.get");
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
				_property0SetInvocation ??= new Invocation<T?>("IInterface<T>.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<T?> value, long index)
			{
				_property0SetInvocation ??= new Invocation<T?>("IInterface<T>.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy = "public T? Property { get; set; }";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassPropertyGetSetWithRecord()
	{
		const string property = "public abstract Record<T?> Property { get; set; }";

		const string methods =
			"""
			// Property
			private Setup<MyNihongo.Mock.Tests.Record<T?>>? _property0Get;
			private Invocation? _property0GetInvocation;
			private SetupWithParameter<MyNihongo.Mock.Tests.Record<T?>>? _property0Set;
			private Invocation<MyNihongo.Mock.Tests.Record<T?>>? _property0SetInvocation;

			public Setup<MyNihongo.Mock.Tests.Record<T?>> SetupGetProperty()
			{
				_property0Get ??= new Setup<MyNihongo.Mock.Tests.Record<T?>>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation("Class<T>.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("Class<T>.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}

			public SetupWithParameter<MyNihongo.Mock.Tests.Record<T?>> SetupSetProperty(in It<MyNihongo.Mock.Tests.Record<T?>> value)
			{
				_property0Set ??= new SetupWithParameter<MyNihongo.Mock.Tests.Record<T?>>();
				_property0Set.SetupParameter(value.ValueSetup);
				return _property0Set;
			}

			public void VerifySetProperty(in It<MyNihongo.Mock.Tests.Record<T?>> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<MyNihongo.Mock.Tests.Record<T?>>("Class<T>.Property.set = {0}");
				_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<MyNihongo.Mock.Tests.Record<T?>> value, long index)
			{
				_property0SetInvocation ??= new Invocation<MyNihongo.Mock.Tests.Record<T?>>("Class<T>.Property.set = {0}");
				return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy = "public override MyNihongo.Mock.Tests.Record<T?> Property { get; set; }";

		var testCode = CreateClassTestCode(property, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
