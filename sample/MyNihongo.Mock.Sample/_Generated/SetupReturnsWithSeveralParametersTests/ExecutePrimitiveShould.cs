namespace MyNihongo.Mock.Sample._Generated.SetupReturnsWithSeveralParametersTests;

public sealed class ExecutePrimitiveShould : SetupReturnsWithSeveralParametersTestsBase
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

	[Fact]
	public void ReturnNullForWhereSetups1And2()
	{
		const string returnValue = nameof(returnValue);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForWhereSetups1And2()
	{
		const int returnValue = 374452;
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixtureInt<SetupIntInt<int>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Returns(returnValue);

		const int inputValue1 = 99, inputValue2 = 11;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Any(), setup2 = It<int>.Any();

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 12345678, inputValue2 = 987654321;
		Action actual = () => fixture.Execute(inputValue1, inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(setupValue1, setupValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowForValueSetup1()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 84837621;
		fixture.Execute(inputValue1, setupValue2, out _);
	}

	[Fact]
	public void NotThrowForValueSetup2()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue2 = 84837621;
		fixture.Execute(setupValue1, inputValue2, out _);
	}

	[Fact]
	public void NotThrowForValueSetupFor1And2()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 12345678, setupValue2 = 987654321;
		It<int> setup1 = setupValue1, setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = -771245, inputValue2 = 84837621;
		fixture.Execute(inputValue1, inputValue2, out _);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ThrowForWhereSetup1(int inputValue1)
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(inputValue1, setupValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotThrowForWhereSetup1(int inputValue1)
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue2 = 987654321;
		It<int> setup1 = It<int>.Where(static x => x <= 10), setup2 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Execute(inputValue1, setupValue2, out _);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ThrowForWhereSetup2(int inputValue2)
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(setupValue1, inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotThrowForWhereSetup2(int inputValue2)
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue1 = 987654321;
		It<int> setup1 = setupValue1, setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Execute(setupValue1, inputValue2, out _);
	}

	[Fact]
	public void ThrowForWhereSetups()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 101, inputValue2 = 9;
		Action actual = () => fixture.Execute(inputValue1, inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForWhereSetups1()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 99, inputValue2 = 9;
		fixture.Execute(inputValue1, inputValue2, out _);
	}

	[Fact]
	public void ThrowForWhereSetups2()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 101, inputValue2 = 11;
		fixture.Execute(inputValue1, inputValue2, out _);
	}

	[Fact]
	public void ThrowForWhereSetups1And2()
	{
		const string errorMessage = nameof(errorMessage);
		It<int> setup1 = It<int>.Where(static x => x >= 100), setup2 = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup1, setup2);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue1 = 99, inputValue2 = 11;
		fixture.Execute(inputValue1, inputValue2, out _);
	}

	[Fact]
	public void PrioritiseValueOverAnyReturn4()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		var hasValue = fixture.Execute(setupValue1, setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue4, actual);
	}

	[Fact]
	public void PrioritiseValueOverAnyReturn3()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		const int inputValue1 = 324343242;
		var hasValue = fixture.Execute(inputValue1, setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue3, actual);
	}

	[Fact]
	public void PrioritiseValueOverAnyReturn2()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		const int inputValue2 = 324343242;
		var hasValue = fixture.Execute(setupValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue2, actual);
	}

	[Fact]
	public void PrioritiseValueOverAnyReturn1()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		const int inputValue1 = 324343242, inputValue2 = 837483252;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue1, actual);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow4()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		Action actual = () => fixture.Execute(setupValue1, setupValue2, out _);

		var exception = Assert.Throws<OverflowException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow3()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		const int inputValue1 = 324343242;
		Action actual = () => fixture.Execute(inputValue1, setupValue2, out _);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow2()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		const int inputValue2 = 324343242;
		Action actual = () => fixture.Execute(setupValue1, inputValue2, out _);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverAnyThrow1()
	{
		const int setupValue1 = 1, setupValue2 = 2;

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupValue1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupValue2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupValue1, setup42 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		const int inputValue1 = 324343242, inputValue2 = 837483252;
		Action actual = () => fixture.Execute(inputValue1, inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns9()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		var hasValue = fixture.Execute(setupValue1, setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue9, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns8()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		const int inputValue1 = 25;
		var hasValue = fixture.Execute(inputValue1, setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue8, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns7()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		const int inputValue1 = -345332543;
		var hasValue = fixture.Execute(inputValue1, setupValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue7, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns6()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		const int inputValue2 = 21;
		var hasValue = fixture.Execute(setupValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue6, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns5()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		const int inputValue2 = -21;
		var hasValue = fixture.Execute(setupValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue5, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns4()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		const int inputValue1 = 14, inputValue2 = 21;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue4, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns3()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		const int inputValue1 = -14, inputValue2 = 21;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue3, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns2()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		const int inputValue1 = 14, inputValue2 = -21;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue2, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns1()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string returnValue1 = nameof(returnValue1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string returnValue3 = nameof(returnValue3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string returnValue4 = nameof(returnValue4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string returnValue5 = nameof(returnValue5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string returnValue6 = nameof(returnValue6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string returnValue7 = nameof(returnValue7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string returnValue8 = nameof(returnValue8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string returnValue9 = nameof(returnValue9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Returns(returnValue1);

		fixture.SetupParameters(setup21, setup22);
		fixture.Returns(returnValue2);

		fixture.SetupParameters(setup31, setup32);
		fixture.Returns(returnValue3);

		fixture.SetupParameters(setup41, setup42);
		fixture.Returns(returnValue4);

		fixture.SetupParameters(setup51, setup52);
		fixture.Returns(returnValue5);

		fixture.SetupParameters(setup61, setup62);
		fixture.Returns(returnValue6);

		fixture.SetupParameters(setup71, setup72);
		fixture.Returns(returnValue7);

		fixture.SetupParameters(setup81, setup82);
		fixture.Returns(returnValue8);

		fixture.SetupParameters(setup91, setup92);
		fixture.Returns(returnValue9);

		const int inputValue1 = -14, inputValue2 = -21;
		var hasValue = fixture.Execute(inputValue1, inputValue2, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue1, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow9()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		Action actual = () => fixture.Execute(setupValue1, setupValue2, out _);

		var exception = Assert.Throws<InvalidExpressionException>(actual);
		Assert.Equal(errorMessage9, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow8()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 25;
		Action actual = () => fixture.Execute(inputValue1, setupValue2, out _);

		var exception = Assert.Throws<ArithmeticException>(actual);
		Assert.Equal(errorMessage8, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow7()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -345332543;
		Action actual = () => fixture.Execute(inputValue1, setupValue2, out _);

		var exception = Assert.Throws<AggregateException>(actual);
		Assert.Equal(errorMessage7, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow6()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue2 = 21;
		Action actual = () => fixture.Execute(setupValue1, inputValue2, out _);

		var exception = Assert.Throws<FileLoadException>(actual);
		Assert.Equal(errorMessage6, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow5()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue2 = -21;
		Action actual = () => fixture.Execute(setupValue1, inputValue2, out _);

		var exception = Assert.Throws<MissingSatelliteAssemblyException>(actual);
		Assert.Equal(errorMessage5, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow4()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 14, inputValue2 = 21;
		Action actual = () => fixture.Execute(inputValue1, inputValue2, out _);

		var exception = Assert.Throws<OverflowException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow3()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -14, inputValue2 = 21;
		Action actual = () => fixture.Execute(inputValue1, inputValue2, out _);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow2()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = 14, inputValue2 = -21;
		Action actual = () => fixture.Execute(inputValue1, inputValue2, out _);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrow1()
	{
		const int setupValue1 = 1, setupValue2 = 2;
		It<int> setupWhere1 = It<int>.Where(x => x > 10), setupWhere2 = It<int>.Where(x => x > 20);

		const string errorMessage1 = nameof(errorMessage1);
		It<int> setup11 = It<int>.Any(), setup12 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		It<int> setup21 = setupWhere1, setup22 = default;

		const string errorMessage3 = nameof(errorMessage3);
		It<int> setup31 = default, setup32 = setupWhere2;

		const string errorMessage4 = nameof(errorMessage4);
		It<int> setup41 = setupWhere1, setup42 = setupWhere2;

		const string errorMessage5 = nameof(errorMessage5);
		It<int> setup51 = setupValue1, setup52 = default;

		const string errorMessage6 = nameof(errorMessage6);
		It<int> setup61 = setupValue1, setup62 = setupWhere2;

		const string errorMessage7 = nameof(errorMessage7);
		It<int> setup71 = default, setup72 = setupValue2;

		const string errorMessage8 = nameof(errorMessage8);
		It<int> setup81 = setupWhere1, setup82 = setupValue2;

		const string errorMessage9 = nameof(errorMessage9);
		It<int> setup91 = setupValue1, setup92 = setupValue2;

		var fixture = CreateFixture<SetupIntInt<string>>();
		fixture.SetupParameters(setup11, setup12);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameters(setup21, setup22);
		fixture.Throws(new ArgumentException(errorMessage2));

		fixture.SetupParameters(setup31, setup32);
		fixture.Throws(new InvalidCastException(errorMessage3));

		fixture.SetupParameters(setup41, setup42);
		fixture.Throws(new OverflowException(errorMessage4));

		fixture.SetupParameters(setup51, setup52);
		fixture.Throws(new MissingSatelliteAssemblyException(errorMessage5));

		fixture.SetupParameters(setup61, setup62);
		fixture.Throws(new FileLoadException(errorMessage6));

		fixture.SetupParameters(setup71, setup72);
		fixture.Throws(new AggregateException(errorMessage7));

		fixture.SetupParameters(setup81, setup82);
		fixture.Throws(new ArithmeticException(errorMessage8));

		fixture.SetupParameters(setup91, setup92);
		fixture.Throws(new InvalidExpressionException(errorMessage9));

		const int inputValue1 = -14, inputValue2 = -21;
		Action actual = () => fixture.Execute(inputValue1, inputValue2, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}
}
