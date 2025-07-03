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
}
