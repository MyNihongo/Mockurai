using System.Dynamic;

namespace MyNihongo.Mockurai.MicrosoftExtensions.Configuration.Tests.MockConfigurationExTests;

public sealed class SetupConfigurationShould : MockConfigurationExTestsBase
{
	[Fact]
	public void SetupRootValue()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new
			{
				Value = "value",
			});

		var actual = CreateFixture();
		Assert.Equal("value", actual["Value"]);
	}

	[Fact]
	public void SetupRootValueSection()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new
			{
				Value = "value",
			});

		var actual = CreateFixture()
			.GetSection("Value");

		Assert.Equal("value", actual.Value);
		Assert.Equal("Value", actual.Key);
		Assert.Equal("Value", actual.Path);
	}

	[Fact]
	public void AccessValuesByPath()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(CreateConfigurationObject());

		var actual = CreateFixture();

		// First level
		Assert.Equal("string", actual["String"]);
		Assert.Equal(1.23f, actual.GetValue<float>("ObjectArray:0:Float"));
		Assert.Equal("string1", actual.GetValue<string>("ObjectArray:0:String"));
		Assert.Equal(4.56f, actual.GetValue<float>("ObjectArray:1:Float"));
		Assert.Equal("string2", actual.GetValue<string>("ObjectArray:1:String"));

		// Second level
		Assert.True(actual.GetValue<bool>("Object:Bool"));
		Assert.Equal([1, 2], actual.GetSection("Object:IntArray").Get<int[]>()!);
		Assert.Equal(1, actual.GetValue<int>("Object:IntArray:0"));
		Assert.Equal(2, actual.GetValue<int>("Object:IntArray:1"));

		// Third level
		Assert.Equal(1UL, actual.GetValue<ulong>("Object:NestedObject:Ulong"));

		// Fourth level
		Assert.Equal(123u, actual.GetValue<uint>("Object:NestedObject:DeepObject:Uint"));
		Assert.Equal([12.3d, 45.6d], actual.GetSection("Object:NestedObject:DeepObject:DoubleArray").Get<double[]>()!);
		Assert.Equal(12.3d, actual.GetValue<double>("Object:NestedObject:DeepObject:DoubleArray:0"));
		Assert.Equal(45.6d, actual.GetValue<double>("Object:NestedObject:DeepObject:DoubleArray:1"));
	}

	[Fact]
	public void AccessValuesBySection()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(CreateConfigurationObject());

		var actual = CreateFixture();

		// First level
		var arraySection = actual.GetSection("ObjectArray");
		Assert.Equal(1.23f, arraySection.GetValue<float>("0:Float"));
		Assert.Equal("string1", arraySection.GetValue<string>("0:String"));
		Assert.Equal(4.56f, arraySection.GetValue<float>("1:Float"));
		Assert.Equal("string2", arraySection.GetValue<string>("1:String"));

		// Second level
		var secondSection = actual.GetSection("Object");
		Assert.True(secondSection.GetValue<bool>("Bool"));

		var intSection = secondSection.GetSection("IntArray");
		Assert.Equal(1, intSection.GetValue<int>("0"));
		Assert.Equal(2, intSection.GetValue<int>("1"));

		// Third level
		var thirdSection = secondSection.GetSection("NestedObject");
		Assert.Equal(1UL, thirdSection.GetValue<ulong>("Ulong"));

		// Fourth level
		var fourthSection = thirdSection.GetSection("DeepObject");
		Assert.Equal("Object:NestedObject:DeepObject", fourthSection.Path);
		Assert.Equal("DeepObject", fourthSection.Key);
		Assert.Equal(123u, fourthSection.GetValue<uint>("Uint"));

		var doubleSection = fourthSection.GetSection("DoubleArray");
		Assert.Equal(12.3d, doubleSection.GetValue<double>("0"));
		Assert.Equal(45.6d, doubleSection.GetValue<double>("1"));
	}

	[Fact]
	public void SetupChildren()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new
			{
				First = 1,
				Second = new { Nested = "nested" },
			});

		var actual = CreateFixture().GetChildren().ToArray();

		Assert.Collection(
			actual,
			x =>
			{
				Assert.Equal("First", x.Key);
				Assert.Equal("1", x.Value);
			},
			x =>
			{
				Assert.Equal("Second", x.Key);
				Assert.Null(x.Value);
				Assert.Equal("Second:Nested", Assert.Single(x.GetChildren()).Path);
			});
	}

	[Fact]
	public void BindOptions()
	{
		var expected = new TestOptions
		{
			Name = "name",
			Count = 3,
			Ratio = 0.5m,
			Enabled = true,
			Kind = TestKind.Second,
			Timeout = TimeSpan.FromSeconds(30),
			Date = new DateTime(2026, 11, 12, 10, 20, 30, DateTimeKind.Unspecified),
			Id = Guid.Parse("8B0B7E0A-6F43-4F0A-9C8C-1C2D3E4F5A6B"),
			Url = new Uri("https://example.com/path"),
			Tags = ["a", "b"],
			Nested = new TestNestedOptions { Value = "nested" },
			Map = new Dictionary<string, int> { ["one"] = 1, ["two"] = 2 },
		};

		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Options = expected });

		var section = CreateFixture().GetSection("Options");

		var actualGet = section.Get<TestOptions>();
		Assert.Equivalent(expected, actualGet, strict: true);

		var actualBind = new TestOptions();
		section.Bind(actualBind);
		Assert.Equivalent(expected, actualBind, strict: true);
	}

	[Fact]
	public void SetupConnectionStrings()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new
			{
				ConnectionStrings = new { Default = "Server=.;Database=db" },
			});

		var actual = CreateFixture().GetConnectionString("Default");
		Assert.Equal("Server=.;Database=db", actual);
	}

	[Fact]
	public void SetupDictionaryRoot()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new Dictionary<string, object?>
			{
				["Value"] = 1,
				["Section"] = new Dictionary<string, string> { ["Key"] = "value" },
				["Null"] = null,
			});

		var actual = CreateFixture();

		Assert.Equal("1", actual["Value"]);
		Assert.Equal("value", actual["Section:Key"]);
		Assert.Null(actual["Null"]);
	}

	[Fact]
	public void SetupExpandoObjectRoot()
	{
		dynamic expando = new ExpandoObject();
		expando.Value = "value";
		expando.Section = new { Key = 2 };

		ConfigurationMock
			.SetupConfiguration()
			.Returns((object)expando);

		var actual = CreateFixture();

		Assert.Equal("value", actual["Value"]);
		Assert.Equal(2, actual.GetValue<int>("Section:Key"));
	}

	[Fact]
	public void IgnoreKeyCase()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Section = new { Value = "value" } });

		var actual = CreateFixture();

		Assert.Equal("value", actual["section:VALUE"]);
		Assert.Equal("value", actual.GetSection("SECTION").GetSection("value").Value);
	}

	[Fact]
	public void SerializeValuesWithInvariantCulture()
	{
		var originalCulture = Thread.CurrentThread.CurrentCulture;
		Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("lv-LV");

		try
		{
			ConfigurationMock
				.SetupConfiguration()
				.Returns(new { Double = 1.5d, Decimal = 2.25m, Float = 3.5f });

			var actual = CreateFixture();

			Assert.Equal("1.5", actual["Double"]);
			Assert.Equal("2.25", actual["Decimal"]);
			Assert.Equal("3.5", actual["Float"]);
		}
		finally
		{
			Thread.CurrentThread.CurrentCulture = originalCulture;
		}
	}

	[Fact]
	public void SetupNullCollectionItems()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Items = new[] { "a", null, "c" } });

		var actual = CreateFixture().GetSection("Items");

		Assert.Equal(["0", "1", "2"], actual.GetChildren().Select(x => x.Key));
		Assert.Null(actual["1"]);
		Assert.Equal("c", actual["2"]);
	}

	[Fact]
	public void ReturnEmptySectionIfNotFound()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Section = new { Value = "value" } });

		var actual = CreateFixture().GetSection("Section:Missing:Deep");

		Assert.Equal("Deep", actual.Key);
		Assert.Equal("Section:Missing:Deep", actual.Path);
		Assert.Null(actual.Value);
		Assert.Empty(actual.GetChildren());
		Assert.False(actual.Exists());
	}

	[Fact]
	public void ReturnNullValueIfNotFound()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Value = "value" });

		var actual = CreateFixture();

		Assert.Null(actual["Missing"]);
		Assert.Equal(42, actual.GetValue("Missing", 42));
	}

	[Fact]
	public void SetupEmptyConfigurationIfNull()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(null);

		var actual = CreateFixture();

		Assert.Empty(actual.GetChildren());
		Assert.Null(actual["Value"]);
		Assert.False(actual.GetSection("Value").Exists());
	}

	[Fact]
	public void ReplacePreviousSetup()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { First = "first" });

		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Second = "second" });

		var actual = CreateFixture();

		Assert.Null(actual["First"]);
		Assert.Equal("second", actual["Second"]);
		Assert.Equal("Second", Assert.Single(actual.GetChildren()).Key);
	}

	[Fact]
	public void SetValueByIndexer()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Section = new { Value = "value" } });

		var actual = CreateFixture();
		actual["Section:Value"] = "updated";
		actual["Section:New"] = "new";

		Assert.Equal("updated", actual["Section:Value"]);
		Assert.Equal("new", actual.GetSection("Section").GetSection("New").Value);
	}

	[Fact]
	public void ReturnReloadToken()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Value = "value" });

		var actual = CreateFixture().GetReloadToken();

		Assert.False(actual.HasChanged);
		actual.RegisterChangeCallback(_ => { }, null).Dispose();
	}

	[Fact]
	public void VerifyInvocations()
	{
		ConfigurationMock
			.SetupConfiguration()
			.Returns(new { Value = "value" });

		_ = CreateFixture()["Value"];
		_ = CreateFixture().GetSection("Value");

		ConfigurationMock.VerifyGetIndexer("Value", Times.Once);
		ConfigurationMock.VerifyGetSection("Value", Times.Once);
		ConfigurationMock.VerifyNoOtherCalls();
	}

	[Theory]
	[InlineData(1)]
	[InlineData("string")]
	public void ThrowIfScalarRoot(object value)
	{
		var setup = ConfigurationMock.SetupConfiguration();
		Assert.Throws<InvalidOperationException>(() => setup.Returns(value));
	}

	[Fact]
	public void ThrowIfCollectionRoot()
	{
		var setup = ConfigurationMock.SetupConfiguration();
		Assert.Throws<InvalidOperationException>(() => setup.Returns(new[] { 1, 2 }));
	}

	private static object CreateConfigurationObject() =>
		new
		{
			String = "string",
			ObjectArray = new[]
			{
				new { Float = 1.23f, String = "string1" },
				new { Float = 4.56f, String = "string2" },
			},
			Object = new
			{
				Bool = true,
				IntArray = new[] { 1, 2 },
				NestedObject = new
				{
					Ulong = 1UL,
					DeepObject = new
					{
						Uint = 123u,
						DoubleArray = new[] { 12.3d, 45.6d },
					},
				},
			},
		};

	public enum TestKind
	{
		First,
		Second,
	}

	public sealed class TestOptions
	{
		public string Name { get; set; } = string.Empty;

		public int Count { get; set; }

		public decimal Ratio { get; set; }

		public bool Enabled { get; set; }

		public TestKind Kind { get; set; }

		public TimeSpan Timeout { get; set; }

		public DateTime Date { get; set; }

		public Guid Id { get; set; }

		public Uri? Url { get; set; }

		public string[] Tags { get; set; } = [];

		public TestNestedOptions? Nested { get; set; }

		public Dictionary<string, int> Map { get; set; } = [];
	}

	public sealed class TestNestedOptions
	{
		public string Value { get; set; } = string.Empty;
	}
}
