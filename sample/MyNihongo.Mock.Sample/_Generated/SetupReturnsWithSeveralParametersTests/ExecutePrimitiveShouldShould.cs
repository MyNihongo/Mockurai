namespace MyNihongo.Mock.Sample._Generated.SetupReturnsWithSeveralParametersTests;

public sealed class ExecutePrimitiveShouldShould : SetupReturnsWithSeveralParametersTestsBase
{
	[Fact]
	public void ReturnForAnySetup()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 12345678, inputValue2 = 987654321;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnForValueSetup()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(setupValue1, setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnNullForValueSetup1()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 84837621;
		var hasValue = fixture.Execute(inputValue1, setupValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForValueSetup1()
	{
		const int returnValue = -731473432;
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupIntInt<int>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 84837621;
		var hasValue = fixture.Execute(inputValue1, setupValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForValueSetup2()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue2 = 84837621;
		var hasValue = fixture.Execute(setupValue1, inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForValueSetup2()
	{
		const int returnValue = -731473432;
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupIntInt<int>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue2 = 84837621;
		var hasValue = fixture.Execute(setupValue1, inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForValueSetup1And2()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = -771245, inputValue2 = 84837621;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForValueSetup1And2()
	{
		const int returnValue = -731473432;
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupIntInt<int>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = -771245, inputValue2 = 84837621;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ReturnForWhereSetup1(int inputValue1)
	{
		const string returnValue = nameof(returnValue);
		const int setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(inputValue1, setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnNullForWhereSetup1(int inputValue2)
	{
		const string returnValue = nameof(returnValue);
		const int setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(inputValue2, setupValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetup1(int inputValue1)
	{
		const int returnValue = 12345;
		const int setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixtureInt<SetupIntInt<int>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(inputValue1, setupValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ReturnForWhereSetup2(int inputValue2)
	{
		const string returnValue = nameof(returnValue);
		const int setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(setupValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnNullForWhereSetup2(int inputValue2)
	{
		const string returnValue = nameof(returnValue);
		const int setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(setupValue1, inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetup2(int inputValue2)
	{
		const int returnValue = 12345;
		const int setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupIntInt<int>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(setupValue1, inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnForWhereSetups()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 101, inputValue2 = 9;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnNullForWhereSetups1()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 99, inputValue2 = 9;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForWhereSetups1()
	{
		const int returnValue = 374452;
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupIntInt<int>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 99, inputValue2 = 9;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnNullForWhereSetups2()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 101, inputValue2 = 11;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForWhereSetups2()
	{
		const int returnValue = 374452;
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupIntInt<int>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 101, inputValue2 = 11;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}
}
